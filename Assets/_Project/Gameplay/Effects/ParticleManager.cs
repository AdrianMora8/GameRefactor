using UnityEngine;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// PARTICLE MANAGER
    /// ============================================
    /// Manages all particle effects in the game
    /// 
    /// DESIGN PATTERN: Facade
    /// - Simplifies particle system access
    /// ============================================
    /// </summary>
    public class ParticleManager : MonoBehaviour
    {
        [Header("Death Effects")]
        [SerializeField] private ParticleSystem deathParticles;
        [SerializeField] private ParticleSystem featherParticles;
        
        [Header("Score Effects")]
        [SerializeField] private ParticleSystem scoreParticles;
        
        [Header("Flap Effects")]
        [SerializeField] private ParticleSystem flapParticles;

        private static ParticleManager _instance;
        public static ParticleManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        /// <summary>
        /// Play death particles at position
        /// </summary>
        public void PlayDeathEffect(Vector3 position)
        {
            if (deathParticles != null)
            {
                deathParticles.transform.position = position;
                deathParticles.Play();
            }

            if (featherParticles != null)
            {
                featherParticles.transform.position = position;
                featherParticles.Play();
            }
        }

        /// <summary>
        /// Play score particles at position
        /// </summary>
        public void PlayScoreEffect(Vector3 position)
        {
            if (scoreParticles != null)
            {
                scoreParticles.transform.position = position;
                scoreParticles.Play();
            }
        }

        /// <summary>
        /// Play flap particles at position
        /// </summary>
        public void PlayFlapEffect(Vector3 position)
        {
            if (flapParticles != null)
            {
                flapParticles.transform.position = position;
                flapParticles.Play();
            }
        }
    }
}
