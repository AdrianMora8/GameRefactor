using System;
using FlappyBird.UI.Views;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Pause Screen Logic
    /// ============================================
    /// Handles pause menu interactions
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// ============================================
    /// </summary>
    public class PausePresenter
    {
        private readonly PauseView _view;

        public event Action OnResumeRequested;
        public event Action OnMenuRequested;

        public PausePresenter(PauseView view)
        {
            _view = view;

            // Setup button listeners
            var resumeButton = _view.GetResumeButton();
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(HandleResume);
            }

            var menuButton = _view.GetMenuButton();
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(HandleMenu);
            }
        }

        /// <summary>
        /// Show pause screen
        /// </summary>
        public void Show()
        {
            _view.Show();
        }

        /// <summary>
        /// Hide pause screen
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        private void HandleResume()
        {
            OnResumeRequested?.Invoke();
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
            var resumeButton = _view.GetResumeButton();
            if (resumeButton != null)
            {
                resumeButton.onClick.RemoveListener(HandleResume);
            }

            var menuButton = _view.GetMenuButton();
            if (menuButton != null)
            {
                menuButton.onClick.RemoveListener(HandleMenu);
            }
        }
    }
}
