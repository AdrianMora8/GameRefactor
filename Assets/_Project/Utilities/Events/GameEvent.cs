using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Utilities.Events
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Observer Pattern
    /// ============================================
    /// Purpose: Decouple event publishers from subscribers
    /// Benefits:
    /// - No direct dependencies between systems
    /// - Easy to add new listeners without modifying publishers
    /// - Follows Open/Closed Principle
    /// - Testable and maintainable
    /// 
    /// Usage:
    /// - Publishers call: gameEvent.Raise()
    /// - Subscribers: Add GameEventListener component OR use RegisterListener(Action)
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Flappy Bird/Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// List of all listeners subscribed to this event
        /// </summary>
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();
        
        /// <summary>
        /// Direct action callbacks (for code-based subscriptions)
        /// </summary>
        private event Action _onEventRaised;

        /// <summary>
        /// Raise the event, notifying all listeners
        /// </summary>
        public void Raise()
        {
            // Notify component-based listeners
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
            
            // Notify action-based listeners
            _onEventRaised?.Invoke();
        }

        /// <summary>
        /// Register a listener component to this event
        /// </summary>
        public void RegisterListener(GameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        /// <summary>
        /// Unregister a listener from this event
        /// </summary>
        public void UnregisterListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Register an action callback to this event
        /// </summary>
        public void RegisterListener(Action callback)
        {
            _onEventRaised += callback;
        }

        /// <summary>
        /// Unregister an action callback from this event
        /// </summary>
        public void UnregisterListener(Action callback)
        {
            _onEventRaised -= callback;
        }
    }
}
