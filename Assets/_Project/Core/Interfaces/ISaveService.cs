namespace FlappyBird.Core.Interfaces
{
    /// <summary>
    /// ============================================
    /// INTERFACE: Save/Load Service
    /// ============================================
    /// Contract for data persistence
    /// 
    /// SOLID: Dependency Inversion
    /// - Game logic doesn't depend on PlayerPrefs directly
    /// - Can swap to JSON, Cloud Save, etc. easily
    /// ============================================
    /// </summary>
    public interface ISaveService
    {
        /// <summary>
        /// Save an integer value
        /// </summary>
        void SaveInt(string key, int value);

        /// <summary>
        /// Load an integer value
        /// </summary>
        int LoadInt(string key, int defaultValue = 0);

        /// <summary>
        /// Save a float value
        /// </summary>
        void SaveFloat(string key, float value);

        /// <summary>
        /// Load a float value
        /// </summary>
        float LoadFloat(string key, float defaultValue = 0f);

        /// <summary>
        /// Save a string value
        /// </summary>
        void SaveString(string key, string value);

        /// <summary>
        /// Load a string value
        /// </summary>
        string LoadString(string key, string defaultValue = "");

        /// <summary>
        /// Save a boolean value
        /// </summary>
        void SaveBool(string key, bool value);

        /// <summary>
        /// Load a boolean value
        /// </summary>
        bool LoadBool(string key, bool defaultValue = false);

        /// <summary>
        /// Check if a key exists
        /// </summary>
        bool HasKey(string key);

        /// <summary>
        /// Delete a specific key
        /// </summary>
        void DeleteKey(string key);

        /// <summary>
        /// Delete all saved data
        /// </summary>
        void DeleteAll();
    }
}
