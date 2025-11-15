using UnityEngine;

namespace FlappyBird.Core.Interfaces
{
    /// <summary>
    /// ============================================
    /// INTERFACE: Object Pool Service
    /// ============================================
    /// Contract for object pooling system
    /// 
    /// DESIGN PATTERN: Object Pool + Facade
    /// - Hides complexity of pool management
    /// - Single interface for all pooled objects
    /// ============================================
    /// </summary>
    public interface IPoolService
    {
        /// <summary>
        /// Get an object from the pool
        /// </summary>
        /// <param name="poolName">Name of the pool (e.g., "Pipe", "Background")</param>
        /// <param name="position">Position to place the object</param>
        /// <param name="rotation">Rotation of the object</param>
        GameObject Get(string poolName, Vector3 position, Quaternion rotation);

        /// <summary>
        /// Return an object to the pool
        /// </summary>
        void Return(string poolName, GameObject obj);

        /// <summary>
        /// Create a new pool
        /// </summary>
        void CreatePool(string poolName, GameObject prefab, int initialSize);

        /// <summary>
        /// Clear a specific pool
        /// </summary>
        void ClearPool(string poolName);

        /// <summary>
        /// Clear all pools
        /// </summary>
        void ClearAllPools();
    }
}
