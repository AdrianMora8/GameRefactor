using UnityEngine;
using FlappyBird.Configuration;
using FlappyBird.Core.Entities;
using FlappyBird.Gameplay.Environment;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Gameplay.Managers
{
    /// <summary>
    /// ============================================
    /// MANAGER: Difficulty Progression
    /// ============================================
    /// Manages dynamic difficulty based on score
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles difficulty adjustments
    /// 
    /// PATTERN: Observer Pattern
    /// - Listens to score changes via GameEvent
    /// ============================================
    /// </summary>
    public class DifficultyManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private DifficultyConfig difficultyConfig;

        [Header("Events")]
        [SerializeField] private IntGameEvent onScoreChanged;

        [Header("References")]
        [SerializeField] private PipeSpawner pipeSpawner;

        private DifficultyLevel _currentLevel;

        private void Start()
        {
            // Validate
            if (difficultyConfig == null)
            {
                Debug.LogError("[DifficultyManager] DifficultyConfig not assigned!");
                return;
            }

            if (pipeSpawner == null)
            {
                pipeSpawner = FindObjectOfType<PipeSpawner>();
                if (pipeSpawner == null)
                {
                    Debug.LogError("[DifficultyManager] PipeSpawner not found!");
                    return;
                }
            }

            // Subscribe to score changes
            if (onScoreChanged != null)
            {
                onScoreChanged.RegisterListener(OnScoreChanged);
                Debug.Log("[DifficultyManager] Subscribed to score changes");
            }

            // Set initial difficulty
            SetDifficulty(0);
        }

        private void OnDestroy()
        {
            // Unsubscribe
            if (onScoreChanged != null)
            {
                onScoreChanged.UnregisterListener(OnScoreChanged);
            }
        }

        private void OnScoreChanged(int newScore)
        {
            // Only update if score changed to a different difficulty threshold
            var newLevel = difficultyConfig.GetDifficultyForScore(newScore);

            if (_currentLevel == null || newLevel.LevelName != _currentLevel.LevelName)
            {
                SetDifficulty(newScore);
            }
        }

        private void SetDifficulty(int score)
        {
            _currentLevel = difficultyConfig.GetDifficultyForScore(score);

            if (_currentLevel == null)
            {
                Debug.LogWarning("[DifficultyManager] Could not determine difficulty level");
                return;
            }

            // Apply difficulty settings
            if (pipeSpawner != null)
            {
                pipeSpawner.SetSpawnRate(_currentLevel.SpawnRate);
                pipeSpawner.SetPipeGap(_currentLevel.PipeGap);
                pipeSpawner.SetPipeSpeed(_currentLevel.PipeSpeed);
            }

            Debug.Log($"[DifficultyManager] Difficulty set to: {_currentLevel.LevelName} " +
                      $"(Speed: {_currentLevel.PipeSpeed}, Gap: {_currentLevel.PipeGap}, Rate: {_currentLevel.SpawnRate})");
        }

        /// <summary>
        /// Reset to initial difficulty
        /// </summary>
        public void ResetDifficulty()
        {
            SetDifficulty(0);
        }
    }
}
