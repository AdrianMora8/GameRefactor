using UnityEngine;
using System.Collections.Generic;

namespace FlappyBird.Infrastructure.Optimization
{
    /// <summary>
    /// ============================================
    /// GENERIC OBJECT POOL
    /// ============================================
    /// Reusable pool for any GameObject
    /// Reduces GC allocations and improves performance
    /// 
    /// DESIGN PATTERN: Object Pool
    /// - Pre-instantiate objects
    /// - Reuse instead of destroy/instantiate
    /// - Better performance
    /// 
    /// USAGE:
    /// var pool = new GenericObjectPool<Pipe>(pipePrefab, 10);
    /// Pipe obj = pool.Get();
    /// pool.Return(obj);
    /// ============================================
    /// </summary>
    /// <typeparam name="T">MonoBehaviour type to pool</typeparam>
    public class GenericObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _pool;
        private readonly List<T> _active;
        private readonly int _initialSize;

        public int AvailableCount => _pool.Count;
        public int ActiveCount => _active.Count;
        public int TotalCount => _pool.Count + _active.Count;

        /// <summary>
        /// Create object pool
        /// </summary>
        /// <param name="prefab">Prefab to instantiate</param>
        /// <param name="initialSize">Initial pool size</param>
        /// <param name="parent">Parent transform (optional)</param>
        public GenericObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _initialSize = initialSize;
            _pool = new Queue<T>(initialSize);
            _active = new List<T>(initialSize);

            // Pre-warm pool
            for (int i = 0; i < initialSize; i++)
            {
                T obj = CreateNewObject();
                _pool.Enqueue(obj);
            }

            Debug.Log($"[ObjectPool] Created pool for {typeof(T).Name} with {initialSize} objects");
        }

        /// <summary>
        /// Get object from pool
        /// </summary>
        public T Get()
        {
            T obj;

            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }
            else
            {
                // Pool exhausted - create new object
                obj = CreateNewObject();
                Debug.LogWarning($"[ObjectPool] Pool exhausted for {typeof(T).Name}, creating new object");
            }

            obj.gameObject.SetActive(true);
            _active.Add(obj);

            return obj;
        }

        /// <summary>
        /// Return object to pool
        /// </summary>
        public void Return(T obj)
        {
            if (obj == null) return;

            obj.gameObject.SetActive(false);
            _active.Remove(obj);
            _pool.Enqueue(obj);
        }

        /// <summary>
        /// Return all active objects to pool
        /// </summary>
        public void ReturnAll()
        {
            // Copy list to avoid modification during iteration
            T[] activeObjects = _active.ToArray();
            
            foreach (T obj in activeObjects)
            {
                Return(obj);
            }
        }

        /// <summary>
        /// Clear entire pool
        /// </summary>
        public void Clear()
        {
            foreach (T obj in _active)
            {
                if (obj != null)
                    Object.Destroy(obj.gameObject);
            }

            foreach (T obj in _pool)
            {
                if (obj != null)
                    Object.Destroy(obj.gameObject);
            }

            _active.Clear();
            _pool.Clear();
        }

        private T CreateNewObject()
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }

    /// <summary>
    /// ============================================
    /// POOL STATISTICS
    /// ============================================
    /// Helper class for pool debugging
    /// ============================================
    /// </summary>
    public static class PoolStatistics
    {
        public static void LogPoolStats<T>(GenericObjectPool<T> pool, string poolName) where T : MonoBehaviour
        {
            Debug.Log($"[Pool Stats] {poolName}:\n" +
                     $"  Available: {pool.AvailableCount}\n" +
                     $"  Active: {pool.ActiveCount}\n" +
                     $"  Total: {pool.TotalCount}");
        }
    }
}
