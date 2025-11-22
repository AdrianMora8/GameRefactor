using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Main Menu Screen
    /// ============================================
    /// Start screen with play button
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// ============================================
    /// </summary>
    public class MainMenuView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        [Header("Panels")]
        [SerializeField] private GameObject titlePanel;

        /// <summary>
        /// Get play button (for presenter)
        /// </summary>
        public Button GetPlayButton() => playButton;

        /// <summary>
        /// Get quit button (for presenter)
        /// </summary>
        public Button GetQuitButton() => quitButton;

        /// <summary>
        /// Show menu
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide menu
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
