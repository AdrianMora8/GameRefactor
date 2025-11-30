using System.Collections;
using UnityEngine;
using FlappyBird.Core.Interfaces;
using FlappyBird.Infrastructure.DI;

namespace FlappyBird.Gameplay.Environment
{
    /// <summary>
    /// ============================================
    /// PIPE SPAWNER: Procedural Obstacle Generation
    /// ============================================
    /// Spawns pipes at intervals using object pooling
    /// 
    /// DESIGN PATTERN: Object Pool + Factory
    /// - Creates pipes on demand from pool
    /// - Randomizes gap position for difficulty
    /// 
    /// SOLID: Single Responsibility
    /// - Only spawns pipes, doesn't handle their behavior
    /// ============================================
    /// </summary>
    public class PipeSpawner : MonoBehaviour
    {
        [Header("Spawning")]
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private float spawnInterval = 2f;
        [SerializeField] private float spawnX = 10f;

        [Header("Randomization")]
        [SerializeField] private float minY = -2f;
        [SerializeField] private float maxY = 2f;

        [Header("Pipe Settings")]
        [SerializeField] private float pipeSpeed = 2f;
        [SerializeField] private float pipeGap = 2.5f;

        private IPoolService _poolService;
        private Coroutine _spawnCoroutine;
        private bool _isSpawning = false;

        private const string PIPE_POOL_NAME = "PipePool";

        // Dynamic difficulty properties
        private float _currentSpawnInterval;
        private float _currentPipeSpeed;
        private float _currentPipeGap;

        private void Start()
        {
            // Initialize dynamic values with defaults
            _currentSpawnInterval = spawnInterval;
            _currentPipeSpeed = pipeSpeed;
            _currentPipeGap = pipeGap;

            // Get pool service
            _poolService = ServiceLocator.Get<IPoolService>();

            if (_poolService == null)
            {
                Debug.LogError("[PipeSpawner] PoolService not found!");
                return;
            }

            // Create pipe pool
            if (pipePrefab != null)
            {
                _poolService.CreatePool(PIPE_POOL_NAME, pipePrefab, 5);
                Debug.Log("[PipeSpawner] Pipe pool created");
            }
            else
            {
                Debug.LogError("[PipeSpawner] Pipe prefab not assigned!");
            }
        }

        /// <summary>
        /// Start spawning pipes
        /// </summary>
        public void StartSpawning()
        {
            if (_isSpawning) return;

            _isSpawning = true;
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
            Debug.Log("[PipeSpawner] Started spawning pipes");
        }

        /// <summary>
        /// Stop spawning pipes
        /// </summary>
        public void StopSpawning()
        {
            if (!_isSpawning) return;

            _isSpawning = false;

            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            Debug.Log("[PipeSpawner] Stopped spawning pipes");
        }

        /// <summary>
        /// Clear all active pipes
        /// </summary>
        public void ClearAllPipes()
        {
            if (_poolService == null) return;

            // Find all active pipes and deactivate them
            Pipe[] activePipes = FindObjectsOfType<Pipe>();
            foreach (Pipe pipe in activePipes)
            {
                pipe.Deactivate();
            }

            Debug.Log("[PipeSpawner] All pipes cleared");
        }

        private IEnumerator SpawnRoutine()
        {
            while (_isSpawning)
            {
                SpawnPipe();
                yield return new WaitForSeconds(_currentSpawnInterval);
            }
        }

        private void SpawnPipe()
        {
            if (_poolService == null) return;

            // Random Y position for difficulty
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

            // Get pipe from pool - position is set here initially
            GameObject pipe = _poolService.Get(PIPE_POOL_NAME, spawnPosition, Quaternion.identity);

            if (pipe != null)
            {
                // Activate pipe behavior with current difficulty settings
                // Using the dynamic values (_currentPipeSpeed, _currentPipeGap) instead of serialized defaults
                Pipe pipeComponent = pipe.GetComponent<Pipe>();
                if (pipeComponent != null)
                {
                    // Pass current dynamic gap and speed to avoid visual repositioning
                    pipeComponent.Activate(spawnPosition, _currentPipeSpeed, _currentPipeGap);
                }
                else
                {
                    Debug.LogWarning("[PipeSpawner] Pipe component not found on spawned object!");
                }
            }
        }

        /// <summary>
        /// Set spawn rate dynamically (for difficulty)
        /// </summary>
        public void SetSpawnRate(float rate)
        {
            _currentSpawnInterval = rate;
            Debug.Log($"[PipeSpawner] Spawn rate set to: {rate}s");
        }

        /// <summary>
        /// Set pipe gap dynamically (for difficulty)
        /// </summary>
        public void SetPipeGap(float gap)
        {
            _currentPipeGap = gap;
            Debug.Log($"[PipeSpawner] Pipe gap set to: {gap}");
        }

        /// <summary>
        /// Set pipe speed dynamically (for difficulty)
        /// </summary>
        public void SetPipeSpeed(float speed)
        {
            _currentPipeSpeed = speed;
            pipeSpeed = speed; // Update serialized field too
            Debug.Log($"[PipeSpawner] Pipe speed set to: {speed}");
        }

        private void OnDrawGizmosSelected()
        {
            // Draw spawn line
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                new Vector3(spawnX, -10f, 0f),
                new Vector3(spawnX, 10f, 0f)
            );

            // Draw spawn range
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(
                new Vector3(spawnX, minY, 0f),
                new Vector3(spawnX, maxY, 0f)
            );
        }
    }
}
