using UnityEngine;
using FlappyBird.Core.Interfaces;
using FlappyBird.Infrastructure.DI;

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
    /// Usage:
    /// Add this component to a GameObject in your scene
    /// It will auto-register the correct input service on Awake
    /// ============================================
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [Header("Debug")]
        [Tooltip("Force a specific platform for testing")]
        public bool forcePC = false;
        
        [Tooltip("Force mobile input for testing")]
        public bool forceMobile = false;

        private IInputService _inputService;

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
                Debug.Log("[InputManager] Forcing PC input mode");
            }
            else if (forceMobile)
            {
                isMobile = true;
                Debug.Log("[InputManager] Forcing Mobile input mode");
            }

            // Create appropriate input provider
            if (isMobile)
            {
                _inputService = new MobileInputProvider();
                Debug.Log("[InputManager] Mobile input initialized");
            }
            else
            {
                _inputService = new PCInputProvider();
                Debug.Log("[InputManager] PC input initialized");
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
            if (!Application.isEditor) return;

            // Show current input mode in top-left corner
            GUIStyle style = new GUIStyle();
            style.fontSize = 16;
            style.normal.textColor = Color.white;
            style.padding = new RectOffset(10, 10, 10, 10);

            string inputMode = _inputService is PCInputProvider ? "PC Input" : "Mobile Input";
            GUI.Label(new Rect(10, 10, 200, 30), $"Input: {inputMode}", style);
        }

        #endregion
    }
}
