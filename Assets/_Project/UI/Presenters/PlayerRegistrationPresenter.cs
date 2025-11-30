using System;
using UnityEngine;
using FlappyBird.UI.Views;
using FlappyBird.Infrastructure.Services;
using FlappyBird.Infrastructure.DI;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Player Registration Logic
    /// ============================================
    /// Handles player registration/login
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// - Contains business logic
    /// - Coordinates between View and Service
    /// ============================================
    /// </summary>
    public class PlayerRegistrationPresenter
    {
        private readonly PlayerRegistrationView _view;
        private PlayerService _playerService;

        public event Action<string> OnPlayerRegistered;

        public PlayerRegistrationPresenter(PlayerRegistrationView view)
        {
            _view = view;

            // Get PlayerService
            _playerService = ServiceLocator.Get<PlayerService>();

            if (_playerService == null)
            {
                Debug.LogError("[PlayerRegistrationPresenter] PlayerService not found!");
            }

            // Setup button listener
            var startButton = _view.GetStartButton();
            if (startButton != null)
            {
                startButton.onClick.AddListener(HandleStartButton);
            }

            // Setup input field listener (Enter key)
            var inputField = _view.GetNameInputField();
            if (inputField != null)
            {
                inputField.onSubmit.AddListener(OnInputSubmit);
            }
        }

        /// <summary>
        /// Show registration screen
        /// </summary>
        public void Show()
        {
            _view.Show();
        }

        /// <summary>
        /// Hide registration screen
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        private void HandleStartButton()
        {
            RegisterPlayer();
        }

        private void OnInputSubmit(string input)
        {
            RegisterPlayer();
        }

        private void RegisterPlayer()
        {
            string playerName = _view.GetPlayerName().Trim();

            // Validate
            if (string.IsNullOrWhiteSpace(playerName))
            {
                _view.ShowError("Please enter your name!");
                return;
            }

            if (playerName.Length < 2)
            {
                _view.ShowError("Name must be at least 2 characters!");
                return;
            }

            if (playerName.Length > 15)
            {
                _view.ShowError("Name must be less than 15 characters!");
                return;
            }

            // Register player
            try
            {
                _playerService.RegisterPlayer(playerName);
                Debug.Log($"[PlayerRegistrationPresenter] Player registered: {playerName}");

                _view.HideError();
                _view.ClearInput();

                // Notify
                OnPlayerRegistered?.Invoke(playerName);
            }
            catch (Exception e)
            {
                _view.ShowError($"Error: {e.Message}");
                Debug.LogError($"[PlayerRegistrationPresenter] Error registering player: {e.Message}");
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            var startButton = _view.GetStartButton();
            if (startButton != null)
            {
                startButton.onClick.RemoveListener(HandleStartButton);
            }

            var inputField = _view.GetNameInputField();
            if (inputField != null)
            {
                inputField.onSubmit.RemoveListener(OnInputSubmit);
            }
        }
    }
}
