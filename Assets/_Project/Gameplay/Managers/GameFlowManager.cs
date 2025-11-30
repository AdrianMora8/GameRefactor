using UnityEngine;
using UnityEngine.EventSystems;
using FlappyBird.Core.Entities;
using FlappyBird.Core.Interfaces;
using FlappyBird.Core.Services;
using FlappyBird.Gameplay.StateMachine;
using FlappyBird.Gameplay.Bird;
using FlappyBird.Infrastructure.DI;
using FlappyBird.Infrastructure.Services;
using FlappyBird.Utilities.Events;
using FlappyBird.Configuration;

namespace FlappyBird.Gameplay.Managers
{
    /// <summary>
    /// ============================================
    /// GAME FLOW MANAGER - Game Orchestrator
    /// ============================================
    /// The brain of the game - coordinates all systems
    /// 
    /// DESIGN PATTERN: Facade + Mediator
    /// - Simplifies interaction between complex subsystems
    /// - Centralizes game flow control
    /// 
    /// RESPONSIBILITIES:
    /// - Manages state machine transitions
    /// - Handles player input for game control
    /// - Coordinates Bird, Score, Audio, UI
    /// - Replaces old Game_Manager completely
    /// ============================================
    /// </summary>
    public class GameFlowManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private GameConfig gameConfig;

        [Header("Game Objects")]
        [SerializeField] private BirdController birdController;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Environment.PipeSpawner pipeSpawner;
        [SerializeField] private Environment.BackgroundScroller backgroundScroller;
        [SerializeField] private Environment.GroundScroller groundScroller;

        [Header("Events")]
        [SerializeField] private GameEvent onGameStarted;
        [SerializeField] private GameEvent onGameOver;
        [SerializeField] private GameEvent onPauseToggled;
        [SerializeField] private IntGameEvent onScoreChanged;

        // Services
        private IInputService _inputService;
        private IAudioService _audioService;
        private ScoreService _scoreService;

        // State Machine
        private GameStateMachine _stateMachine;

        // Game State
        private bool _isInitialized = false;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Get services
            _inputService = ServiceLocator.Get<IInputService>();
            _audioService = ServiceLocator.Get<IAudioService>();
            _scoreService = new ScoreService(
                ServiceLocator.Get<ISaveService>(),
                onScoreChanged
            );

            // Validate dependencies
            if (!ValidateDependencies())
            {
                return;
            }

            // Initialize state machine
            InitializeStateMachine();

            // Subscribe to events
            SubscribeToEvents();

            // Start in menu state
            _stateMachine.Initialize(GameState.Menu);
            StartMenuState();

            _isInitialized = true;
        }

        private bool ValidateDependencies()
        {
            bool isValid = true;

            if (birdController == null)
            {
                isValid = false;
            }

            if (gameConfig == null)
            {
                isValid = false;
            }

            if (_inputService == null)
            {
                isValid = false;
            }

            return isValid;
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new GameStateMachine();

            // Register states
            _stateMachine.RegisterState(new MenuState(onGameStarted));
            _stateMachine.RegisterState(new PlayingState());
            _stateMachine.RegisterState(new PausedState(onPauseToggled));
            _stateMachine.RegisterState(new GameOverState(onGameOver, null));

            // Subscribe to state changes
            _stateMachine.OnStateChanged += OnStateChanged;
        }

        private void SubscribeToEvents()
        {
            // Bird events handled by BirdController directly via events
            // We just listen to state-relevant events here
        }

        private void Update()
        {
            if (!_isInitialized) return;

            // Update current state
            _stateMachine.Update();

            // Handle input based on current state
            HandleInput();
        }

        private void HandleInput()
        {
            if (_inputService == null) return;

            // IMPORTANT: Ignore input if clicking on UI elements
            if (IsPointerOverUI()) return;

            GameState currentState = _stateMachine.CurrentStateType;

            switch (currentState)
            {
                case GameState.Menu:
                    if (_inputService.GetJumpInput())
                    {
                        StartGame();
                    }
                    break;

                case GameState.Playing:
                    if (_inputService.GetJumpInput())
                    {
                        birdController.TryFlap();
                    }
                    
                    // Pause input (optional - can be triggered by UI)
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                    {
                        PauseGame();
                    }
                    break;

                case GameState.Paused:
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                    {
                        ResumeGame();
                    }
                    break;

                case GameState.GameOver:
                    if (_inputService.GetJumpInput())
                    {
                        RestartGame();
                    }
                    break;
            }
        }

        #region State Transition Handlers

        private void OnStateChanged(GameState previousState, GameState newState)
        {
            // Handle state-specific setup
            switch (newState)
            {
                case GameState.Menu:
                    // Handled by StartMenuState()
                    break;

                case GameState.Playing:
                    // Handled by StartGame()
                    break;

                case GameState.Paused:
                    // Already handled by PausedState.Enter()
                    break;

                case GameState.GameOver:
                    // Handled by EndGame()
                    break;
            }
        }

        #endregion

        #region Game Flow Methods

        /// <summary>
        /// Initialize menu state
        /// </summary>
        private void StartMenuState()
        {
            // Reset bird to start position
            birdController.ResetBird();
            
            // Start auto-flap animation
            birdController.StartAutoFlap();

            // Reset score
            _scoreService.ResetScore();

            // Stop spawning pipes
            if (pipeSpawner != null)
            {
                pipeSpawner.StopSpawning();
                pipeSpawner.ClearAllPipes();
            }

            // Start background scrolling (slow)
            if (backgroundScroller != null)
                backgroundScroller.StartScrolling();
            
            if (groundScroller != null)
                groundScroller.StartScrolling();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            if (_stateMachine.CurrentStateType != GameState.Menu)
            {
                return;
            }

            // Stop auto-flap
            birdController.StopAutoFlap();

            // Enable physics
            birdController.SetPhysicsEnabled(true);
            birdController.SetCanFlap(true);

            // Reset score
            _scoreService.ResetScore();

            // Start spawning pipes
            if (pipeSpawner != null)
                pipeSpawner.StartSpawning();

            // Start scrolling
            if (backgroundScroller != null)
                backgroundScroller.StartScrolling();
            
            if (groundScroller != null)
                groundScroller.StartScrolling();

            // Transition to playing state
            _stateMachine.ChangeState(GameState.Playing);

            // Play start sound
            _audioService?.PlaySFX("Start");
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        public void PauseGame()
        {
            if (_stateMachine.CurrentStateType != GameState.Playing)
            {
                return;
            }

            _stateMachine.ChangeState(GameState.Paused);
        }

        /// <summary>
        /// Resume from pause
        /// </summary>
        public void ResumeGame()
        {
            if (_stateMachine.CurrentStateType != GameState.Paused)
            {
                return;
            }

            _stateMachine.ChangeState(GameState.Playing);
        }

        /// <summary>
        /// End the game (called when bird dies)
        /// </summary>
        public void EndGame()
        {
            if (_stateMachine.CurrentStateType != GameState.Playing)
            {
                return;
            }

            // Stop spawning pipes
            if (pipeSpawner != null)
                pipeSpawner.StopSpawning();

            // Stop scrolling
            if (backgroundScroller != null)
                backgroundScroller.StopScrolling();
            
            if (groundScroller != null)
                groundScroller.StopScrolling();

            // Save score to ScoreService (local best)
            _scoreService.SaveBestScore();

            // Save score to PlayerService (for leaderboard)
            SaveScoreToPlayerService();

            // Transition to game over
            _stateMachine.ChangeState(GameState.GameOver);
        }

        /// <summary>
        /// Save current score to PlayerService for leaderboard
        /// </summary>
        private void SaveScoreToPlayerService()
        {
            try
            {
                var playerService = ServiceLocator.Get<PlayerService>();
                if (playerService != null)
                {
                    int currentScore = GetCurrentScore();
                    playerService.UpdateCurrentPlayerScore(currentScore);
                }
            }
            catch (System.Exception)
            {
                // Score not saved to leaderboard
            }
        }

        /// <summary>
        /// Restart the game
        /// </summary>
        public void RestartGame()
        {
            if (_stateMachine.CurrentStateType != GameState.GameOver)
            {
                return;
            }

            // Go back to menu
            _stateMachine.ChangeState(GameState.Menu);
            StartMenuState();
        }

        /// <summary>
        /// Return to main menu
        /// </summary>
        public void ReturnToMenu()
        {
            _stateMachine.ChangeState(GameState.Menu);
            StartMenuState();
        }

        #endregion

        #region UI Input Helpers

        /// <summary>
        /// Check if pointer/mouse is over a UI element
        /// This prevents game input when clicking buttons
        /// </summary>
        private bool IsPointerOverUI()
        {
            // Check for mouse/touch over UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            // Also check for touch input on mobile
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Score Management

        /// <summary>
        /// Add score point (called by ScoreZone trigger)
        /// </summary>
        public void AddScorePoint()
        {
            if (_stateMachine.CurrentStateType != GameState.Playing)
            {
                return;
            }

            _scoreService.AddScore(1);
            _audioService?.PlaySFX("Point");
        }

        /// <summary>
        /// Get current score
        /// </summary>
        public int GetCurrentScore()
        {
            (int current, int best) = _scoreService.GetScores();
            return current;
        }

        /// <summary>
        /// Get best score
        /// </summary>
        public int GetBestScore()
        {
            (int current, int best) = _scoreService.GetScores();
            return best;
        }

        #endregion

        #region Event Callbacks

        /// <summary>
        /// Called when bird dies (subscribe this to OnBirdDied event)
        /// </summary>
        public void OnBirdDied()
        {
            EndGame();
        }

        #endregion

        private void OnDestroy()
        {
            if (_stateMachine != null)
            {
                _stateMachine.OnStateChanged -= OnStateChanged;
            }
        }
    }
}
