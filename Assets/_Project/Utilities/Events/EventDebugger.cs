using UnityEngine;

namespace FlappyBird.Utilities.Events
{
    /// <summary>
    /// ============================================
    /// EVENT DEBUGGER
    /// ============================================
    /// Utility component to debug events in the Inspector
    /// Add to any GameObject to log when specific events fire
    /// Useful for testing event flow during development
    /// ============================================
    /// </summary>
    public class EventDebugger : MonoBehaviour
    {
        [Header("Event to Debug")]
        [Tooltip("The event to monitor")]
        public GameEvent gameEvent;

        [Header("Debug Settings")]
        [Tooltip("Message to log when event fires")]
        public string debugMessage = "Event fired!";

        [Tooltip("Enable/disable logging")]
        public bool enableLogging = true;

        private GameEventListener _listener;

        private void Awake()
        {
            // Add GameEventListener component and configure it
            _listener = gameObject.AddComponent<GameEventListener>();
            _listener.hideFlags = HideFlags.HideInInspector;
        }

        private void OnEnable()
        {
            if (_listener != null && gameEvent != null)
            {
                _listener.gameEvent = gameEvent;
                _listener.response = new UnityEngine.Events.UnityEvent();
                _listener.response.AddListener(OnEventFired);
            }
        }

        private void OnDisable()
        {
            if (_listener != null && _listener.response != null)
            {
                _listener.response.RemoveListener(OnEventFired);
            }
        }

        private void OnEventFired()
        {
            // Debug logging removed
        }
    }
}
