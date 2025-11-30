using System;
using FlappyBird.UI.Views;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Main Menu Logic
    /// ============================================
    /// Handles main menu logic
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// ============================================
    /// </summary>
    public class MainMenuPresenter
    {
        private readonly MainMenuView _view;

        public event Action OnPlayRequested;
        public event Action OnChangePlayerRequested;
        public event Action OnLeaderboardRequested;
        public event Action OnQuitRequested;

        public MainMenuPresenter(MainMenuView view)
        {
            _view = view;

            // Setup button listeners
            var playButton = _view.GetPlayButton();
            if (playButton != null)
            {
                playButton.onClick.AddListener(HandlePlay);
            }

            var changePlayerButton = _view.GetChangePlayerButton();
            if (changePlayerButton != null)
            {
                changePlayerButton.onClick.AddListener(HandleChangePlayer);
            }

            var leaderboardButton = _view.GetLeaderboardButton();
            if (leaderboardButton != null)
            {
                leaderboardButton.onClick.AddListener(HandleLeaderboard);
            }

            var quitButton = _view.GetQuitButton();
            if (quitButton != null)
            {
                quitButton.onClick.AddListener(HandleQuit);
            }
        }

        /// <summary>
        /// Show main menu
        /// </summary>
        public void Show()
        {
            _view.Show();
        }

        /// <summary>
        /// Hide main menu
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        private void HandlePlay()
        {
            OnPlayRequested?.Invoke();
        }

        private void HandleChangePlayer()
        {
            OnChangePlayerRequested?.Invoke();
        }

        private void HandleLeaderboard()
        {
            OnLeaderboardRequested?.Invoke();
        }

        private void HandleQuit()
        {
            OnQuitRequested?.Invoke();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            var playButton = _view.GetPlayButton();
            if (playButton != null)
            {
                playButton.onClick.RemoveListener(HandlePlay);
            }

            var changePlayerButton = _view.GetChangePlayerButton();
            if (changePlayerButton != null)
            {
                changePlayerButton.onClick.RemoveListener(HandleChangePlayer);
            }

            var leaderboardButton = _view.GetLeaderboardButton();
            if (leaderboardButton != null)
            {
                leaderboardButton.onClick.RemoveListener(HandleLeaderboard);
            }

            var quitButton = _view.GetQuitButton();
            if (quitButton != null)
            {
                quitButton.onClick.RemoveListener(HandleQuit);
            }
        }
    }
}
