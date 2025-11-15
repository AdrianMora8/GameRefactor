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
    /// - Subscribers: Add GameEventListener component
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
        /// Raise the event, notifying all listeners
        /// </summary>
        public void Raise()
        {
            // Iterate backwards in case listeners are removed during iteration
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        /// <summary>
        /// Register a listener to this event
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
    }
}
