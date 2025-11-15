namespace FlappyBird.Core.Entities
{
    /// <summary>
    /// ============================================
    /// CORE ENTITY: Score Data
    /// ============================================
    /// Represents score and scoring logic
    /// Pure C# class, no Unity dependencies
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles score calculations and tracking
    /// ============================================
    /// </summary>
    public class ScoreData
    {
        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }
        public bool IsNewBestScore { get; private set; }
        
        // Combo/streak system (for future features)
        public int CurrentStreak { get; private set; }
        public int BestStreak { get; private set; }

        /// <summary>
        /// Initialize with saved best score
        /// </summary>
        public ScoreData(int savedBestScore = 0)
        {
            CurrentScore = 0;
            BestScore = savedBestScore;
            IsNewBestScore = false;
            CurrentStreak = 0;
            BestStreak = 0;
        }

        /// <summary>
        /// Add points to current score
        /// </summary>
        public void AddScore(int points = 1)
        {
            CurrentScore += points;
            CurrentStreak++;

            // Track best streak
            if (CurrentStreak > BestStreak)
            {
                BestStreak = CurrentStreak;
            }

            // Check if new best score
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
                IsNewBestScore = true;
            }
        }

        /// <summary>
        /// Reset current game score (keep best score)
        /// </summary>
        public void ResetCurrentScore()
        {
            CurrentScore = 0;
            CurrentStreak = 0;
            IsNewBestScore = false;
        }

        /// <summary>
        /// Reset all scores (including best)
        /// </summary>
        public void ResetAll()
        {
            CurrentScore = 0;
            BestScore = 0;
            CurrentStreak = 0;
            BestStreak = 0;
            IsNewBestScore = false;
        }

        /// <summary>
        /// Update best score from saved data
        /// </summary>
        public void LoadBestScore(int savedBestScore)
        {
            BestScore = savedBestScore;
            
            // Check if current score is still better
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
                IsNewBestScore = true;
            }
        }

        /// <summary>
        /// Break combo streak (e.g., on pause)
        /// </summary>
        public void BreakStreak()
        {
            CurrentStreak = 0;
        }

        /// <summary>
        /// Get score difference from best
        /// </summary>
        public int GetScoreGap()
        {
            return BestScore - CurrentScore;
        }

        /// <summary>
        /// Check if player is close to best score
        /// </summary>
        public bool IsNearBestScore(int threshold = 5)
        {
            return GetScoreGap() <= threshold && GetScoreGap() > 0;
        }
    }
}
