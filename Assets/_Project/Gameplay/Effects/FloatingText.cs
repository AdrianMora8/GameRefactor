using System.Collections;
using UnityEngine;
using TMPro;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// FLOATING TEXT
    /// ============================================
    /// Creates floating "+1" text effect on score
    /// 
    /// Animates upward and fades out
    /// ============================================
    /// </summary>
    public class FloatingText : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float floatSpeed = 2f;
        [SerializeField] private float fadeDuration = 0.8f;
        [SerializeField] private float scaleStart = 0.5f;
        [SerializeField] private float scaleEnd = 1.2f;

        private TextMeshPro _textMesh;
        private Color _originalColor;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
            if (_textMesh != null)
            {
                _originalColor = _textMesh.color;
            }
        }

        /// <summary>
        /// Initialize and start animation
        /// </summary>
        public void Initialize(string text, Vector3 position)
        {
            if (_textMesh == null)
            {
                _textMesh = GetComponent<TextMeshPro>();
            }

            transform.position = position;
            _textMesh.text = text;
            _textMesh.color = _originalColor;
            transform.localScale = Vector3.one * scaleStart;

            gameObject.SetActive(true);
            StartCoroutine(AnimateAndDestroy());
        }

        private IEnumerator AnimateAndDestroy()
        {
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / fadeDuration;

                // Move upward
                transform.position += Vector3.up * floatSpeed * Time.deltaTime;

                // Scale up
                float scale = Mathf.Lerp(scaleStart, scaleEnd, t);
                transform.localScale = Vector3.one * scale;

                // Fade out
                Color color = _originalColor;
                color.a = Mathf.Lerp(1f, 0f, t);
                _textMesh.color = color;

                yield return null;
            }

            // Return to pool or destroy
            gameObject.SetActive(false);
        }
    }
}
