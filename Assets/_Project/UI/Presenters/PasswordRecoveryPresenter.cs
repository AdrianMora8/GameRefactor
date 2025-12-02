using System;
using FlappyBird.UI.Views;
using FlappyBird.Infrastructure.Services;
using FlappyBird.Infrastructure.DI;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Password Recovery Logic
    /// ============================================
    /// Handles password reset flow
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// - Contains business logic
    /// - Coordinates between View and Service
    /// ============================================
    /// </summary>
    public class PasswordRecoveryPresenter
    {
        private readonly PasswordRecoveryView _view;
        private readonly PlayerService _playerService;

        public event Action OnPasswordReset;
        public event Action OnCancelled;

        public PasswordRecoveryPresenter(PasswordRecoveryView view)
        {
            _view = view;
            _playerService = ServiceLocator.Get<PlayerService>();

            // Setup button listeners
            var resetButton = _view.GetResetButton();
            if (resetButton != null)
            {
                resetButton.onClick.AddListener(HandleResetButton);
            }

            var cancelButton = _view.GetCancelButton();
            if (cancelButton != null)
            {
                cancelButton.onClick.AddListener(HandleCancelButton);
            }

            // Enter key on password field
            var passwordField = _view.GetNewPasswordInputField();
            if (passwordField != null)
            {
                passwordField.onSubmit.AddListener(OnPasswordSubmit);
            }
        }

        /// <summary>
        /// Show recovery screen
        /// </summary>
        public void Show()
        {
            _view.Show();
        }

        /// <summary>
        /// Hide recovery screen
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        private void HandleResetButton()
        {
            ResetPassword();
        }

        private void HandleCancelButton()
        {
            _view.Hide();
            OnCancelled?.Invoke();
        }

        private void OnPasswordSubmit(string input)
        {
            ResetPassword();
        }

        private void ResetPassword()
        {
            string username = _view.GetUsername().Trim();
            string newPassword = _view.GetNewPassword();

            // Validate username
            if (string.IsNullOrWhiteSpace(username))
            {
                _view.ShowMessage("Please enter your username!");
                return;
            }

            // Validate new password
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                _view.ShowMessage("Please enter a new password!");
                return;
            }

            if (newPassword.Length < 3)
            {
                _view.ShowMessage("Password must be at least 3 characters!");
                return;
            }

            // Try to reset password
            try
            {
                _playerService.ResetPassword(username, newPassword);

                _view.ShowMessage("Password changed successfully!", isError: false);
                _view.ClearInputs();

                // Notify success after a moment
                OnPasswordReset?.Invoke();
            }
            catch (Exception e)
            {
                _view.ShowMessage(e.Message);
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            var resetButton = _view.GetResetButton();
            if (resetButton != null)
            {
                resetButton.onClick.RemoveListener(HandleResetButton);
            }

            var cancelButton = _view.GetCancelButton();
            if (cancelButton != null)
            {
                cancelButton.onClick.RemoveListener(HandleCancelButton);
            }

            var passwordField = _view.GetNewPasswordInputField();
            if (passwordField != null)
            {
                passwordField.onSubmit.RemoveListener(OnPasswordSubmit);
            }
        }
    }
}
