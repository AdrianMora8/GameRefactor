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
    /// ============================================
    /// </summary>
    public class Pipe : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 2f;

        [Header("Pooling")]
        [SerializeField] private float despawnX = -10f;

        private bool _isActive = false;

        /// <summary>
        /// Activate pipe (called by pool)
        /// </summary>
        public void Activate(Vector3 position, float speed)
        {
            transform.position = position;
            moveSpeed = speed;
            _isActive = true;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivate pipe (return to pool)
        /// </summary>
        public void Deactivate()
        {
            _isActive = false;
            gameObject.SetActive(false);
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

        private void OnDrawGizmosSelected()
        {
            // Draw despawn line
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                new Vector3(despawnX, -10f, 0f),
                new Vector3(despawnX, 10f, 0f)
            );
        }
    }
}
