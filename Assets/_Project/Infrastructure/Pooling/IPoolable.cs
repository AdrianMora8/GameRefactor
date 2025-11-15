using UnityEngine;

namespace FlappyBird.Infrastructure.Pooling
{
    /// <summary>
    /// ============================================
    /// POOLABLE OBJECT INTERFACE
    /// ============================================
    /// Optional interface for pooled objects
    /// Allows objects to reset their state when returned to pool
    /// 
    /// Usage:
    /// public class Pipe : MonoBehaviour, IPoolable
    /// {
    ///     public void OnSpawnFromPool() { /* Reset state */ }
    ///     public void OnReturnToPool() { /* Clean up */ }
    /// }
    /// ============================================
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Called when object is taken from pool
        /// Use this to reset state, enable components, etc.
        /// </summary>
        void OnSpawnFromPool();

        /// <summary>
        /// Called when object is returned to pool
        /// Use this to clean up, disable components, etc.
        /// </summary>
        void OnReturnToPool();
    }
}
