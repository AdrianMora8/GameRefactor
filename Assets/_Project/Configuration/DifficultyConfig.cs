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
    /// SOLID: Open/Closed Principle
    /// - Open for extension (add new levels)
    /// - Closed for modification (no code changes needed)
    /// 
    /// PATTERN: Strategy Pattern
    /// - Different difficulty configurations
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Flappy Bird/Configuration/Difficulty Config")]
    public class DifficultyConfig : ScriptableObject
    {
        [Header("Easy Difficulty (0-9 points)")]
        [SerializeField] private float easyPipeSpeed = 3f;
        [SerializeField] private float easyPipeGap = 2.5f;
        [SerializeField] private float easySpawnRate = 2f;

        [Header("Medium Difficulty (10-19 points)")]
        [SerializeField] private float mediumPipeSpeed = 4f;
        [SerializeField] private float mediumPipeGap = 2.2f;
        [SerializeField] private float mediumSpawnRate = 1.5f;

        [Header("Hard Difficulty (20+ points)")]
        [SerializeField] private float hardPipeSpeed = 5f;
        [SerializeField] private float hardPipeGap = 2.0f;
        [SerializeField] private float hardSpawnRate = 1.2f;

        private DifficultyLevel[] _difficultyLevels;

        private void OnEnable()
        {
            InitializeLevels();
        }

        private void InitializeLevels()
        {
            _difficultyLevels = new DifficultyLevel[]
            {
                new DifficultyLevel("Easy", 0, 9, easyPipeSpeed, easyPipeGap, easySpawnRate),
                new DifficultyLevel("Medium", 10, 19, mediumPipeSpeed, mediumPipeGap, mediumSpawnRate),
                new DifficultyLevel("Hard", 20, -1, hardPipeSpeed, hardPipeGap, hardSpawnRate) // -1 = no max
            };
        }

        /// <summary>
        /// Get difficulty level for a given score
        /// </summary>
        public DifficultyLevel GetDifficultyForScore(int score)
        {
            if (_difficultyLevels == null || _difficultyLevels.Length == 0)
            {
                InitializeLevels();
            }

            foreach (var level in _difficultyLevels)
            {
                if (level.AppliesTo(score))
                {
                    return level;
                }
            }

            // Fallback to hardest
            return _difficultyLevels[_difficultyLevels.Length - 1];
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
