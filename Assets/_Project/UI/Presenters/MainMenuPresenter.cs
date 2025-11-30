using System;
using UnityEngine;
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
                Debug.Log("[MainMenuPresenter] Play button listener added");
            }
            else
            {
                Debug.LogWarning("[MainMenuPresenter] Play button is NULL!");
            }

            var changePlayerButton = _view.GetChangePlayerButton();
            if (changePlayerButton != null)
            {
                changePlayerButton.onClick.AddListener(HandleChangePlayer);
                Debug.Log("[MainMenuPresenter] Change player button listener added");
            }
            else
            {
                Debug.LogWarning("[MainMenuPresenter] Change player button is NULL!");
            }

            var leaderboardButton = _view.GetLeaderboardButton();
            if (leaderboardButton != null)
            {
                leaderboardButton.onClick.AddListener(HandleLeaderboard);
                Debug.Log("[MainMenuPresenter] Leaderboard button listener added");
            }
            else
            {
                Debug.LogWarning("[MainMenuPresenter] Leaderboard button is NULL - this is OK if not implemented");
            }

            var quitButton = _view.GetQuitButton();
            if (quitButton != null)
            {
                quitButton.onClick.AddListener(HandleQuit);
                Debug.Log("[MainMenuPresenter] Quit button listener added");
            }
            else
            {
                Debug.LogWarning("[MainMenuPresenter] Quit button is NULL - this is OK");
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
            Debug.Log("<color=green>[MainMenuPresenter] HandlePlay() called!</color>");
            OnPlayRequested?.Invoke();
        }

        private void HandleChangePlayer()
        {
            Debug.Log("<color=yellow>[MainMenuPresenter] HandleChangePlayer() called!</color>");
            OnChangePlayerRequested?.Invoke();
        }

        private void HandleLeaderboard()
        {
            Debug.Log("<color=magenta>[MainMenuPresenter] HandleLeaderboard() called!</color>");
            OnLeaderboardRequested?.Invoke();
        }

        private void HandleQuit()
        {
            Debug.Log("<color=red>[MainMenuPresenter] HandleQuit() called!</color>");
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
