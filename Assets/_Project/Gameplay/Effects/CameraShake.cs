using UnityEngine;

namespace FlappyBird.Gameplay.Effects
{
    /// <summary>
    /// ============================================
    /// CAMERA SHAKE: Visual Feedback
    /// ============================================
    /// Adds screen shake for impactful events
    /// 
    /// USAGE:
    /// - Death collision
    /// - Hard impacts
    /// - Score milestones (optional)
    /// ============================================
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        [Header("Shake Settings")]
        [SerializeField] private float shakeDuration = 0.15f;
        [SerializeField] private float shakeMagnitude = 0.1f;
        [SerializeField] private float dampingSpeed = 1.0f;

        private static CameraShake _instance;
        public static CameraShake Instance => _instance;

        private Transform _cameraTransform;
        private Vector3 _initialPosition;
        private float _shakeTimer = 0f;

        private void Awake()
        {
            // Singleton setup
            if (_instance != null && _instance != this)
            {
                Destroy(this);
                return;
            }
            _instance = this;

            _cameraTransform = transform;
            _initialPosition = _cameraTransform.localPosition;
        }

        private void Update()
        {
            if (_shakeTimer > 0)
            {
                _cameraTransform.localPosition = _initialPosition + Random.insideUnitSphere * shakeMagnitude;
                _shakeTimer -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                _shakeTimer = 0f;
                _cameraTransform.localPosition = _initialPosition;
            }
        }

        /// <summary>
        /// Trigger camera shake
        /// </summary>
        public void Shake()
        {
            _shakeTimer = shakeDuration;
        }

        /// <summary>
        /// Trigger camera shake with custom parameters
        /// </summary>
        public void Shake(float duration, float magnitude)
        {
            shakeDuration = duration;
            shakeMagnitude = magnitude;
            _shakeTimer = duration;
        }

        /// <summary>
        /// Stop shake immediately
        /// </summary>
        public void StopShake()
        {
            _shakeTimer = 0f;
            _cameraTransform.localPosition = _initialPosition;
        }
    }
}
