using UnityEngine;

namespace FlappyBird.Configuration
{
    /// <summary>
    /// ============================================
    /// SCRIPTABLE OBJECT: Game Configuration
    /// ============================================
    /// Centralizes all game settings in a single asset
    /// Allows runtime tweaking without recompilation
    /// SOLID: Single Responsibility - Only game settings
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Flappy Bird/Configuration/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [Header("Game Settings")]
        [Tooltip("Enable/disable debug logs")]
        public bool enableDebugLogs = false;
        
        [Tooltip("Target frame rate for the game")]
        public int targetFrameRate = 60;
        
        [Header("Difficulty Settings")]
        [Tooltip("Time between pipe spawns (lower = harder)")]
        [Range(1f, 5f)]
        public float pipeSpawnInterval = 2f;
        
        [Tooltip("Gap size between pipes (lower = harder)")]
        [Range(1f, 3f)]
        public float pipeGapSize = 2f;
        
        [Header("Physics Settings")]
        [Tooltip("Gravity scale for the bird")]
        [Range(0.5f, 3f)]
        public float birdGravityScale = 1f;
        
        [Header("Score Settings")]
        [Tooltip("Points awarded per pipe passed")]
        public int pointsPerPipe = 1;
        
        [Header("Vibration Settings")]
        [Tooltip("Enable haptic feedback on mobile")]
        public bool enableVibration = true;
    }
}
