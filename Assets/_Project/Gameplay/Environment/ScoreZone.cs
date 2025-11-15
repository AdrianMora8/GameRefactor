using UnityEngine;
using FlappyBird.Gameplay.Managers;

namespace FlappyBird.Gameplay.Environment
{
    /// <summary>
    /// ============================================
    /// SCORE ZONE: Trigger for Scoring
    /// ============================================
    /// Detects when bird passes through pipe gap
    /// Awards points to player
    /// 
    /// SOLID: Single Responsibility
    /// - Only detects scoring, doesn't handle score logic
    /// ============================================
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class ScoreZone : MonoBehaviour
    {
        private GameFlowManager _gameFlowManager;
        private bool _hasScored = false;

        private void Start()
        {
            // Find GameFlowManager
            _gameFlowManager = FindObjectOfType<GameFlowManager>();

            if (_gameFlowManager == null)
            {
                Debug.LogError("[ScoreZone] GameFlowManager not found!");
            }

            // Ensure collider is trigger
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Only score once per pipe
            if (_hasScored) return;

            // Check if it's the bird
            if (collision.CompareTag("Player"))
            {
                _hasScored = true;
                _gameFlowManager?.AddScorePoint();
                Debug.Log("[ScoreZone] Bird passed! Point awarded.");
            }
        }

        /// <summary>
        /// Reset for reuse (when pipe returns to pool)
        /// </summary>
        public void ResetScore()
        {
            _hasScored = false;
        }

        private void OnDrawGizmos()
        {
            // Visualize score zone
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
                Gizmos.DrawCube(
                    transform.position + (Vector3)collider.offset,
                    collider.size
                );
            }
        }
    }
}
