using UnityEngine;

namespace FlappyBird.Configuration
{
    /// <summary>
    /// ============================================
    /// SCRIPTABLE OBJECT: Object Pool Configuration
    /// ============================================
    /// Contains settings for object pooling system
    /// SOLID: Single Responsibility - Only pool settings
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Flappy Bird/Configuration/Pool Config")]
    public class PoolConfig : ScriptableObject
    {
        [Header("Pipe Pooling")]
        [Tooltip("Prefab for pipe obstacles")]
        public GameObject pipePrefab;
        
        [Tooltip("Initial pool size for pipes")]
        [Range(10, 50)]
        public int pipePoolSize = 25;
        
        [Tooltip("Distance between pipe spawns")]
        [Range(1f, 3f)]
        public float pipeSpawnDistance = 2f;
        
        [Tooltip("Minimum Y position for pipe spawning")]
        public float pipeMinY = -1.34f;
        
        [Tooltip("Maximum Y position for pipe spawning")]
        public float pipeMaxY = 1.03f;
        
        [Header("Background Pooling")]
        [Tooltip("Prefab for background tiles")]
        public GameObject backgroundPrefab;
        
        [Tooltip("Initial pool size for backgrounds")]
        [Range(5, 20)]
        public int backgroundPoolSize = 12;
        
        [Tooltip("Width of each background tile")]
        public float backgroundWidth = 4.5f;
        
        [Tooltip("Distance before spawning new background")]
        public float backgroundSpawnThreshold = 4f;
    }
}
