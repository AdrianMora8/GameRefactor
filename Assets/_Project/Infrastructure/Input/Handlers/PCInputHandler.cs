using UnityEngine;

namespace FlappyBird.Infrastructure.Input.Handlers
{
    /// <summary>
    /// ============================================
    /// PC INPUT HANDLER
    /// ============================================
    /// Handles keyboard and mouse input for PC
    /// 
    /// FEATURES:
    /// - Keyboard input (Space, Enter, etc.)
    /// - Mouse clicks
    /// - Multiple key bindings
    /// ============================================
    /// </summary>
    public class PCInputHandler : IInputHandler
    {
        private bool _jumpRequested = false;

        /// <summary>
        /// Update PC input state
        /// </summary>
        public void UpdateInput()
        {
            _jumpRequested = false;

            // Check keyboard input
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) ||
                UnityEngine.Input.GetKeyDown(KeyCode.W) ||
                UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) ||
                UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                _jumpRequested = true;
            }

            // Check mouse input (left click)
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _jumpRequested = true;
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
        /// Get mouse position (for UI interactions)
        /// </summary>
        public Vector2 GetMousePosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        /// <summary>
        /// Check if mouse button is held
        /// </summary>
        public bool IsMouseHeld()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }
    }
}
