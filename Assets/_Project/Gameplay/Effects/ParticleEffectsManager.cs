using UnityEngine;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// PARTICLE EFFECTS MANAGER
    /// ============================================
    /// Manages particle effects for game events
    /// 
    /// EFFECTS:
    /// - Death explosion
    /// - Score celebration
    /// - Feather trail (optional)
    /// ============================================
    /// </summary>
    public class ParticleEffectsManager : MonoBehaviour
    {
        [Header("Death Effects")]
        [SerializeField] private ParticleSystem deathExplosion;
        [SerializeField] private ParticleSystem featherBurst;

        [Header("Score Effects")]
        [SerializeField] private ParticleSystem scoreSparkle;
        [SerializeField] private ParticleSystem scoreStar;

        /// <summary>
        /// Play death explosion at position
        /// </summary>
        public void PlayDeathEffect(Vector3 position)
        {
            if (deathExplosion != null)
            {
                deathExplosion.transform.position = position;
                deathExplosion.Play();
            }

            if (featherBurst != null)
            {
                featherBurst.transform.position = position;
                featherBurst.Play();
            }
        }

        /// <summary>
        /// Play score celebration effect at position
        /// </summary>
        public void PlayScoreEffect(Vector3 position)
        {
            if (scoreSparkle != null)
            {
                scoreSparkle.transform.position = position;
                scoreSparkle.Play();
            }

            if (scoreStar != null)
            {
                scoreStar.transform.position = position;
                scoreStar.Play();
            }
        }

        /// <summary>
        /// Stop all effects
        /// </summary>
        public void StopAllEffects()
        {
            if (deathExplosion != null && deathExplosion.isPlaying)
                deathExplosion.Stop();

            if (featherBurst != null && featherBurst.isPlaying)
                featherBurst.Stop();

            if (scoreSparkle != null && scoreSparkle.isPlaying)
                scoreSparkle.Stop();

            if (scoreStar != null && scoreStar.isPlaying)
                scoreStar.Stop();
        }
    }
}
