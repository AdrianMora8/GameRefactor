using UnityEngine;
using System.Collections.Generic;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// FLOATING TEXT SPAWNER
    /// ============================================
    /// Spawns and pools floating text effects
    /// 
    /// DESIGN PATTERN: Object Pool
    /// ============================================
    /// </summary>
    public class FloatingTextSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject floatingTextPrefab;
        
        [Header("Pool Settings")]
        [SerializeField] private int poolSize = 10;

        [Header("Spawn Settings")]
        [SerializeField] private Vector3 spawnOffset = new Vector3(0.5f, 0.5f, 0f);

        private static FloatingTextSpawner _instance;
        public static FloatingTextSpawner Instance => _instance;

        private Queue<FloatingText> _pool;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;

            InitializePool();
        }

        private void InitializePool()
        {
            _pool = new Queue<FloatingText>();

            if (floatingTextPrefab == null) return;

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(floatingTextPrefab, transform);
                obj.SetActive(false);
                
                FloatingText floatingText = obj.GetComponent<FloatingText>();
                if (floatingText != null)
                {
                    _pool.Enqueue(floatingText);
                }
            }
        }

        /// <summary>
        /// Spawn "+1" floating text at position
        /// </summary>
        public void SpawnScoreText(Vector3 position)
        {
            SpawnText("+1", position + spawnOffset);
        }

        /// <summary>
        /// Spawn custom text at position
        /// </summary>
        public void SpawnText(string text, Vector3 position)
        {
            if (_pool == null || _pool.Count == 0) return;

            FloatingText floatingText = _pool.Dequeue();
            floatingText.Initialize(text, position);

            // Return to pool after animation (handled by coroutine in FloatingText)
            StartCoroutine(ReturnToPoolAfterDelay(floatingText, 1f));
        }

        private System.Collections.IEnumerator ReturnToPoolAfterDelay(FloatingText text, float delay)
        {
            yield return new WaitForSeconds(delay);
            _pool.Enqueue(text);
        }
    }
}
