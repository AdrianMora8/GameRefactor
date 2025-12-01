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

            // Setup start button listener (handles both register and login)
            var startButton = _view.GetStartButton();
            if (startButton != null)
            {
                startButton.onClick.AddListener(HandleStartButton);
            }

            // Setup input field listener (Enter key)
            var passwordField = _view.GetPasswordInputField();
            if (passwordField != null)
            {
                passwordField.onSubmit.AddListener(OnPasswordSubmit);
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
            AuthenticatePlayer();
        }

        private void OnPasswordSubmit(string input)
        {
            AuthenticatePlayer();
        }

        private bool ValidateInput(out string playerName, out string password)
        {
            playerName = _view.GetPlayerName().Trim();
            password = _view.GetPassword();

            // Validate name
            if (string.IsNullOrWhiteSpace(playerName))
            {
                _view.ShowError("Please enter your name!");
                return false;
            }

            if (playerName.Length < 2)
            {
                _view.ShowError("Name must be at least 2 characters!");
                return false;
            }

            if (playerName.Length > 15)
            {
                _view.ShowError("Name must be less than 15 characters!");
                return false;
            }

            // Validate password
            if (string.IsNullOrWhiteSpace(password))
            {
                _view.ShowError("Please enter a password!");
                return false;
            }

            if (password.Length < 3)
            {
                _view.ShowError("Password must be at least 3 characters!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Auto-detect: if player exists -> login, if not -> register
        /// </summary>
        private void AuthenticatePlayer()
        {
            if (!ValidateInput(out string playerName, out string password))
            {
                return;
            }

            try
            {
                // Check if player exists
                if (_playerService.PlayerExists(playerName))
                {
                    // Player exists -> Login
                    _playerService.LoginPlayer(playerName, password);
                }
                else
                {
                    // New player -> Register
                    _playerService.RegisterPlayer(playerName, password);
                }

                _view.HideError();
                _view.ClearInput();

                // Notify
                OnPlayerRegistered?.Invoke(playerName);
            }
            catch (UnauthorizedAccessException)
            {
                _view.ShowError("Incorrect password!");
            }
            catch (Exception e)
            {
                _view.ShowError(e.Message);
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

            var passwordField = _view.GetPasswordInputField();
            if (passwordField != null)
            {
                passwordField.onSubmit.RemoveListener(OnPasswordSubmit);
            }
        }
    }
}
