using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Gameplay HUD
    /// ============================================
    /// Displays in-game information (score)
    /// Pure UI, no game logic
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// - Only handles visual representation
    /// - Presenter handles logic
    /// ============================================
    /// </summary>
    public class GameplayHUDView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject getReadyPanel;

        /// <summary>
        /// Update score display
        /// </summary>
        public void UpdateScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }

        /// <summary>
        /// Show "Get Ready" panel
        /// </summary>
        public void ShowGetReady()
        {
            if (getReadyPanel != null)
            {
                getReadyPanel.SetActive(true);
            }
        }

        /// <summary>
        /// Hide "Get Ready" panel
        /// </summary>
        public void HideGetReady()
        {
            if (getReadyPanel != null)
            {
                getReadyPanel.SetActive(false);
            }
        }

        /// <summary>
        /// Show HUD
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide HUD
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
