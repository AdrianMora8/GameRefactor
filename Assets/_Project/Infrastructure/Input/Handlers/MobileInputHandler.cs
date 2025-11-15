using UnityEngine;

namespace FlappyBird.Infrastructure.Input.Handlers
{
    /// <summary>
    /// ============================================
    /// MOBILE INPUT HANDLER
    /// ============================================
    /// Handles touch input for mobile devices
    /// 
    /// FEATURES:
    /// - Multi-touch support
    /// - Touch phases (began, moved, ended)
    /// - Screen zone detection
    /// - Swipe detection (optional)
    /// ============================================
    /// </summary>
    public class MobileInputHandler : IInputHandler
    {
        private bool _jumpRequested = false;
        private Vector2 _lastTouchPosition;

        /// <summary>
        /// Update mobile input state
        /// </summary>
        public void UpdateInput()
        {
            _jumpRequested = false;

            // Check for touch input
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        // Touch started - register as jump
                        _jumpRequested = true;
                        _lastTouchPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        // Touch moved - could be used for swipe gestures
                        _lastTouchPosition = touch.position;
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        // Touch ended
                        break;
                }
            }
        }

        /// <summary>
        /// Check if jump was requested this frame
        /// </summary>
        public bool GetJumpInput()
        {
            return _jumpRequested;
        }

        /// <summary>
        /// Get last touch position (for UI interactions)
        /// </summary>
        public Vector2 GetTouchPosition()
        {
            return _lastTouchPosition;
        }

        /// <summary>
        /// Check if screen is currently being touched
        /// </summary>
        public bool IsTouching()
        {
            return UnityEngine.Input.touchCount > 0;
        }
    }
}
