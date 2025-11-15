using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Game Over Screen
    /// ============================================
    /// Displays game over information and scores
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// - Only UI, no logic
    /// ============================================
    /// </summary>
    public class GameOverView : MonoBehaviour
    {
        [Header("Score Display")]
        [SerializeField] private TextMeshProUGUI currentScoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private GameObject newBestIcon;

        [Header("Buttons")]
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        [Header("Medal/Reward")]
        [SerializeField] private Image medalImage;
        [SerializeField] private Sprite bronzeMedal;
        [SerializeField] private Sprite silverMedal;
        [SerializeField] private Sprite goldMedal;

        private void Awake()
        {
            // Ensure buttons have listeners set up by presenter
            if (restartButton != null)
            {
                // Listener will be added by presenter
            }

            if (menuButton != null)
            {
                // Listener will be added by presenter
            }
        }

        /// <summary>
        /// Update scores display
        /// </summary>
        public void UpdateScores(int currentScore, int bestScore, bool isNewBest)
        {
            if (currentScoreText != null)
            {
                currentScoreText.text = currentScore.ToString();
            }

            if (bestScoreText != null)
            {
                bestScoreText.text = bestScore.ToString();
            }

            if (newBestIcon != null)
            {
                newBestIcon.SetActive(isNewBest);
            }
        }

        /// <summary>
        /// Show medal based on score
        /// </summary>
        public void ShowMedal(int score)
        {
            if (medalImage == null) return;

            if (score >= 40 && goldMedal != null)
            {
                medalImage.sprite = goldMedal;
                medalImage.gameObject.SetActive(true);
            }
            else if (score >= 20 && silverMedal != null)
            {
                medalImage.sprite = silverMedal;
                medalImage.gameObject.SetActive(true);
            }
            else if (score >= 10 && bronzeMedal != null)
            {
                medalImage.sprite = bronzeMedal;
                medalImage.gameObject.SetActive(true);
            }
            else
            {
                medalImage.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Get restart button (for presenter to add listener)
        /// </summary>
        public Button GetRestartButton() => restartButton;

        /// <summary>
        /// Get menu button (for presenter to add listener)
        /// </summary>
        public Button GetMenuButton() => menuButton;

        /// <summary>
        /// Show game over screen
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide game over screen
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
