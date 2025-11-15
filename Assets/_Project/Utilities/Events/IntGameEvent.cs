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
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "IntGameEvent", menuName = "Flappy Bird/Events/Int Game Event")]
    public class IntGameEvent : ScriptableObject
    {
        private readonly List<IntGameEventListener> _listeners = new List<IntGameEventListener>();

        /// <summary>
        /// Raise the event with an integer value
        /// </summary>
        public void Raise(int value)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(IntGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void UnregisterListener(IntGameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
