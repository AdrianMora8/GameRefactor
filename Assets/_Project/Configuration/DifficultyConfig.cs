using UnityEngine;
using FlappyBird.Core.Entities;

namespace FlappyBird.Configuration
{
    /// <summary>
    /// ============================================
    /// CONFIGURATION: Difficulty Settings
    /// ============================================
    /// ScriptableObject for difficulty progression
    /// 
    /// Cycle: Level 1 → 2 → 3 → 4 → 1 → 2 → 3 → 4 → ...
    /// Each level lasts 10 points
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Flappy Bird/Configuration/Difficulty Config")]
    public class DifficultyConfig : ScriptableObject
    {
        [Header("Level 1 - Easy (0-9, 40-49, 80-89...)")]
        [SerializeField] private float level1PipeSpeed = 2f;
        [SerializeField] private float level1PipeGap = 5.0f;
        [SerializeField] private float level1SpawnRate = 2.0f;

        [Header("Level 2 - Normal (10-19, 50-59, 90-99...)")]
        [SerializeField] private float level2PipeSpeed = 2f;
        [SerializeField] private float level2PipeGap = 5.0f;
        [SerializeField] private float level2SpawnRate = 1.7f;

        [Header("Level 3 - Hard (20-29, 60-69, 100-109...)")]
        [SerializeField] private float level3PipeSpeed = 2f;
        [SerializeField] private float level3PipeGap = 4.5f;
        [SerializeField] private float level3SpawnRate = 1.7f;

        [Header("Level 4 - Very Hard (30-39, 70-79, 110-119...)")]
        [SerializeField] private float level4PipeSpeed = 2f;
        [SerializeField] private float level4PipeGap = 4.0f;
        [SerializeField] private float level4SpawnRate = 1.5f;

        private DifficultyLevel[] _difficultyLevels;

        private void OnEnable()
        {
            InitializeLevels();
        }

        private void InitializeLevels()
        {
            _difficultyLevels = new DifficultyLevel[]
            {
                new DifficultyLevel("Level 1", 0, 9, level1PipeSpeed, level1PipeGap, level1SpawnRate),
                new DifficultyLevel("Level 2", 10, 19, level2PipeSpeed, level2PipeGap, level2SpawnRate),
                new DifficultyLevel("Level 3", 20, 29, level3PipeSpeed, level3PipeGap, level3SpawnRate),
                new DifficultyLevel("Level 4", 30, 39, level4PipeSpeed, level4PipeGap, level4SpawnRate)
            };
        }

        /// <summary>
        /// Get difficulty level for a given score (cycles every 40 points)
        /// </summary>
        public DifficultyLevel GetDifficultyForScore(int score)
        {
            if (_difficultyLevels == null || _difficultyLevels.Length == 0)
            {
                InitializeLevels();
            }

            // Calculate which level in the cycle (0-3)
            // Score 0-9 = Level 0, Score 10-19 = Level 1, etc.
            // After 40, it cycles: Score 40-49 = Level 0 again
            int cyclePosition = (score / 10) % 4;
            
            return _difficultyLevels[cyclePosition];
        }

        /// <summary>
        /// Get current difficulty level number (1-4) for a given score
        /// </summary>
        public int GetDifficultyLevelNumber(int score)
        {
            return ((score / 10) % 4) + 1;
        }

        /// <summary>
        /// Get all difficulty levels
        /// </summary>
        public DifficultyLevel[] GetAllLevels()
        {
            if (_difficultyLevels == null || _difficultyLevels.Length == 0)
            {
                InitializeLevels();
            }
            return _difficultyLevels;
        }
    }
}
