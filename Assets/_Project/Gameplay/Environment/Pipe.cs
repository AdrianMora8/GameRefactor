using UnityEngine;

namespace FlappyBird.Gameplay.Environment
{
    /// <summary>
    /// ============================================
    /// PIPE: Individual Pipe Behavior
    /// ============================================
    /// Single pipe obstacle behavior
    /// Works with object pooling system
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles pipe movement and collision
    /// 
    /// Prefab Structure Expected:
    /// - Pipes (root) <- this script
    ///   - Pipe (top pipe child)
    ///   - Pipe (1) (bottom pipe child)  
    ///   - MiddlePipe (score zone trigger)
    /// ============================================
    /// </summary>
    public class Pipe : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 2f;

        [Header("Pooling")]
        [SerializeField] private float despawnX = -10f;

        [Header("Pipe Children References")]
        [Tooltip("Reference to the top pipe child. If null, will try to find by index.")]
        [SerializeField] private Transform topPipe;
        [Tooltip("Reference to the bottom pipe child. If null, will try to find by index.")]
        [SerializeField] private Transform bottomPipe;

        [Header("Default Gap Settings")]
        [Tooltip("This should match the original gap in the prefab")]
        [SerializeField] private float defaultGap = 2.5f;

        private bool _isActive = false;
        private float _currentGap;
        private Vector3 _topPipeInitialLocalPos;
        private Vector3 _bottomPipeInitialLocalPos;
        private float _originalGapDistance;
        private bool _initialPositionsCached = false;

        private void Awake()
        {
            CacheChildReferences();
        }

        /// <summary>
        /// Cache references to top and bottom pipe children
        /// </summary>
        private void CacheChildReferences()
        {
            // Try to find children if not assigned
            if (topPipe == null && transform.childCount > 0)
            {
                topPipe = transform.GetChild(0);
            }

            if (bottomPipe == null && transform.childCount > 1)
            {
                bottomPipe = transform.GetChild(1);
            }

            // Cache initial positions for gap calculations
            if (!_initialPositionsCached)
            {
                if (topPipe != null)
                    _topPipeInitialLocalPos = topPipe.localPosition;
                if (bottomPipe != null)
                    _bottomPipeInitialLocalPos = bottomPipe.localPosition;
                
                // Calculate original gap from prefab positions
                if (topPipe != null && bottomPipe != null)
                {
                    _originalGapDistance = _topPipeInitialLocalPos.y - _bottomPipeInitialLocalPos.y;
                }
                else
                {
                    _originalGapDistance = defaultGap;
                }
                
                _initialPositionsCached = true;
                _currentGap = defaultGap;
            }
        }

        /// <summary>
        /// Activate pipe (called by pool) - backwards compatible
        /// </summary>
        public void Activate(Vector3 position, float speed)
        {
            Activate(position, speed, defaultGap);
        }

        /// <summary>
        /// Activate pipe with specific gap (for dynamic difficulty)
        /// </summary>
        public void Activate(Vector3 position, float speed, float gap)
        {
            // Ensure references are cached
            if (!_initialPositionsCached)
            {
                CacheChildReferences();
            }

            // Set position FIRST before enabling
            transform.position = position;
            moveSpeed = speed;
            _currentGap = gap;

            // IMPORTANT: Do NOT adjust pipe children positions
            // The prefab is already configured with correct gap
            // Only reset to original prefab positions
            ResetToOriginalPositions();

            // Now activate
            _isActive = true;
            gameObject.SetActive(true);

            // Reset score zone
            ResetScoreZone();
        }

        /// <summary>
        /// Reset pipe children to their original prefab positions
        /// </summary>
        private void ResetToOriginalPositions()
        {
            if (topPipe != null)
                topPipe.localPosition = _topPipeInitialLocalPos;
            if (bottomPipe != null)
                bottomPipe.localPosition = _bottomPipeInitialLocalPos;
        }

        /// <summary>
        /// Adjust the gap between top and bottom pipes
        /// NOTE: Currently disabled - using original prefab positions
        /// </summary>
        private void AdjustGap(float gap)
        {
            // Disabled - prefab is already configured with correct gap
            // If you need dynamic gap adjustment, the sprites need proper pivot points
            return;
        }

        /// <summary>
        /// Reset the score zone trigger
        /// </summary>
        private void ResetScoreZone()
        {
            ScoreZone scoreZone = GetComponentInChildren<ScoreZone>();
            if (scoreZone != null)
            {
                scoreZone.ResetScore();
            }
        }

        /// <summary>
        /// Deactivate pipe (return to pool)
        /// </summary>
        public void Deactivate()
        {
            _isActive = false;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Called when returning to pool - reset state
        /// </summary>
        public void OnReturnToPool()
        {
            _isActive = false;
            ResetScoreZone();
        }

        private void Update()
        {
            if (!_isActive) return;

            // Move left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // Check if out of bounds
            if (transform.position.x < despawnX)
            {
                Deactivate();
            }
        }

        /// <summary>
        /// Get current gap size
        /// </summary>
        public float GetCurrentGap() => _currentGap;

        private void OnDrawGizmosSelected()
        {
            // Draw despawn line
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                new Vector3(despawnX, -10f, 0f),
                new Vector3(despawnX, 10f, 0f)
            );

            // Draw gap visualization
            Gizmos.color = Color.green;
            float halfGap = _currentGap / 2f;
            Gizmos.DrawLine(
                transform.position + Vector3.up * halfGap,
                transform.position + Vector3.down * halfGap
            );
        }
    }
}
