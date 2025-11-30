using UnityEngine;
using FlappyBird.UI.Views;
using FlappyBird.UI.Presenters;
using FlappyBird.Utilities.Events;

namespace FlappyBird.UI.Managers
{
    /// <summary>
    /// ============================================
    /// UI MANAGER: Coordinates All UI
    /// ============================================
    /// Central manager for all UI screens
    /// Creates and manages presenters
    /// 
    /// DESIGN PATTERN: Facade + MVP
    /// - Simplifies UI management
    /// - Coordinates between presenters
    /// ============================================
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private PlayerRegistrationView playerRegistrationView;
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private GameplayHUDView gameplayHUDView;
        [SerializeField] private LeaderboardView leaderboardView;

        [Header("Events")]
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onGameStarted;
        [SerializeField] private GameEvent onGameOver;



        // Presenters
        private PlayerRegistrationPresenter _playerRegistrationPresenter;
        private MainMenuPresenter _mainMenuPresenter;
        private GameplayHUDPresenter _gameplayHUDPresenter;
        private LeaderboardPresenter _leaderboardPresenter;

        // Reference to GameFlowManager
        private Gameplay.Managers.GameFlowManager _gameFlowManager;

        private void Start()
        {
            // Find GameFlowManager
            _gameFlowManager = FindObjectOfType<Gameplay.Managers.GameFlowManager>();

            // Create presenters
            InitializePresenters();

            // Subscribe to game events
            SubscribeToEvents();

            // Start with player registration
            ShowPlayerRegistration();
        }

        private void InitializePresenters()
        {
            // Player Registration Presenter
            if (playerRegistrationView != null)
            {
                _playerRegistrationPresenter = new PlayerRegistrationPresenter(playerRegistrationView);
                _playerRegistrationPresenter.OnPlayerRegistered += HandlePlayerRegistered;
            }

            // Main Menu Presenter
            if (mainMenuView != null)
            {
                _mainMenuPresenter = new MainMenuPresenter(mainMenuView);
                _mainMenuPresenter.OnPlayRequested += HandlePlayRequested;
                _mainMenuPresenter.OnChangePlayerRequested += HandleChangePlayerRequested;
                _mainMenuPresenter.OnLeaderboardRequested += HandleLeaderboardFromMenuRequested;
                Debug.Log("[UIManager] MainMenuPresenter initialized with all event handlers");
            }

            // Gameplay HUD Presenter
            if (gameplayHUDView != null)
            {
                _gameplayHUDPresenter = new GameplayHUDPresenter(gameplayHUDView, onScoreChanged);
            }

            // Leaderboard Presenter
            if (leaderboardView != null)
            {
                _leaderboardPresenter = new LeaderboardPresenter(leaderboardView);
                _leaderboardPresenter.OnReplayRequested += HandleLeaderboardReplayRequested;
                _leaderboardPresenter.OnExitRequested += HandleLeaderboardExitRequested;
                Debug.Log("[UIManager] LeaderboardPresenter initialized");
            }
            else
            {
                Debug.LogWarning("[UIManager] LeaderboardView not assigned - leaderboard feature disabled");
            }

            // Pause Presenter
        }

        private void SubscribeToEvents()
        {
            if (onGameStarted != null)
            {
                onGameStarted.RegisterListener(OnGameStarted);
            }

            if (onGameOver != null)
            {
                onGameOver.RegisterListener(OnGameOver);
            }
        }

        #region UI State Management

        /// <summary>
        /// Show player registration screen
        /// </summary>
        public void ShowPlayerRegistration()
        {
            _playerRegistrationPresenter?.Show();
            _mainMenuPresenter?.Hide();
            _gameplayHUDPresenter?.Hide();
            _leaderboardPresenter?.Hide();
        }

        /// <summary>
        /// Show main menu
        /// </summary>
        public void ShowMainMenu()
        {
            _playerRegistrationPresenter?.Hide();
            _mainMenuPresenter?.Show();
            _gameplayHUDPresenter?.Hide();
            _leaderboardPresenter?.Hide();
        }

        /// <summary>
        /// Show gameplay HUD
        /// </summary>
        public void ShowGameplayHUD()
        {
            _playerRegistrationPresenter?.Hide();
            _mainMenuPresenter?.Hide();
            _gameplayHUDPresenter?.ShowGameplayHUD();
            _leaderboardPresenter?.Hide();
        }

        /// <summary>
        /// Show game over - directly shows leaderboard
        /// </summary>
        public void ShowGameOver(int currentScore, int bestScore, bool isNewBest)
        {
            Debug.Log($"[UIManager] Game Over! Score: {currentScore}, Best: {bestScore}, NewBest: {isNewBest}");
            ShowLeaderboard();
        }

        /// <summary>
        /// Show leaderboard screen
        /// </summary>
        public void ShowLeaderboard()
        {
            _playerRegistrationPresenter?.Hide();
            _mainMenuPresenter?.Hide();
            _gameplayHUDPresenter?.Hide();
            _leaderboardPresenter?.Show(10);
        }

        /// <summary>
        /// Hide leaderboard
        /// </summary>
        public void HideLeaderboard()
        {
            _leaderboardPresenter?.Hide();
        }

        #endregion

        #region Public Fallback Methods (For Inspector Button Assignment)

        /// <summary>
        /// PUBLIC: Request to change player (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestChangePlayer()
        {
            Debug.Log("<color=cyan>[UIManager] RequestChangePlayer() called via public method</color>");
            HandleChangePlayerRequested();
        }

        /// <summary>
        /// PUBLIC: Request to show leaderboard (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestShowLeaderboard()
        {
            Debug.Log("<color=cyan>[UIManager] RequestShowLeaderboard() called via public method</color>");
            ShowLeaderboard();
        }

        /// <summary>
        /// PUBLIC: Request to start game (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestPlay()
        {
            Debug.Log("<color=cyan>[UIManager] RequestPlay() called via public method</color>");
            HandlePlayRequested();
        }

        /// <summary>
        /// PUBLIC: Request to return to main menu (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestMainMenu()
        {
            Debug.Log("<color=cyan>[UIManager] RequestMainMenu() called via public method</color>");
            HandleMenuRequested();
        }

        /// <summary>
        /// PUBLIC: Request restart (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestRestart()
        {
            Debug.Log("<color=cyan>[UIManager] RequestRestart() called via public method</color>");
            HandleRestartRequested();
        }

        #endregion

        #region Event Handlers

        private void HandlePlayerRegistered(string playerName)
        {
            Debug.Log($"[UIManager] Player '{playerName}' registered - showing main menu");
            ShowMainMenu();
        }

        private void HandleChangePlayerRequested()
        {
            Debug.Log("[UIManager] Change player requested - showing registration");
            ShowPlayerRegistration();
        }

        private void HandleLeaderboardFromMenuRequested()
        {
            Debug.Log("[UIManager] Leaderboard requested from main menu");
            ShowLeaderboard();
        }

        private void HandlePlayRequested()
        {
            Debug.Log("[UIManager] Play requested - hiding main menu");
            ShowGameplayHUD(); // Ocultar menú inmediatamente
            _gameFlowManager?.StartGame();
        }

        private void HandleRestartRequested()
        {
            Debug.Log("[UIManager] Restart requested");
            _gameFlowManager?.RestartGame();
        }

        private void HandleMenuRequested()
        {
            Debug.Log("[UIManager] Menu requested - returning to main menu");
            // Ocultar todas las pantallas y mostrar menú principal
            _gameplayHUDPresenter?.Hide();
            _leaderboardPresenter?.Hide();
            ShowMainMenu();
            // Reiniciar el estado del juego
            _gameFlowManager?.ReturnToMenu();
        }

        private void HandleLeaderboardReplayRequested()
        {
            Debug.Log("[UIManager] Leaderboard replay requested");
            _leaderboardPresenter?.Hide();
            _gameFlowManager?.RestartGame();
        }

        private void HandleLeaderboardExitRequested()
        {
            Debug.Log("[UIManager] Leaderboard exit requested - returning to main menu");
            _leaderboardPresenter?.Hide();
            ShowMainMenu();
            _gameFlowManager?.ReturnToMenu();
        }

        private void OnGameStarted()
        {
            Debug.Log("[UIManager] Game started - showing gameplay HUD");
            ShowGameplayHUD();
            _gameplayHUDPresenter?.StartGame();
        }

        private void OnGameOver()
        {
            Debug.Log("[UIManager] Game over - showing game over screen");
            
            if (_gameFlowManager != null)
            {
                int currentScore = _gameFlowManager.GetCurrentScore();
                int bestScore = _gameFlowManager.GetBestScore();
                bool isNewBest = currentScore > bestScore;
                
                ShowGameOver(currentScore, bestScore, isNewBest);
            }
        }

        #endregion

        private void OnDestroy()
        {
            // Unsubscribe from presenter events before disposing
            if (_mainMenuPresenter != null)
            {
                _mainMenuPresenter.OnPlayRequested -= HandlePlayRequested;
                _mainMenuPresenter.OnChangePlayerRequested -= HandleChangePlayerRequested;
                _mainMenuPresenter.OnLeaderboardRequested -= HandleLeaderboardFromMenuRequested;
            }

            if (_playerRegistrationPresenter != null)
            {
                _playerRegistrationPresenter.OnPlayerRegistered -= HandlePlayerRegistered;
            }

            if (_leaderboardPresenter != null)
            {
                _leaderboardPresenter.OnReplayRequested -= HandleLeaderboardReplayRequested;
                _leaderboardPresenter.OnExitRequested -= HandleLeaderboardExitRequested;
            }

            // Cleanup presenters
            _playerRegistrationPresenter?.Dispose();
            _mainMenuPresenter?.Dispose();
            _gameplayHUDPresenter?.Dispose();
            _leaderboardPresenter?.Dispose();

            // Unsubscribe from events
            if (onGameStarted != null)
            {
                onGameStarted.UnregisterListener(OnGameStarted);
            }

            if (onGameOver != null)
            {
                onGameOver.UnregisterListener(OnGameOver);
            }
        }
    }
}
