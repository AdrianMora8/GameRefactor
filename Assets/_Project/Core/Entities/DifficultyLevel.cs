namespace FlappyBird.Core.Entities
{
    /// <summary>
    /// ============================================
    /// CORE ENTITY: Difficulty Level Data
    /// ============================================
    /// Pure data class for difficulty settings
    /// No Unity dependencies, no behavior - just data
    /// 
    /// SOLID: Single Responsibility
    /// - Only stores difficulty parameters
    /// ============================================
    /// </summary>
    [System.Serializable]
    public class DifficultyLevel
    {
        public string LevelName { get; }
        public int MinScore { get; }
        public int MaxScore { get; }
        public float PipeSpeed { get; }
        public float PipeGap { get; }
        public float SpawnRate { get; }

        public DifficultyLevel(
            string levelName,
            int minScore,
            int maxScore,
            float pipeSpeed,
            float pipeGap,
            float spawnRate)
        {
            LevelName = levelName;
            MinScore = minScore;
            MaxScore = maxScore;
            PipeSpeed = pipeSpeed;
            PipeGap = pipeGap;
            SpawnRate = spawnRate;
        }

        /// <summary>
        /// Check if this difficulty level applies to the given score
        /// </summary>
        public bool AppliesTo(int score)
        {
            return score >= MinScore && (MaxScore < 0 || score <= MaxScore);
        }
    }
}
