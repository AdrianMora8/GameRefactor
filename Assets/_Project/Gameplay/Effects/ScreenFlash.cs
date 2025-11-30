using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// SCREEN FLASH
    /// ============================================
    /// Creates a white/red flash effect on death
    /// 
    /// Requires a UI Image covering the screen
    /// ============================================
    /// </summary>
    public class ScreenFlash : MonoBehaviour
    {
        [Header("Flash Settings")]
        [SerializeField] private Image flashImage;
        [SerializeField] private Color deathFlashColor = new Color(1f, 0f, 0f, 0.5f);
        [SerializeField] private Color scoreFlashColor = new Color(1f, 1f, 1f, 0.3f);
        [SerializeField] private float flashDuration = 0.15f;

        private static ScreenFlash _instance;
        public static ScreenFlash Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;

            // Ensure flash image starts transparent
            if (flashImage != null)
            {
                Color c = flashImage.color;
                c.a = 0f;
                flashImage.color = c;
            }
        }

        /// <summary>
        /// Flash red on death
        /// </summary>
        public void FlashDeath()
        {
            if (flashImage != null)
            {
                StartCoroutine(FlashCoroutine(deathFlashColor));
            }
        }

        /// <summary>
        /// Flash white on score
        /// </summary>
        public void FlashScore()
        {
            if (flashImage != null)
            {
                StartCoroutine(FlashCoroutine(scoreFlashColor));
            }
        }

        /// <summary>
        /// Custom flash
        /// </summary>
        public void Flash(Color color, float duration)
        {
            if (flashImage != null)
            {
                StartCoroutine(FlashCoroutine(color, duration));
            }
        }

        private IEnumerator FlashCoroutine(Color color)
        {
            yield return FlashCoroutine(color, flashDuration);
        }

        private IEnumerator FlashCoroutine(Color color, float duration)
        {
            // Set color
            flashImage.color = color;

            // Fade out
            float elapsed = 0f;
            Color startColor = color;
            Color endColor = new Color(color.r, color.g, color.b, 0f);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                flashImage.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }

            flashImage.color = endColor;
        }
    }
}
