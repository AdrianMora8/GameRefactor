using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Infrastructure.Pooling
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Object Pool Pattern
    /// ============================================
    /// Generic object pool for any Component type
    /// 
    /// Benefits:
    /// - Eliminates Instantiate/Destroy calls (expensive)
    /// - Reduces garbage collection spikes
    /// - Improves performance significantly
    /// 
    /// Usage:
    /// var pool = new ObjectPool<Pipe>(pipePrefab, 20, transform);
    /// var pipe = pool.Get();
    /// pool.Return(pipe);
    /// ============================================
    /// </summary>
    /// <typeparam name="T">Component type to pool</typeparam>
    public class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _availableObjects;
        private readonly List<T> _allObjects;

        public int TotalCount => _allObjects.Count;
        public int AvailableCount => _availableObjects.Count;
        public int ActiveCount => TotalCount - AvailableCount;

        /// <summary>
        /// Create a new object pool
        /// </summary>
        /// <param name="prefab">Prefab to instantiate</param>
        /// <param name="initialSize">Number of objects to pre-instantiate</param>
        /// <param name="parent">Parent transform for pooled objects</param>
        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _availableObjects = new Queue<T>(initialSize);
            _allObjects = new List<T>(initialSize);

            // Pre-instantiate objects
            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        /// <summary>
        /// Get an object from the pool
        /// </summary>
        public T Get()
        {
            T obj;

            if (_availableObjects.Count > 0)
            {
                // Reuse existing object
                obj = _availableObjects.Dequeue();
            }
            else
            {
                // Create new object if pool is empty
                obj = CreateNewObject();
                Debug.LogWarning($"[ObjectPool] Pool exhausted for {typeof(T).Name}, creating new instance. Consider increasing pool size.");
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Get an object at specific position and rotation
        /// </summary>
        public T Get(Vector3 position, Quaternion rotation)
        {
            T obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        /// <summary>
        /// Return an object to the pool
        /// </summary>
        public void Return(T obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("[ObjectPool] Attempting to return null object");
                return;
            }

            if (!_allObjects.Contains(obj))
            {
                Debug.LogWarning($"[ObjectPool] Attempting to return object that doesn't belong to this pool: {obj.name}");
                return;
            }

            obj.gameObject.SetActive(false);
            
            if (!_availableObjects.Contains(obj))
            {
                _availableObjects.Enqueue(obj);
            }
        }

        /// <summary>
        /// Return all active objects to the pool
        /// </summary>
        public void ReturnAll()
        {
            foreach (T obj in _allObjects)
            {
                if (obj.gameObject.activeSelf)
                {
                    Return(obj);
                }
            }
        }

        /// <summary>
        /// Destroy all pooled objects
        /// </summary>
        public void Clear()
        {
            foreach (T obj in _allObjects)
            {
                if (obj != null)
                {
                    Object.Destroy(obj.gameObject);
                }
            }

            _availableObjects.Clear();
            _allObjects.Clear();
        }

        /// <summary>
        /// Create a new object and add it to the pool
        /// </summary>
        private T CreateNewObject()
        {
            T newObj = Object.Instantiate(_prefab, _parent);
            newObj.gameObject.SetActive(false);
            
            _allObjects.Add(newObj);
            _availableObjects.Enqueue(newObj);
            
            return newObj;
        }
    }
}
