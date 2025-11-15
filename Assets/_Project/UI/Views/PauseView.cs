using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Pause Screen
    /// ============================================
    /// Displays pause menu with resume/menu options
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// ============================================
    /// </summary>
    public class PauseView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button menuButton;

        [Header("Background")]
        [SerializeField] private Image backgroundOverlay;

        /// <summary>
        /// Get resume button (for presenter)
        /// </summary>
        public Button GetResumeButton() => resumeButton;

        /// <summary>
        /// Get menu button (for presenter)
        /// </summary>
        public Button GetMenuButton() => menuButton;

        /// <summary>
        /// Show pause screen
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide pause screen
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
