using UnityEngine;
using FlappyBird.Core.Entities;

namespace FlappyBird.Gameplay.Environment
{
    /// <summary>
    /// ============================================
    /// BACKGROUND SCROLLER: Parallax Effect
    /// ============================================
    /// Creates infinite scrolling background
    /// 
    /// DESIGN PATTERN: Facade
    /// - Simplifies parallax scrolling logic
    /// ============================================
    /// </summary>
    public class BackgroundScroller : MonoBehaviour
    {
        [Header("Scrolling")]
        [SerializeField] private float scrollSpeed = 1f;
        [SerializeField] private Vector2 offset = Vector2.zero;

        [Header("Looping")]
        [SerializeField] private bool loop = true;
        [SerializeField] private float resetPositionX = -10f;
        [SerializeField] private float startPositionX = 10f;

        private Material _material;
        private bool _isScrolling = false;

        private void Start()
        {
            // Get material from renderer
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                _material = renderer.material;
            }

        }

        private void Update()
        {
            if (!_isScrolling || _material == null) return;

            // Scroll material offset
            offset.x += scrollSpeed * Time.deltaTime;
            _material.mainTextureOffset = offset;

            // Loop position if using transform scrolling
            if (loop && transform.position.x < resetPositionX)
            {
                Vector3 pos = transform.position;
                pos.x = startPositionX;
                transform.position = pos;
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
        /// Reset scroll position
        /// </summary>
        public void ResetScroll()
        {
            offset = Vector2.zero;
            if (_material != null)
            {
                _material.mainTextureOffset = offset;
            }
        }
    }
}
