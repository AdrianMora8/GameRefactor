using UnityEngine;
using FlappyBird.UI.Views;
using FlappyBird.UI.Presenters;
using FlappyBird.Utilities.Events;
using FlappyBird.Core.Entities;

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
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private GameplayHUDView gameplayHUDView;
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private PauseView pauseView;

        [Header("Events")]
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onGameStarted;
        [SerializeField] private GameEvent onGameOver;

        // Presenters
        private MainMenuPresenter _mainMenuPresenter;
        private GameplayHUDPresenter _gameplayHUDPresenter;
        private GameOverPresenter _gameOverPresenter;
        private PausePresenter _pausePresenter;

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

            // Start with main menu
            ShowMainMenu();
        }

        private void InitializePresenters()
        {
            // Main Menu Presenter
            if (mainMenuView != null)
            {
                _mainMenuPresenter = new MainMenuPresenter(mainMenuView);
                _mainMenuPresenter.OnPlayRequested += HandlePlayRequested;
            }

            // Gameplay HUD Presenter
            if (gameplayHUDView != null)
            {
                _gameplayHUDPresenter = new GameplayHUDPresenter(gameplayHUDView, onScoreChanged);
            }

            // Game Over Presenter
            if (gameOverView != null)
            {
                _gameOverPresenter = new GameOverPresenter(gameOverView);
                _gameOverPresenter.OnRestartRequested += HandleRestartRequested;
                _gameOverPresenter.OnMenuRequested += HandleMenuRequested;
            }

            // Pause Presenter
            if (pauseView != null)
            {
                _pausePresenter = new PausePresenter(pauseView);
                _pausePresenter.OnResumeRequested += HandleResumeRequested;
                _pausePresenter.OnMenuRequested += HandleMenuRequested;
            }
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
        /// Show main menu
        /// </summary>
        public void ShowMainMenu()
        {
            _mainMenuPresenter?.Show();
            _gameplayHUDPresenter?.Hide();
            _gameOverPresenter?.Hide();
            _pausePresenter?.Hide();
        }

        /// <summary>
        /// Show gameplay HUD
        /// </summary>
        public void ShowGameplayHUD()
        {
            _mainMenuPresenter?.Hide();
            _gameplayHUDPresenter?.ShowGameplayHUD();
            _gameOverPresenter?.Hide();
            _pausePresenter?.Hide();
        }

        /// <summary>
        /// Show game over screen
        /// </summary>
        public void ShowGameOver(int currentScore, int bestScore, bool isNewBest)
        {
            _mainMenuPresenter?.Hide();
            _gameplayHUDPresenter?.Hide();
            _gameOverPresenter?.ShowGameOver(currentScore, bestScore, isNewBest);
            _pausePresenter?.Hide();
        }

        /// <summary>
        /// Show pause screen
        /// </summary>
        public void ShowPause()
        {
            _pausePresenter?.Show();
        }

        /// <summary>
        /// Hide pause screen
        /// </summary>
        public void HidePause()
        {
            _pausePresenter?.Hide();
        }

        #endregion

        #region Event Handlers

        private void HandlePlayRequested()
        {
            Debug.Log("[UIManager] Play requested");
            _gameFlowManager?.StartGame();
        }

        private void HandleRestartRequested()
        {
            Debug.Log("[UIManager] Restart requested");
            _gameFlowManager?.RestartGame();
        }

        private void HandleMenuRequested()
        {
            Debug.Log("[UIManager] Menu requested");
            _gameFlowManager?.ReturnToMenu();
        }

        private void HandleResumeRequested()
        {
            Debug.Log("[UIManager] Resume requested");
            _gameFlowManager?.ResumeGame();
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
            // Cleanup presenters
            _mainMenuPresenter?.Dispose();
            _gameplayHUDPresenter?.Dispose();
            _gameOverPresenter?.Dispose();
            _pausePresenter?.Dispose();

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
