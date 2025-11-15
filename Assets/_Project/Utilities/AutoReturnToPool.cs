using UnityEngine;
using FlappyBird.Infrastructure.Pooling;

namespace FlappyBird.Utilities
{
    /// <summary>
    /// ============================================
    /// AUTO-RETURN TO POOL COMPONENT
    /// ============================================
    /// Automatically returns object to pool after a delay
    /// Useful for temporary objects (effects, particles, etc.)
    /// 
    /// Usage:
    /// Add this component to any pooled prefab
    /// Set returnDelay in Inspector
    /// Object will auto-return after spawning
    /// ============================================
    /// </summary>
    public class AutoReturnToPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [Tooltip("Name of the pool this object belongs to")]
        public string poolName;

        [Tooltip("Time in seconds before auto-returning to pool")]
        public float returnDelay = 5f;

        private float _spawnTime;

        private void OnEnable()
        {
            _spawnTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - _spawnTime >= returnDelay)
            {
                ReturnToPool();
            }
        }

        public void ReturnToPool()
        {
            // This will be called by PoolManager when implemented
            gameObject.SetActive(false);
        }
    }
}
