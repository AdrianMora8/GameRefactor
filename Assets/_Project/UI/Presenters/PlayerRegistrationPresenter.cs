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

            // Setup register button listener
            var registerButton = _view.GetRegisterButton();
            if (registerButton != null)
            {
                registerButton.onClick.AddListener(HandleRegisterButton);
            }

            // Setup login button listener
            var loginButton = _view.GetLoginButton();
            if (loginButton != null)
            {
                loginButton.onClick.AddListener(HandleLoginButton);
            }

            // Setup input field listener (Enter key for login)
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

        private void HandleRegisterButton()
        {
            RegisterPlayer();
        }

        private void HandleLoginButton()
        {
            LoginPlayer();
        }

        private void OnPasswordSubmit(string input)
        {
            LoginPlayer();
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

        private void RegisterPlayer()
        {
            if (!ValidateInput(out string playerName, out string password))
            {
                return;
            }

            // Register new player
            try
            {
                _playerService.RegisterPlayer(playerName, password);

                _view.HideError();
                _view.ClearInput();

                // Notify
                OnPlayerRegistered?.Invoke(playerName);
            }
            catch (Exception e)
            {
                _view.ShowError(e.Message);
            }
        }

        private void LoginPlayer()
        {
            if (!ValidateInput(out string playerName, out string password))
            {
                return;
            }

            // Login existing player
            try
            {
                _playerService.LoginPlayer(playerName, password);

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
            var registerButton = _view.GetRegisterButton();
            if (registerButton != null)
            {
                registerButton.onClick.RemoveListener(HandleRegisterButton);
            }

            var loginButton = _view.GetLoginButton();
            if (loginButton != null)
            {
                loginButton.onClick.RemoveListener(HandleLoginButton);
            }

            var passwordField = _view.GetPasswordInputField();
            if (passwordField != null)
            {
                passwordField.onSubmit.RemoveListener(OnPasswordSubmit);
            }
        }
    }
}
