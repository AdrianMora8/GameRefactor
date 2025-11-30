using System.Collections.Generic;
using UnityEngine;
using FlappyBird.Core.Interfaces;

namespace FlappyBird.Infrastructure.Pooling
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Facade Pattern + Object Pool
    /// ============================================
    /// Manages multiple object pools
    /// Provides a unified interface for pooling system
    /// 
    /// SOLID: Single Responsibility
    /// - Only manages pools, doesn't handle game logic
    /// 
    /// SOLID: Dependency Inversion
    /// - Implements IPoolService interface
    /// - High-level modules depend on interface, not this class
    /// ============================================
    /// </summary>
    public class PoolManager : MonoBehaviour, IPoolService
    {
        private readonly Dictionary<string, IGenericPool> _pools = new Dictionary<string, IGenericPool>();

        /// <summary>
        /// Create a new pool with a specific name
        /// </summary>
        public void CreatePool(string poolName, GameObject prefab, int initialSize)
        {
            if (_pools.ContainsKey(poolName))
            {
                return;
            }

            var pool = new GameObjectPool(prefab, initialSize, transform);
            _pools.Add(poolName, pool);
        }

        /// <summary>
        /// Get an object from a named pool
        /// </summary>
        public GameObject Get(string poolName, Vector3 position, Quaternion rotation)
        {
            if (!_pools.TryGetValue(poolName, out IGenericPool pool))
            {
                return null;
            }

            return pool.Get(position, rotation);
        }

        /// <summary>
        /// Return an object to a named pool
        /// </summary>
        public void Return(string poolName, GameObject obj)
        {
            if (!_pools.TryGetValue(poolName, out IGenericPool pool))
            {
                return;
            }

            pool.Return(obj);
        }

        /// <summary>
        /// Clear a specific pool
        /// </summary>
        public void ClearPool(string poolName)
        {
            if (_pools.TryGetValue(poolName, out IGenericPool pool))
            {
                pool.Clear();
                _pools.Remove(poolName);
            }
        }

        /// <summary>
        /// Clear all pools
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }
            
            _pools.Clear();
        }

        /// <summary>
        /// Get pool statistics for debugging
        /// </summary>
        public void LogPoolStats()
        {
            // Pool statistics available via ActiveCount and AvailableCount properties
        }

        private void OnDestroy()
        {
            ClearAllPools();
        }
    }

    /// <summary>
    /// Internal interface for generic pool access
    /// Allows storing different pool types in same dictionary
    /// </summary>
    internal interface IGenericPool
    {
        GameObject Get(Vector3 position, Quaternion rotation);
        void Return(GameObject obj);
        void Clear();
        int ActiveCount { get; }
        int AvailableCount { get; }
    }

    /// <summary>
    /// GameObject-specific pool implementation
    /// </summary>
    internal class GameObjectPool : IGenericPool
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly Queue<GameObject> _availableObjects;
        private readonly List<GameObject> _allObjects;

        public int ActiveCount => _allObjects.Count - _availableObjects.Count;
        public int AvailableCount => _availableObjects.Count;

        public GameObjectPool(GameObject prefab, int initialSize, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _availableObjects = new Queue<GameObject>(initialSize);
            _allObjects = new List<GameObject>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject obj = _availableObjects.Count > 0 
                ? _availableObjects.Dequeue() 
                : CreateNewObject();

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            return obj;
        }

        public void Return(GameObject obj)
        {
            if (obj == null || !_allObjects.Contains(obj)) return;

            obj.SetActive(false);
            
            if (!_availableObjects.Contains(obj))
            {
                _availableObjects.Enqueue(obj);
            }
        }

        public void Clear()
        {
            foreach (GameObject obj in _allObjects)
            {
                if (obj != null)
                {
                    Object.Destroy(obj);
                }
            }

            _availableObjects.Clear();
            _allObjects.Clear();
        }

        private GameObject CreateNewObject()
        {
            GameObject newObj = Object.Instantiate(_prefab, _parent);
            newObj.SetActive(false);
            
            _allObjects.Add(newObj);
            _availableObjects.Enqueue(newObj);
            
            return newObj;
        }
    }
}
