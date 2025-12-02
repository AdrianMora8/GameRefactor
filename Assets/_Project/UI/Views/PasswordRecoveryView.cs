using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Password Recovery Screen
    /// ============================================
    /// Simple UI for password reset
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// - Only UI, no logic
    /// ============================================
    /// </summary>
    public class PasswordRecoveryView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField newPasswordInputField;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI messageText;

        /// <summary>
        /// Get username input field
        /// </summary>
        public TMP_InputField GetUsernameInputField() => usernameInputField;

        /// <summary>
        /// Get new password input field
        /// </summary>
        public TMP_InputField GetNewPasswordInputField() => newPasswordInputField;

        /// <summary>
        /// Get reset button
        /// </summary>
        public Button GetResetButton() => resetButton;

        /// <summary>
        /// Get cancel button
        /// </summary>
        public Button GetCancelButton() => cancelButton;

        /// <summary>
        /// Get current username
        /// </summary>
        public string GetUsername()
        {
            return usernameInputField != null ? usernameInputField.text : string.Empty;
        }

        /// <summary>
        /// Get new password
        /// </summary>
        public string GetNewPassword()
        {
            return newPasswordInputField != null ? newPasswordInputField.text : string.Empty;
        }

        /// <summary>
        /// Show message (error or success)
        /// </summary>
        public void ShowMessage(string message, bool isError = true)
        {
            if (messageText != null)
            {
                messageText.text = message;
                messageText.color = isError ? Color.red : Color.green;
                messageText.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Hide message
        /// </summary>
        public void HideMessage()
        {
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Clear all inputs
        /// </summary>
        public void ClearInputs()
        {
            if (usernameInputField != null)
            {
                usernameInputField.text = string.Empty;
            }
            if (newPasswordInputField != null)
            {
                newPasswordInputField.text = string.Empty;
            }
        }

        /// <summary>
        /// Show recovery screen
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            HideMessage();
            ClearInputs();

            if (usernameInputField != null)
            {
                usernameInputField.Select();
                usernameInputField.ActivateInputField();
            }
        }

        /// <summary>
        /// Hide recovery screen
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
