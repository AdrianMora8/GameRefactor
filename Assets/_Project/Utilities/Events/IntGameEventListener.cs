using UnityEngine;

namespace FlappyBird.Utilities.Events
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Observer Pattern (Listener with data)
    /// ============================================
    /// Listener for IntGameEvent
    /// ============================================
    /// </summary>
    public class IntGameEventListener : MonoBehaviour
    {
        [Header("Event Configuration")]
        [Tooltip("The IntGameEvent to listen to")]
        public IntGameEvent gameEvent;

        [Header("Response")]
        [Tooltip("Unity Event with int parameter")]
        public IntUnityEvent response;

        private void OnEnable()
        {
            if (gameEvent != null)
            {
                gameEvent.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            if (gameEvent != null)
            {
                gameEvent.UnregisterListener(this);
            }
        }

        public void OnEventRaised(int value)
        {
            response?.Invoke(value);
        }
    }

    /// <summary>
    /// UnityEvent with int parameter
    /// Needed for Inspector serialization
    /// </summary>
    [System.Serializable]
    public class IntUnityEvent : UnityEngine.Events.UnityEvent<int> { }
}
