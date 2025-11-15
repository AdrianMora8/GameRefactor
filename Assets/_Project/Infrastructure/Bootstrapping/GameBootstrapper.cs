using UnityEngine;
using FlappyBird.Core.Interfaces;
using FlappyBird.Infrastructure.DI;
using FlappyBird.Infrastructure.Audio;
using FlappyBird.Infrastructure.Input;
using FlappyBird.Infrastructure.Save;
using FlappyBird.Infrastructure.Pooling;
using FlappyBird.Configuration;

namespace FlappyBird.Infrastructure.Bootstrapping
{
    /// <summary>
    /// ============================================
    /// BOOTSTRAPPER: Game Initialization
    /// ============================================
    /// Initializes all services and systems
    /// This runs FIRST before anything else
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles service initialization
    /// ============================================
    /// </summary>
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private PoolConfig poolConfig;

        [Header("Scene References")]
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private PoolManager poolManager;
        [SerializeField] private InputManager inputManager;

        private void Awake()
        {
            Debug.Log("[GameBootstrapper] Initializing game systems...");

            // Auto-find managers if not assigned
            if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();
            if (poolManager == null) poolManager = FindObjectOfType<PoolManager>();
            if (inputManager == null) inputManager = FindObjectOfType<InputManager>();

            // Validate configs
            ValidateConfigurations();

            // Initialize services
            InitializeServices();

            Debug.Log("[GameBootstrapper] Game systems initialized successfully!");
        }

        private void ValidateConfigurations()
        {
            if (gameConfig == null)
            {
                Debug.LogError("[GameBootstrapper] GameConfig not assigned!");
            }

            if (audioConfig == null)
            {
                Debug.LogError("[GameBootstrapper] AudioConfig not assigned!");
            }

            if (poolConfig == null)
            {
                Debug.LogError("[GameBootstrapper] PoolConfig not assigned!");
            }
        }

        private void InitializeServices()
        {
            // 1. Input Service (must be first - InputManager registers itself)
            InitializeInputService();

            // 2. Audio Service
            InitializeAudioService();

            // 3. Save Service
            InitializeSaveService();

            // 4. Pool Service
            InitializePoolService();

            Debug.Log("[GameBootstrapper] All services registered in ServiceLocator");
        }

        private void InitializeAudioService()
        {
            if (audioManager == null)
            {
                // Look for existing AudioManager in scene
                audioManager = FindObjectOfType<AudioManager>();
                
                if (audioManager == null)
                {
                    // Create AudioManager
                    GameObject audioGO = new GameObject("AudioManager");
                    audioManager = audioGO.AddComponent<AudioManager>();
                    Debug.Log("[GameBootstrapper] Created AudioManager");
                }
            }

            // Wait for AudioManager to initialize in its Awake
            // We'll register it after Start
            ServiceLocator.Register<IAudioService>(audioManager);
            Debug.Log("[GameBootstrapper] ✓ AudioService registered");
        }

        private void InitializeInputService()
        {
            if (inputManager == null)
            {
                // Look for existing InputManager in scene
                inputManager = FindObjectOfType<InputManager>();
                
                if (inputManager == null)
                {
                    // Create InputManager
                    GameObject inputGO = new GameObject("InputManager");
                    inputManager = inputGO.AddComponent<InputManager>();
                    Debug.Log("[GameBootstrapper] Created InputManager");
                }
            }

            // InputManager registers itself in its Awake()
            // Just log that it's ready
            Debug.Log("[GameBootstrapper] ✓ InputService will be registered by InputManager");
        }

        private void InitializeSaveService()
        {
            ISaveService saveService = new SaveService();
            ServiceLocator.Register<ISaveService>(saveService);
            Debug.Log("[GameBootstrapper] ✓ SaveService registered");
        }

        private void InitializePoolService()
        {
            if (poolManager == null)
            {
                // Look for existing PoolManager in scene
                poolManager = FindObjectOfType<PoolManager>();
                
                if (poolManager == null)
                {
                    // Create PoolManager
                    GameObject poolGO = new GameObject("PoolManager");
                    poolManager = poolGO.AddComponent<PoolManager>();
                    Debug.Log("[GameBootstrapper] Created PoolManager");
                }
            }

            ServiceLocator.Register<IPoolService>(poolManager);
            Debug.Log("[GameBootstrapper] ✓ PoolService registered");
        }
    }
}
