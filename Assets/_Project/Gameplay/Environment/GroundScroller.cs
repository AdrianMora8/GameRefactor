using UnityEngine;

namespace FlappyBird.Gameplay.Environment
{
    /// <summary>
    /// ============================================
    /// GROUND SCROLLER: Infinite Ground Effect
    /// ============================================
    /// Creates infinite scrolling ground
    /// Uses dual sprites for seamless looping
    /// ============================================
    /// </summary>
    public class GroundScroller : MonoBehaviour
    {
        [Header("Scrolling")]
        [SerializeField] private float scrollSpeed = 2f;

        [Header("Ground Sprites")]
        [SerializeField] private Transform ground1;
        [SerializeField] private Transform ground2;

        [Header("Looping")]
        [SerializeField] private float groundWidth = 10f;

        private bool _isScrolling = false;

        private void Update()
        {
            if (!_isScrolling) return;

            // Move both grounds left
            if (ground1 != null)
            {
                ground1.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

                // Reset position when off screen
                if (ground1.position.x < -groundWidth)
                {
                    Vector3 pos = ground1.position;
                    pos.x += groundWidth * 2f;
                    ground1.position = pos;
                }
            }

            if (ground2 != null)
            {
                ground2.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

                // Reset position when off screen
                if (ground2.position.x < -groundWidth)
                {
                    Vector3 pos = ground2.position;
                    pos.x += groundWidth * 2f;
                    ground2.position = pos;
                }
            }
        }

        /// <summary>
        /// Start scrolling
        /// </summary>
        public void StartScrolling()
        {
            _isScrolling = true;
        }

        /// <summary>
        /// Stop scrolling
        /// </summary>
        public void StopScrolling()
        {
            _isScrolling = false;
        }

        /// <summary>
        /// Reset ground positions
        /// </summary>
        public void ResetPositions()
        {
            if (ground1 != null)
            {
                Vector3 pos = ground1.localPosition;
                pos.x = 0f;
                ground1.localPosition = pos;
            }

            if (ground2 != null)
            {
                Vector3 pos = ground2.localPosition;
                pos.x = groundWidth;
                ground2.localPosition = pos;
            }
        }
    }
}
