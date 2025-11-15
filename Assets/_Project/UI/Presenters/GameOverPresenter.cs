using System;
using FlappyBird.UI.Views;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Game Over Logic
    /// ============================================
    /// Handles game over screen logic
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// ============================================
    /// </summary>
    public class GameOverPresenter
    {
        private readonly GameOverView _view;

        public event Action OnRestartRequested;
        public event Action OnMenuRequested;

        public GameOverPresenter(GameOverView view)
        {
            _view = view;

            // Setup button listeners
            var restartButton = _view.GetRestartButton();
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(HandleRestart);
            }

            var menuButton = _view.GetMenuButton();
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(HandleMenu);
            }
        }

        /// <summary>
        /// Show game over screen with scores
        /// </summary>
        public void ShowGameOver(int currentScore, int bestScore, bool isNewBest)
        {
            _view.UpdateScores(currentScore, bestScore, isNewBest);
            _view.ShowMedal(currentScore);
            _view.Show();
        }

        /// <summary>
        /// Hide game over screen
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        private void HandleRestart()
        {
            OnRestartRequested?.Invoke();
        }

        private void HandleMenu()
        {
            OnMenuRequested?.Invoke();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            var restartButton = _view.GetRestartButton();
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(HandleRestart);
            }

            var menuButton = _view.GetMenuButton();
            if (menuButton != null)
            {
                menuButton.onClick.RemoveListener(HandleMenu);
            }
        }
    }
}
