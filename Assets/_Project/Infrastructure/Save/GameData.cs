using System;
using UnityEngine;

namespace FlappyBird.Infrastructure.Save
{
    /// <summary>
    /// ============================================
    /// GAME DATA MODEL
    /// ============================================
    /// Represents all saveable game data
    /// Can be serialized to JSON for alternative save systems
    /// 
    /// Usage:
    /// var data = new GameData();
    /// data.bestScore = 100;
    /// string json = JsonUtility.ToJson(data);
    /// ============================================
    /// </summary>
    [Serializable]
    public class GameData
    {
        [Header("Scores")]
        public int bestScore;
        public int totalGames;
        public int totalDeaths;

        [Header("Settings")]
        public bool soundEnabled = true;
        public bool musicEnabled = true;
        public float masterVolume = 1f;
        public float sfxVolume = 1f;
        public float musicVolume = 0.5f;

        [Header("Statistics")]
        public int totalJumps;
        public int totalPipesPassed;
        public float totalPlayTime; // In seconds

        [Header("Timestamps")]
        public string lastPlayedDate;
        public string firstPlayedDate;

        /// <summary>
        /// Create default game data
        /// </summary>
        public GameData()
        {
            bestScore = 0;
            totalGames = 0;
            totalDeaths = 0;
            soundEnabled = true;
            musicEnabled = true;
            masterVolume = 1f;
            sfxVolume = 1f;
            musicVolume = 0.5f;
            totalJumps = 0;
            totalPipesPassed = 0;
            totalPlayTime = 0f;
            firstPlayedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lastPlayedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Reset all data to defaults
        /// </summary>
        public void Reset()
        {
            bestScore = 0;
            totalGames = 0;
            totalDeaths = 0;
            totalJumps = 0;
            totalPipesPassed = 0;
            totalPlayTime = 0f;
            // Keep settings
        }

        /// <summary>
        /// Convert to JSON string
        /// </summary>
        public string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }

        /// <summary>
        /// Load from JSON string
        /// </summary>
        public static GameData FromJson(string json)
        {
            try
            {
                return JsonUtility.FromJson<GameData>(json);
            }
            catch (Exception)
            {
                return new GameData();
            }
        }
    }
}
