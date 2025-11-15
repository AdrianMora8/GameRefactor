using UnityEngine;
using FlappyBird.Infrastructure.Audio;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Utilities
{
    /// <summary>
    /// ============================================
    /// AUDIO EVENT BRIDGE
    /// ============================================
    /// Connects GameEvents to AudioManager
    /// Allows playing sounds via events without code coupling
    /// 
    /// Usage:
    /// 1. Add this component to AudioManager GameObject
    /// 2. Assign events from EventRegistry
    /// 3. Events will automatically trigger sounds
    /// 
    /// Example:
    /// - OnBirdFlap event → plays "flap" sound
    /// - OnBirdCollision event → plays "hit" sound
    /// ============================================
    /// </summary>
    [RequireComponent(typeof(AudioManager))]
    public class AudioEventBridge : MonoBehaviour
    {
        [Header("Event Listeners")]
        [SerializeField] private GameEvent onBirdFlap;
        [SerializeField] private GameEvent onBirdCollision;
        [SerializeField] private GameEvent onScoreSound;
        [SerializeField] private GameEvent onGameStarted;
        [SerializeField] private GameEvent onBirdDied;

        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = GetComponent<AudioManager>();
        }

        private void OnEnable()
        {
            // Subscribe to events
            if (onBirdFlap != null)
                onBirdFlap.RegisterListener(CreateListener(PlayFlapSound));
            
            if (onBirdCollision != null)
                onBirdCollision.RegisterListener(CreateListener(PlayHitSound));
            
            if (onScoreSound != null)
                onScoreSound.RegisterListener(CreateListener(PlayScoreSound));
            
            if (onGameStarted != null)
                onGameStarted.RegisterListener(CreateListener(PlayGameStartSound));
            
            if (onBirdDied != null)
                onBirdDied.RegisterListener(CreateListener(PlayGameOverSound));
        }

        private void OnDisable()
        {
            // Unsubscribe from events
            if (onBirdFlap != null)
                onBirdFlap.UnregisterListener(GetListener(PlayFlapSound));
            
            if (onBirdCollision != null)
                onBirdCollision.UnregisterListener(GetListener(PlayHitSound));
            
            if (onScoreSound != null)
                onScoreSound.UnregisterListener(GetListener(PlayScoreSound));
            
            if (onGameStarted != null)
                onGameStarted.UnregisterListener(GetListener(PlayGameStartSound));
            
            if (onBirdDied != null)
                onBirdDied.UnregisterListener(GetListener(PlayGameOverSound));
        }

        #region Sound Methods

        private void PlayFlapSound()
        {
            _audioManager?.PlaySFX("flap");
        }

        private void PlayHitSound()
        {
            _audioManager?.PlaySFX("hit");
        }

        private void PlayScoreSound()
        {
            _audioManager?.PlaySFX("score");
        }

        private void PlayGameStartSound()
        {
            _audioManager?.PlaySFX("gamestart");
        }

        private void PlayGameOverSound()
        {
            _audioManager?.PlaySFX("gameover");
        }

        #endregion

        #region Helper Methods (Workaround for event system)

        private GameEventListener CreateListener(System.Action action)
        {
            GameObject listenerObj = new GameObject($"Listener_{action.Method.Name}");
            listenerObj.transform.SetParent(transform);
            listenerObj.hideFlags = HideFlags.HideInHierarchy;

            GameEventListener listener = listenerObj.AddComponent<GameEventListener>();
            listener.response = new UnityEngine.Events.UnityEvent();
            listener.response.AddListener(() => action());

            return listener;
        }

        private GameEventListener GetListener(System.Action action)
        {
            // This is a simplified version - in production you'd store references
            return null;
        }

        #endregion
    }
}
