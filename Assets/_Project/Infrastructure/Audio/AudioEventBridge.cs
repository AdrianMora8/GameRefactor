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
        private bool _isSubscribed = false;

        private void Awake()
        {
            _audioManager = GetComponent<AudioManager>();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            if (_isSubscribed) return;
            
            if (onBirdFlap != null)
                onBirdFlap.RegisterListener(PlayFlapSound);
            
            if (onBirdCollision != null)
                onBirdCollision.RegisterListener(PlayHitSound);
            
            if (onScoreSound != null)
                onScoreSound.RegisterListener(PlayScoreSound);
            
            if (onGameStarted != null)
                onGameStarted.RegisterListener(PlayGameStartSound);
            
            if (onBirdDied != null)
                onBirdDied.RegisterListener(PlayGameOverSound);

            _isSubscribed = true;
        }

        private void UnsubscribeFromEvents()
        {
            if (!_isSubscribed) return;
            
            if (onBirdFlap != null)
                onBirdFlap.UnregisterListener(PlayFlapSound);
            
            if (onBirdCollision != null)
                onBirdCollision.UnregisterListener(PlayHitSound);
            
            if (onScoreSound != null)
                onScoreSound.UnregisterListener(PlayScoreSound);
            
            if (onGameStarted != null)
                onGameStarted.UnregisterListener(PlayGameStartSound);
            
            if (onBirdDied != null)
                onBirdDied.UnregisterListener(PlayGameOverSound);

            _isSubscribed = false;
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
    }
}
