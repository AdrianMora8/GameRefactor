using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Utilities.Events
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Observer Pattern (with data)
    /// ============================================
    /// GameEvent variant that passes an integer parameter
    /// Useful for score updates, combo counters, etc.
    /// 
    /// Supports both component-based and action-based subscriptions
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "IntGameEvent", menuName = "Flappy Bird/Events/Int Game Event")]
    public class IntGameEvent : ScriptableObject
    {
        private readonly List<IntGameEventListener> _listeners = new List<IntGameEventListener>();
        
        /// <summary>
        /// Direct action callbacks (for code-based subscriptions)
        /// </summary>
        private event Action<int> _onEventRaised;

        /// <summary>
        /// Raise the event with an integer value
        /// </summary>
        public void Raise(int value)
        {
            // Notify component-based listeners
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
            
            // Notify action-based listeners
            _onEventRaised?.Invoke(value);
        }

        /// <summary>
        /// Register a listener component
        /// </summary>
        public void RegisterListener(IntGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        /// <summary>
        /// Unregister a listener component
        /// </summary>
        public void UnregisterListener(IntGameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Register an action callback to this event
        /// </summary>
        public void RegisterListener(Action<int> callback)
        {
            _onEventRaised += callback;
        }

        /// <summary>
        /// Unregister an action callback from this event
        /// </summary>
        public void UnregisterListener(Action<int> callback)
        {
            _onEventRaised -= callback;
        }
    }
}
