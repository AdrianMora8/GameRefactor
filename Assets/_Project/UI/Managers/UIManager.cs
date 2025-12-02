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
        [SerializeField] private PasswordRecoveryView passwordRecoveryView;
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private GameplayHUDView gameplayHUDView;
        [SerializeField] private LeaderboardView leaderboardView;

        [Header("Events")]
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onGameStarted;
        [SerializeField] private GameEvent onGameOver;



        // Presenters
        private PlayerRegistrationPresenter _playerRegistrationPresenter;
        private PasswordRecoveryPresenter _passwordRecoveryPresenter;
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
                _playerRegistrationPresenter.OnForgotPasswordClicked += HandleForgotPasswordClicked;
            }

            // Password Recovery Presenter
            if (passwordRecoveryView != null)
            {
                _passwordRecoveryPresenter = new PasswordRecoveryPresenter(passwordRecoveryView);
                _passwordRecoveryPresenter.OnPasswordReset += HandlePasswordReset;
                _passwordRecoveryPresenter.OnCancelled += HandlePasswordRecoveryCancelled;
            }

            // Main Menu Presenter
            if (mainMenuView != null)
            {
                _mainMenuPresenter = new MainMenuPresenter(mainMenuView);
                _mainMenuPresenter.OnPlayRequested += HandlePlayRequested;
                _mainMenuPresenter.OnChangePlayerRequested += HandleChangePlayerRequested;
                _mainMenuPresenter.OnLeaderboardRequested += HandleLeaderboardFromMenuRequested;
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
            _passwordRecoveryPresenter?.Hide();
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
            _passwordRecoveryPresenter?.Hide();
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
            HandleChangePlayerRequested();
        }

        /// <summary>
        /// PUBLIC: Request to show leaderboard (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestShowLeaderboard()
        {
            ShowLeaderboard();
        }

        /// <summary>
        /// PUBLIC: Request to start game (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestPlay()
        {
            HandlePlayRequested();
        }

        /// <summary>
        /// PUBLIC: Request to return to main menu (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestMainMenu()
        {
            HandleMenuRequested();
        }

        /// <summary>
        /// PUBLIC: Request restart (can be assigned to button OnClick in Inspector)
        /// </summary>
        public void RequestRestart()
        {
            HandleRestartRequested();
        }

        #endregion

        #region Event Handlers

        private void HandlePlayerRegistered(string playerName)
        {
            ShowMainMenu();
        }

        private void HandleForgotPasswordClicked()
        {
            _playerRegistrationPresenter?.Hide();
            _passwordRecoveryPresenter?.Show();
        }

        private void HandlePasswordReset()
        {
            // Password was reset successfully, go back to registration
            _passwordRecoveryPresenter?.Hide();
            _playerRegistrationPresenter?.Show();
        }

        private void HandlePasswordRecoveryCancelled()
        {
            // User cancelled, go back to registration
            _passwordRecoveryPresenter?.Hide();
            _playerRegistrationPresenter?.Show();
        }

        private void HandleChangePlayerRequested()
        {
            ShowPlayerRegistration();
        }

        private void HandleLeaderboardFromMenuRequested()
        {
            ShowLeaderboard();
        }

        private void HandlePlayRequested()
        {
            ShowGameplayHUD(); // Ocultar menú inmediatamente
            _gameFlowManager?.StartGame();
        }

        private void HandleRestartRequested()
        {
            _gameFlowManager?.RestartGame();
        }

        private void HandleMenuRequested()
        {
            // Ocultar todas las pantallas y mostrar menú principal
            _gameplayHUDPresenter?.Hide();
            _leaderboardPresenter?.Hide();
            ShowMainMenu();
            // Reiniciar el estado del juego
            _gameFlowManager?.ReturnToMenu();
        }

        private void HandleLeaderboardReplayRequested()
        {
            _leaderboardPresenter?.Hide();
            _gameFlowManager?.RestartGame();
        }

        private void HandleLeaderboardExitRequested()
        {
            _leaderboardPresenter?.Hide();
            ShowMainMenu();
            _gameFlowManager?.ReturnToMenu();
        }

        private void OnGameStarted()
        {
            ShowGameplayHUD();
            _gameplayHUDPresenter?.StartGame();
        }

        private void OnGameOver()
        {
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
                _playerRegistrationPresenter.OnForgotPasswordClicked -= HandleForgotPasswordClicked;
            }

            if (_passwordRecoveryPresenter != null)
            {
                _passwordRecoveryPresenter.OnPasswordReset -= HandlePasswordReset;
                _passwordRecoveryPresenter.OnCancelled -= HandlePasswordRecoveryCancelled;
            }

            if (_leaderboardPresenter != null)
            {
                _leaderboardPresenter.OnReplayRequested -= HandleLeaderboardReplayRequested;
                _leaderboardPresenter.OnExitRequested -= HandleLeaderboardExitRequested;
            }

            // Cleanup presenters
            _playerRegistrationPresenter?.Dispose();
            _passwordRecoveryPresenter?.Dispose();
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
