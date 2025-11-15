using UnityEngine;
using UnityEngine.Events;

namespace FlappyBird.Utilities.Events
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Observer Pattern (Listener)
    /// ============================================
    /// MonoBehaviour component that listens to GameEvents
    /// 
    /// Usage:
    /// 1. Add this component to any GameObject
    /// 2. Assign a GameEvent asset
    /// 3. Configure UnityEvent response in Inspector
    /// 
    /// Example:
    /// - Event: OnBirdDied
    /// - Response: GameOverPresenter.Show()
    /// ============================================
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        [Header("Event Configuration")]
        [Tooltip("The GameEvent to listen to")]
        public GameEvent gameEvent;

        [Header("Response")]
        [Tooltip("Actions to perform when event is raised")]
        public UnityEvent response;

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

        /// <summary>
        /// Called by GameEvent when it's raised
        /// </summary>
        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}
