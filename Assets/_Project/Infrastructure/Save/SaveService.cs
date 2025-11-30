using UnityEngine;
using FlappyBird.Core.Interfaces;

namespace FlappyBird.Infrastructure.Save
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Adapter Pattern
    /// ============================================
    /// Wraps Unity's PlayerPrefs with our ISaveService interface
    /// 
    /// Benefits:
    /// - Easy to swap to different save system (JSON, Cloud, etc.)
    /// - Testable (can create mock save service)
    /// - Type-safe with helper methods
    /// 
    /// SOLID: Dependency Inversion
    /// - Game logic depends on ISaveService, not PlayerPrefs
    /// - Can swap implementation without changing game code
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles data persistence
    /// ============================================
    /// </summary>
    public class SaveService : ISaveService
    {
        private const string ENCRYPTION_KEY = "FlappyBird2025"; // Simple obfuscation key

        #region ISaveService Implementation

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public float LoadFloat(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public void SaveBool(string key, bool value)
        {
            // PlayerPrefs doesn't support bool, so we convert to int
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
            }
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        #endregion

        #region Additional Helper Methods

        /// <summary>
        /// Save encrypted string (simple obfuscation, not secure encryption)
        /// </summary>
        public void SaveEncryptedString(string key, string value)
        {
            string encrypted = SimpleEncrypt(value);
            SaveString(key, encrypted);
        }

        /// <summary>
        /// Load encrypted string
        /// </summary>
        public string LoadEncryptedString(string key, string defaultValue = "")
        {
            string encrypted = LoadString(key, "");
            if (string.IsNullOrEmpty(encrypted))
            {
                return defaultValue;
            }
            return SimpleDecrypt(encrypted);
        }

        /// <summary>
        /// Get all saved keys (debugging only - PlayerPrefs doesn't support this natively)
        /// </summary>
        public void LogAllSavedData()
        {
            // Saved data available via Load methods
        }

        #endregion

        #region Simple Encryption (Obfuscation)

        private string SimpleEncrypt(string plainText)
        {
            // Simple XOR cipher for obfuscation (NOT secure for sensitive data)
            char[] chars = plainText.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] ^ ENCRYPTION_KEY[i % ENCRYPTION_KEY.Length]);
            }
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(chars));
        }

        private string SimpleDecrypt(string encryptedText)
        {
            try
            {
                byte[] bytes = System.Convert.FromBase64String(encryptedText);
                char[] chars = System.Text.Encoding.UTF8.GetString(bytes).ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    chars[i] = (char)(chars[i] ^ ENCRYPTION_KEY[i % ENCRYPTION_KEY.Length]);
                }
                return new string(chars);
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}
