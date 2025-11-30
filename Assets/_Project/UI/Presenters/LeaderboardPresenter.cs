using System;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird.UI.Views;
using FlappyBird.Infrastructure.Services;
using FlappyBird.Infrastructure.DI;
using FlappyBird.Core.Entities;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Leaderboard Logic
    /// ============================================
    /// Handles leaderboard display logic
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// - Contains business logic
    /// - Coordinates between View and Service
    /// ============================================
    /// </summary>
    public class LeaderboardPresenter
    {
        private readonly LeaderboardView _view;
        private PlayerService _playerService;

        public event Action OnReplayRequested;
        public event Action OnExitRequested;

        public LeaderboardPresenter(LeaderboardView view)
        {
            _view = view;

            // Get PlayerService
            _playerService = ServiceLocator.Get<PlayerService>();

            if (_playerService == null)
            {
                Debug.LogError("[LeaderboardPresenter] PlayerService not found!");
            }

            // Setup button listeners
            var replayButton = _view.GetReplayButton();
            if (replayButton != null)
            {
                replayButton.onClick.AddListener(HandleReplay);
                Debug.Log("[LeaderboardPresenter] Replay button listener added");
            }

            var exitButton = _view.GetExitButton();
            if (exitButton != null)
            {
                exitButton.onClick.AddListener(HandleExit);
                Debug.Log("[LeaderboardPresenter] Exit button listener added");
            }
        }

        /// <summary>
        /// Show leaderboard with top N players
        /// </summary>
        public void Show(int topN = 10)
        {
            Debug.Log($"[LeaderboardPresenter] Showing leaderboard (top {topN})");

            List<Player> players = new List<Player>();

            if (_playerService != null)
            {
                players = _playerService.GetLeaderboard(topN);
                Debug.Log($"[LeaderboardPresenter] Retrieved {players.Count} players from service");
            }
            else
            {
                Debug.LogWarning("[LeaderboardPresenter] PlayerService is null, showing empty leaderboard");
            }

            _view.SetTitle("TOP 10");
            _view.Show(players);
        }

        /// <summary>
        /// Hide leaderboard
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        private void HandleReplay()
        {
            Debug.Log("<color=green>[LeaderboardPresenter] HandleReplay() called!</color>");
            OnReplayRequested?.Invoke();
        }

        private void HandleExit()
        {
            Debug.Log("<color=yellow>[LeaderboardPresenter] HandleExit() called!</color>");
            OnExitRequested?.Invoke();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            var replayButton = _view.GetReplayButton();
            if (replayButton != null)
            {
                replayButton.onClick.RemoveListener(HandleReplay);
            }

            var exitButton = _view.GetExitButton();
            if (exitButton != null)
            {
                exitButton.onClick.RemoveListener(HandleExit);
            }
        }
    }
}
