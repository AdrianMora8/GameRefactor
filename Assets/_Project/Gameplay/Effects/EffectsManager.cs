using UnityEngine;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// EFFECTS MANAGER
    /// ============================================
    /// Central coordinator for all visual effects
    /// Listens to game events and triggers effects
    /// 
    /// DESIGN PATTERN: Facade + Observer
    /// ============================================
    /// </summary>
    public class EffectsManager : MonoBehaviour
    {
        [Header("Event Listeners")]
        [SerializeField] private GameEvent onBirdDied;
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onBirdFlapped;

        [Header("Effect Settings")]
        [SerializeField] private bool enableCameraShake = true;
        [SerializeField] private bool enableScreenFlash = true;
        [SerializeField] private bool enableParticles = true;
        [SerializeField] private bool enableFloatingText = true;

        [Header("References")]
        [SerializeField] private Transform birdTransform;

        private bool _isSubscribed = false;

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void Start()
        {
            // Find bird if not assigned
            if (birdTransform == null)
            {
                var bird = FindObjectOfType<Bird.BirdController>();
                if (bird != null)
                {
                    birdTransform = bird.transform;
                }
            }
        }

        private void SubscribeToEvents()
        {
            if (_isSubscribed) return;

            if (onBirdDied != null)
                onBirdDied.RegisterListener(OnBirdDied);

            if (onScoreChanged != null)
                onScoreChanged.RegisterListener(OnScoreChangedInt);

            if (onBirdFlapped != null)
                onBirdFlapped.RegisterListener(OnBirdFlapped);

            _isSubscribed = true;
        }

        private void UnsubscribeFromEvents()
        {
            if (!_isSubscribed) return;

            if (onBirdDied != null)
                onBirdDied.UnregisterListener(OnBirdDied);

            if (onScoreChanged != null)
                onScoreChanged.UnregisterListener(OnScoreChangedInt);

            if (onBirdFlapped != null)
                onBirdFlapped.UnregisterListener(OnBirdFlapped);

            _isSubscribed = false;
        }

        private void OnBirdDied()
        {
            Vector3 position = birdTransform != null ? birdTransform.position : Vector3.zero;

            // Camera shake
            if (enableCameraShake && CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.4f, 0.3f);
            }

            // Screen flash
            if (enableScreenFlash && ScreenFlash.Instance != null)
            {
                ScreenFlash.Instance.FlashDeath();
            }

            // Death particles
            if (enableParticles && ParticleManager.Instance != null)
            {
                ParticleManager.Instance.PlayDeathEffect(position);
            }
        }

        private void OnScoreChangedInt(int score)
        {
            OnScoreChanged();
        }

        private void OnScoreChanged()
        {
            Vector3 position = birdTransform != null ? birdTransform.position : Vector3.zero;

            // Floating "+1" text
            if (enableFloatingText && FloatingTextSpawner.Instance != null)
            {
                FloatingTextSpawner.Instance.SpawnScoreText(position);
            }

            // Score particles
            if (enableParticles && ParticleManager.Instance != null)
            {
                ParticleManager.Instance.PlayScoreEffect(position);
            }
        }

        private void OnBirdFlapped()
        {
            // Optional: small flap particles (disabled by default for performance)
            // if (enableParticles && ParticleManager.Instance != null)
            // {
            //     Vector3 position = birdTransform != null ? birdTransform.position : Vector3.zero;
            //     ParticleManager.Instance.PlayFlapEffect(position);
            // }
        }
    }
}
