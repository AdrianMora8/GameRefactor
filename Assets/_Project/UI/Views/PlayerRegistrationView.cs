using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Player Registration Screen
    /// ============================================
    /// Input for player name
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// - Only UI, no logic
    /// ============================================
    /// </summary>
    public class PlayerRegistrationView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private Button startButton;
        [SerializeField] private Button forgotPasswordButton;
        [SerializeField] private TextMeshProUGUI errorText;

        /// <summary>
        /// Get name input field
        /// </summary>
        public TMP_InputField GetNameInputField() => nameInputField;

        /// <summary>
        /// Get password input field
        /// </summary>
        public TMP_InputField GetPasswordInputField() => passwordInputField;

        /// <summary>
        /// Get start button (used for both register and login)
        /// </summary>
        public Button GetStartButton() => startButton;

        /// <summary>
        /// Get forgot password button
        /// </summary>
        public Button GetForgotPasswordButton() => forgotPasswordButton;

        /// <summary>
        /// Get current input name
        /// </summary>
        public string GetPlayerName()
        {
            return nameInputField != null ? nameInputField.text : string.Empty;
        }

        /// <summary>
        /// Get current input password
        /// </summary>
        public string GetPassword()
        {
            return passwordInputField != null ? passwordInputField.text : string.Empty;
        }

        /// <summary>
        /// Show error message
        /// </summary>
        public void ShowError(string message)
        {
            if (errorText != null)
            {
                errorText.text = message;
                errorText.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Hide error message
        /// </summary>
        public void HideError()
        {
            if (errorText != null)
            {
                errorText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Clear input
        /// </summary>
        public void ClearInput()
        {
            if (nameInputField != null)
            {
                nameInputField.text = string.Empty;
            }
            if (passwordInputField != null)
            {
                passwordInputField.text = string.Empty;
            }
        }

        /// <summary>
        /// Show registration screen
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            HideError();
            
            // Focus on input field
            if (nameInputField != null)
            {
                nameInputField.Select();
                nameInputField.ActivateInputField();
            }
        }

        /// <summary>
        /// Hide registration screen
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
