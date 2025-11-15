using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Infrastructure.DI
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Service Locator + Singleton
    /// ============================================
    /// Purpose: Centralized service registry and access
    /// 
    /// Benefits:
    /// - Single point of access for all services
    /// - Decouples service consumers from implementations
    /// - Testable (can inject mock services)
    /// - Better than multiple static singletons
    /// 
    /// SOLID Principles:
    /// - Dependency Inversion: Depend on interfaces, not concrete classes
    /// - Single Responsibility: Only manages service registration/retrieval
    /// 
    /// Usage:
    /// // Register:
    /// ServiceLocator.Register<IAudioService>(new AudioManager());
    /// 
    /// // Get:
    /// var audio = ServiceLocator.Get<IAudioService>();
    /// audio.PlaySound("jump");
    /// ============================================
    /// </summary>
    public class ServiceLocator
    {
        #region Singleton Implementation

        private static ServiceLocator _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Thread-safe singleton instance
        /// </summary>
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceLocator();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Service Registry

        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Register a service implementation
        /// </summary>
        /// <typeparam name="T">Service interface type</typeparam>
        /// <param name="service">Service implementation instance</param>
        public static void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            
            if (Instance._services.ContainsKey(type))
            {
                Debug.LogWarning($"[ServiceLocator] Service {type.Name} is already registered. Overwriting...");
                Instance._services[type] = service;
            }
            else
            {
                Instance._services.Add(type, service);
                Debug.Log($"[ServiceLocator] Registered service: {type.Name}");
            }
        }

        /// <summary>
        /// Get a registered service
        /// </summary>
        /// <typeparam name="T">Service interface type</typeparam>
        /// <returns>Service instance or null if not found</returns>
        public static T Get<T>() where T : class
        {
            var type = typeof(T);
            
            if (Instance._services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            Debug.LogError($"[ServiceLocator] Service {type.Name} not found! Make sure it's registered in GameBootstrapper.");
            return null;
        }

        /// <summary>
        /// Check if a service is registered
        /// </summary>
        public static bool IsRegistered<T>() where T : class
        {
            return Instance._services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Unregister a service
        /// </summary>
        public static void Unregister<T>() where T : class
        {
            var type = typeof(T);
            
            if (Instance._services.ContainsKey(type))
            {
                Instance._services.Remove(type);
                Debug.Log($"[ServiceLocator] Unregistered service: {type.Name}");
            }
        }

        /// <summary>
        /// Clear all registered services (useful for testing)
        /// </summary>
        public static void Clear()
        {
            Instance._services.Clear();
            Debug.Log("[ServiceLocator] All services cleared");
        }

        #endregion

        #region Debug Utilities

        /// <summary>
        /// Log all registered services (for debugging)
        /// </summary>
        public static void LogRegisteredServices()
        {
            Debug.Log($"[ServiceLocator] Registered services count: {Instance._services.Count}");
            foreach (var kvp in Instance._services)
            {
                Debug.Log($"  - {kvp.Key.Name} → {kvp.Value.GetType().Name}");
            }
        }

        #endregion
    }
}
