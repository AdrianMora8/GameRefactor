using UnityEngine;
using FlappyBird.Core.Interfaces;
using FlappyBird.Infrastructure.DI;
using FlappyBird.Infrastructure.Input.Handlers;

namespace FlappyBird.Infrastructure.Input
{
    /// <summary>
    /// ============================================
    /// INPUT MANAGER - Platform Detection & Setup
    /// ============================================
    /// Automatically detects platform and registers appropriate input provider
    /// 
    /// DESIGN PATTERN: Strategy Pattern + Facade
    /// - Hides complexity of platform detection
    /// - Automatically selects correct strategy
    /// 
    /// PHASE 16 UPDATE:
    /// - Uses dedicated IInputHandler for each platform
    /// - Supports force override for testing
    /// - Visual debug mode
    /// ============================================
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [Header("Platform Override (Testing)")]
        [Tooltip("Force a specific platform for testing")]
        public bool forcePC = false;
        
        [Tooltip("Force mobile input for testing")]
        public bool forceMobile = false;

        [Header("Debug Visualization")]
        [Tooltip("Show touch/click indicators on screen")]
        public bool showInputDebug = false;

        private IInputService _inputService;
        private IInputHandler _inputHandler;

        private void Awake()
        {
            InitializeInputService();
        }

        private void InitializeInputService()
        {
            // Determine platform
            bool isMobile = IsMobilePlatform();

            // Override for testing
            if (forcePC)
            {
                isMobile = false;
                Debug.Log("[InputManager] 🖥️ Forcing PC input mode");
            }
            else if (forceMobile)
            {
                isMobile = true;
                Debug.Log("[InputManager] 📱 Forcing Mobile input mode");
            }

            // Create appropriate input handler
            if (isMobile)
            {
                _inputHandler = new MobileInputHandler();
                _inputService = new MobileInputProvider();
                Debug.Log("[InputManager] 📱 Mobile input initialized (Touch)");
            }
            else
            {
                _inputHandler = new PCInputHandler();
                _inputService = new PCInputProvider();
                Debug.Log("[InputManager] 🖥️ PC input initialized (Keyboard/Mouse)");
            }

            // Register with ServiceLocator
            ServiceLocator.Register<IInputService>(_inputService);
        }

        private bool IsMobilePlatform()
        {
            #if UNITY_ANDROID || UNITY_IOS
                return true;
            #elif UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
                return false;
            #else
                // Fallback: check RuntimePlatform
                return Application.platform == RuntimePlatform.Android ||
                       Application.platform == RuntimePlatform.IPhonePlayer;
            #endif
        }

        #region Debug Helpers

        private void OnGUI()
        {
            if (!showInputDebug) return;

            // Show current input mode
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 18;
            labelStyle.normal.textColor = Color.yellow;
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.padding = new RectOffset(10, 10, 10, 10);

            string inputMode = _inputService is PCInputProvider ? "🖥️ PC Input (Keyboard/Mouse)" : "📱 Mobile Input (Touch)";
            GUI.Label(new Rect(10, 10, 400, 40), inputMode, labelStyle);

            // Show touch/click visualization
            if (_inputHandler != null && _inputHandler.GetJumpInput())
            {
                // Draw indicator at touch/click position
                Vector2 inputPos = Vector2.zero;
                
                if (_inputHandler is MobileInputHandler mobileHandler)
                {
                    inputPos = mobileHandler.GetTouchPosition();
                }
                else if (_inputHandler is PCInputHandler pcHandler)
                {
                    inputPos = pcHandler.GetMousePosition();
                }

                // Draw circle at input position
                DrawCircle(inputPos, 30f, Color.green);
            }
        }

        private void DrawCircle(Vector2 position, float radius, Color color)
        {
            GUIStyle style = new GUIStyle();
            style.normal.background = Texture2D.whiteTexture;
            
            Color oldColor = GUI.color;
            GUI.color = new Color(color.r, color.g, color.b, 0.5f);
            GUI.Box(new Rect(position.x - radius, Screen.height - position.y - radius, radius * 2, radius * 2), "", style);
            GUI.color = oldColor;
        }

        #endregion
    }
}
