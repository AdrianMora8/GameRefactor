using System;

namespace FlappyBird.Core.Entities
{
    /// <summary>
    /// ============================================
    /// CORE ENTITY: Player Data
    /// ============================================
    /// Pure data class representing player information
    /// No Unity dependencies, no behavior - just data
    /// 
    /// SOLID: Single Responsibility
    /// - Only stores player data
    /// 
    /// This is NOT a MonoBehaviour - it's pure C#
    /// ============================================
    /// </summary>
    [Serializable]
    public class Player
    {
        public string Name { get; private set; }
        public int BestScore { get; private set; }
        public DateTime LastPlayedDate { get; private set; }

        /// <summary>
        /// Constructor for new player
        /// </summary>
        public Player(string name)
        {
            Name = name;
            BestScore = 0;
            LastPlayedDate = DateTime.Now;
        }

        /// <summary>
        /// Constructor for loading existing player
        /// </summary>
        public Player(string name, int bestScore, DateTime lastPlayedDate)
        {
            Name = name;
            BestScore = bestScore;
            LastPlayedDate = lastPlayedDate;
        }

        /// <summary>
        /// Update player's best score if new score is higher
        /// </summary>
        public bool UpdateScore(int newScore)
        {
            if (newScore > BestScore)
            {
                BestScore = newScore;
                LastPlayedDate = DateTime.Now;
                return true;
            }

            LastPlayedDate = DateTime.Now;
            return false;
        }

        /// <summary>
        /// Reset player data
        /// </summary>
        public void Reset()
        {
            BestScore = 0;
            LastPlayedDate = DateTime.Now;
        }
    }
}
