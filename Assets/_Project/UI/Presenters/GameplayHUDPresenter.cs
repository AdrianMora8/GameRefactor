using FlappyBird.UI.Views;
using FlappyBird.Utilities.Events;

namespace FlappyBird.UI.Presenters
{
    /// <summary>
    /// ============================================
    /// PRESENTER: Gameplay HUD Logic
    /// ============================================
    /// Handles logic for in-game HUD
    /// 
    /// DESIGN PATTERN: MVP (Presenter)
    /// - Contains UI logic
    /// - Updates view based on events
    /// - No direct Unity dependencies
    /// ============================================
    /// </summary>
    public class GameplayHUDPresenter
    {
        private readonly GameplayHUDView _view;
        private readonly IntGameEvent _onScoreChanged;

        public GameplayHUDPresenter(GameplayHUDView view, IntGameEvent onScoreChanged)
        {
            _view = view;
            _onScoreChanged = onScoreChanged;

            // Subscribe to events
            if (_onScoreChanged != null)
            {
                _onScoreChanged.RegisterListener(OnScoreChanged);
            }
        }

        /// <summary>
        /// Show HUD for gameplay
        /// </summary>
        public void ShowGameplayHUD()
        {
            _view.Show();
            _view.ShowGetReady();
            _view.UpdateScore(0);
        }

        /// <summary>
        /// Start game (hide get ready, show score)
        /// </summary>
        public void StartGame()
        {
            _view.HideGetReady();
        }

        /// <summary>
        /// Hide HUD
        /// </summary>
        public void Hide()
        {
            _view.Hide();
        }

        /// <summary>
        /// Called when score changes
        /// </summary>
        private void OnScoreChanged(int newScore)
        {
            _view.UpdateScore(newScore);
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            if (_onScoreChanged != null)
            {
                _onScoreChanged.UnregisterListener(OnScoreChanged);
            }
        }
    }
}
