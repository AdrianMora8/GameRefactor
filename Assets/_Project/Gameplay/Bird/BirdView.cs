using UnityEngine;
using FlappyBird.Configuration;

namespace FlappyBird.Gameplay.Bird
{
    /// <summary>
    /// ============================================
    /// VIEW: Bird Visual Representation
    /// ============================================
    /// Handles only visual aspects:
    /// - Rotation animation
    /// - Sprite animation
    /// - Particle effects
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles visuals, no game logic
    /// ============================================
    /// </summary>
    public class BirdView : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float upwardRotationAngle = 20f;
        [SerializeField] private float downwardRotationAngle = -90f;

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem flapParticles;
        [SerializeField] private ParticleSystem deathParticles;

        private Animator _animator;
        private Quaternion _upwardRotation;
        private Quaternion _downwardRotation;

        private static readonly int FlapAnimParam = Animator.StringToHash("Flap");
        private static readonly int DieAnimParam = Animator.StringToHash("Die");

        private void Awake()
        {
            // Try to get Animator if it exists
            _animator = GetComponent<Animator>();
            
            if (_animator == null)
            {
                Debug.LogWarning("[BirdView] No Animator found. Animations will be skipped.");
            }
            
            _upwardRotation = Quaternion.Euler(0, 0, upwardRotationAngle);
            _downwardRotation = Quaternion.Euler(0, 0, downwardRotationAngle);
        }

        /// <summary>
        /// Rotate bird upward (called when flapping)
        /// </summary>
        public void RotateUpward()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _upwardRotation, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Rotate bird downward (gravity effect)
        /// </summary>
        public void RotateDownward()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _downwardRotation, rotationSpeed * Time.deltaTime * 0.1f);
        }

        /// <summary>
        /// Set rotation directly (smooth transition)
        /// </summary>
        public void SetRotation(float angle)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Play flap animation
        /// </summary>
        public void PlayFlapAnimation()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(FlapAnimParam);
            }

            if (flapParticles != null && !flapParticles.isPlaying)
            {
                flapParticles.Play();
            }
        }

        /// <summary>
        /// Play death animation
        /// </summary>
        public void PlayDeathAnimation()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(DieAnimParam);
            }

            if (deathParticles != null)
            {
                deathParticles.Play();
            }
        }

        /// <summary>
        /// Reset visual state
        /// </summary>
        public void ResetVisuals()
        {
            transform.rotation = Quaternion.identity;
            
            if (flapParticles != null && flapParticles.isPlaying)
            {
                flapParticles.Stop();
            }

            if (deathParticles != null && deathParticles.isPlaying)
            {
                deathParticles.Stop();
            }
        }

        /// <summary>
        /// Configure from BirdConfig ScriptableObject
        /// </summary>
        public void Configure(BirdConfig config)
        {
            if (config == null) return;

            rotationSpeed = config.rotationSpeed;
            upwardRotationAngle = config.forwardRotationAngle;
            downwardRotationAngle = config.fallingRotationAngle;

            _upwardRotation = Quaternion.Euler(0, 0, upwardRotationAngle);
            _downwardRotation = Quaternion.Euler(0, 0, downwardRotationAngle);
        }
    }
}
