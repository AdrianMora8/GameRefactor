using System.Collections;
using UnityEngine;
using FlappyBird.Core.Entities;
using FlappyBird.Core.Interfaces;
using FlappyBird.Configuration;
using FlappyBird.Infrastructure.DI;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Gameplay.Bird
{
    /// <summary>
    /// ============================================
    /// CONTROLLER: Bird Logic
    /// ============================================
    /// Handles bird behavior and physics
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles bird logic
    /// 
    /// SOLID: Dependency Inversion
    /// - Depends on interfaces (IAudioService, IInputService)
    /// 
    /// Separation of Concerns:
    /// - Controller = Logic
    /// - View = Visuals
    /// ============================================
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BirdView))]
    public class BirdController : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private BirdConfig birdConfig;

        [Header("Events")]
        [SerializeField] private GameEvent onBirdDied;
        [SerializeField] private GameEvent onBirdFlapped;

        [Header("Audio")]
        [SerializeField] private string flapSoundKey = "flap";
        [SerializeField] private string hitSoundKey = "hit";

        // Services
        private IAudioService _audioService;
        private IInputService _inputService;

        // Components
        private Rigidbody2D _rigidbody;
        private BirdView _view;

        // State
        private Core.Entities.Bird _birdEntity;
        private bool _isAutoFlapping;
        private bool _canFlap = true;
        private Coroutine _autoFlapCoroutine;

        public bool IsDead => _birdEntity?.IsDead ?? false;
        public Vector3 StartPosition { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _view = GetComponent<BirdView>();
            StartPosition = transform.localPosition;

            // Create entity
            _birdEntity = new Core.Entities.Bird();

            // Validate config
            if (birdConfig == null)
            {
            }
        }

        private IEnumerator Start()
        {
            // Validate view
            if (_view == null)
            {
                yield break;
            }

            // Wait 2 frames for all services to register
            yield return null;
            yield return null;

            // Try to get services (optional - won't break if not available)
            _audioService = ServiceLocator.Get<IAudioService>();
            _inputService = ServiceLocator.Get<IInputService>();

            // Don't log warnings, just use them if available

            // Configure
            if (birdConfig != null)
            {
                _view.Configure(birdConfig);
                ConfigurePhysics();
            }

            // Start in idle state
            ResetBird();
        }

        private void Update()
        {
            // Visual rotation based on velocity
            if (_view != null && _birdEntity != null && !_birdEntity.IsDead)
            {
                UpdateRotation();
            }
        }

        /// <summary>
        /// Smooth rotation based on velocity
        /// </summary>
        private void UpdateRotation()
        {
            if (_rigidbody == null) return;

            float velocity = _rigidbody.velocity.y;
            float targetRotation;

            if (velocity > 0)
            {
                // Going up - rotate upward (max 30 degrees)
                targetRotation = Mathf.Lerp(0, 30f, Mathf.Clamp01(velocity / birdConfig.jumpForce));
            }
            else
            {
                // Falling down - rotate downward (max -90 degrees)
                targetRotation = Mathf.Lerp(0, -90f, Mathf.Clamp01(-velocity / 10f));
            }

            _view.SetRotation(targetRotation);
        }

        private void ConfigurePhysics()
        {
            _rigidbody.gravityScale = birdConfig.gravityScale;
        }

        /// <summary>
        /// Start auto-flap (for menu state)
        /// </summary>
        public void StartAutoFlap()
        {
            // Stop any existing auto-flap coroutine first
            StopAutoFlap();
            
            _isAutoFlapping = true;
            _autoFlapCoroutine = StartCoroutine(AutoFlapRoutine());
        }

        /// <summary>
        /// Stop auto-flap (when game starts)
        /// </summary>
        public void StopAutoFlap()
        {
            _isAutoFlapping = false;
            
            // Explicitly stop the coroutine to prevent sound loops
            if (_autoFlapCoroutine != null)
            {
                StopCoroutine(_autoFlapCoroutine);
                _autoFlapCoroutine = null;
            }
        }

        /// <summary>
        /// Enable/disable flap ability
        /// </summary>
        public void SetCanFlap(bool canFlap)
        {
            _canFlap = canFlap;
        }

        /// <summary>
        /// Handle flap input (called by GameFlowManager or InputManager)
        /// </summary>
        public void TryFlap()
        {
            if (_canFlap && !_birdEntity.IsDead)
            {
                Flap();
            }
        }

        /// <summary>
        /// Execute flap
        /// </summary>
        private void Flap()
        {
            FlapInternal(playSound: true);
        }

        /// <summary>
        /// Execute silent flap (for auto-flap in menu)
        /// </summary>
        private void SilentFlap()
        {
            FlapInternal(playSound: false);
        }

        /// <summary>
        /// Internal flap logic
        /// </summary>
        private void FlapInternal(bool playSound)
        {
            if (birdConfig == null) return;

            // Physics
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * birdConfig.jumpForce, ForceMode2D.Force);

            // Visual
            if (_view != null)
            {
                _view.RotateUpward();
                _view.PlayFlapAnimation();
            }

            // Event (AudioEventBridge handles the sound) - only if playSound is true
            if (playSound)
            {
                onBirdFlapped?.Raise();
            }
        }

        /// <summary>
        /// Auto-flap coroutine (for menu idle animation)
        /// </summary>
        private IEnumerator AutoFlapRoutine()
        {
            while (_isAutoFlapping)
            {
                if (transform.position.y <= 0)
                {
                    SilentFlap(); // Use silent flap - no sound in menu
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        /// <summary>
        /// Enable/disable physics simulation
        /// </summary>
        public void SetPhysicsEnabled(bool enabled)
        {
            _rigidbody.simulated = enabled;
        }

        /// <summary>
        /// Kill the bird
        /// </summary>
        public void Die()
        {
            if (_birdEntity == null || _birdEntity.IsDead)
                return;

            _birdEntity.Die();

            // Stop physics
            _rigidbody.velocity = Vector2.zero;
            SetPhysicsEnabled(false);

            // Visual
            if (_view != null)
            {
                _view.PlayDeathAnimation();
            }

            // Event (AudioEventBridge handles the sound)
            onBirdDied?.Raise();
        }

        /// <summary>
        /// Reset bird to initial state
        /// </summary>
        public void ResetBird()
        {
            // Stop any running auto-flap coroutine FIRST
            StopAutoFlap();
            
            // Reset entity
            _birdEntity = new Core.Entities.Bird();

            // Reset position
            transform.localPosition = StartPosition;

            // Reset physics
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            SetPhysicsEnabled(false);

            // Reset visuals
            if (_view != null)
            {
                _view.ResetVisuals();
            }
        }

        #region Collision Detection

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Si ya está muerto, no procesar más colisiones
            if (_birdEntity != null && _birdEntity.IsDead)
                return;

            // Detectar muerte SOLO por colisión SÓLIDA (no triggers)
            if (collision.gameObject.CompareTag("Pipe"))
            {
                Die();
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Los triggers NO causan muerte (ScoreZone usa triggers para puntuación)
            // Solo las colisiones físicas (OnCollisionEnter2D) pueden matar al bird
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmosSelected()
        {
            if (birdConfig == null) return;

            // Draw flap force
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * (birdConfig.jumpForce / 50f));
        }

        #endregion
    }
}
