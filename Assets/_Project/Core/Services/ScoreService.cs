using FlappyBird.Core.Entities;
using FlappyBird.Core.Interfaces;
using FlappyBird.Utilities;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Core.Services
{
    /// <summary>
    /// ============================================
    /// SCORE SERVICE
    /// ============================================
    /// Manages all score-related logic
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles scoring logic
    /// 
    /// SOLID: Dependency Inversion
    /// - Depends on ISaveService interface, not concrete implementation
    /// 
    /// DESIGN PATTERN: Service Layer
    /// - Business logic separated from presentation
    /// - Uses ScoreData entity for calculations
    /// ============================================
    /// </summary>
    public class ScoreService
    {
        private readonly ScoreData _scoreData;
        private readonly ISaveService _saveService;
        
        // Events
        private readonly IntGameEvent _onScoreChanged;
        private readonly IntGameEvent _onNewBestScore;

        /// <summary>
        /// Current score (read-only access)
        /// </summary>
        public int CurrentScore => _scoreData.CurrentScore;

        /// <summary>
        /// Best score (read-only access)
        /// </summary>
        public int BestScore => _scoreData.BestScore;

        /// <summary>
        /// Is current score a new record?
        /// </summary>
        public bool IsNewBestScore => _scoreData.IsNewBestScore;

        /// <summary>
        /// Current combo streak
        /// </summary>
        public int CurrentStreak => _scoreData.CurrentStreak;

        /// <summary>
        /// Initialize score service
        /// </summary>
        /// <param name="saveService">Save service for persistence</param>
        /// <param name="onScoreChanged">Event to raise when score changes</param>
        /// <param name="onNewBestScore">Event to raise on new best score</param>
        public ScoreService(
            ISaveService saveService, 
            IntGameEvent onScoreChanged = null, 
            IntGameEvent onNewBestScore = null)
        {
            _saveService = saveService;
            _onScoreChanged = onScoreChanged;
            _onNewBestScore = onNewBestScore;

            // Load saved best score
            int savedBestScore = _saveService?.LoadInt(GameConstants.SAVE_KEY_BEST_SCORE, 0) ?? 0;
            _scoreData = new ScoreData(savedBestScore);
        }

        /// <summary>
        /// Add points to score
        /// </summary>
        public void AddScore(int points = 1)
        {
            bool wasBestScore = _scoreData.IsNewBestScore;
            
            _scoreData.AddScore(points);

            // Raise score changed event
            _onScoreChanged?.Raise(_scoreData.CurrentScore);

            // Raise new best score event if it just became a record
            if (_scoreData.IsNewBestScore && !wasBestScore)
            {
                _onNewBestScore?.Raise(_scoreData.BestScore);
            }
        }

        /// <summary>
        /// Reset score for new game
        /// </summary>
        public void ResetScore()
        {
            _scoreData.ResetCurrentScore();
            _onScoreChanged?.Raise(0);
        }

        /// <summary>
        /// Save current best score to persistent storage
        /// </summary>
        public void SaveBestScore()
        {
            if (_saveService != null)
            {
                _saveService.SaveInt(GameConstants.SAVE_KEY_BEST_SCORE, _scoreData.BestScore);
            }
        }

        /// <summary>
        /// Get score data for display
        /// </summary>
        public (int current, int best) GetScores()
        {
            return (_scoreData.CurrentScore, _scoreData.BestScore);
        }

        /// <summary>
        /// Check if player is close to beating their record
        /// </summary>
        public bool IsNearBestScore(int threshold = 5)
        {
            return _scoreData.IsNearBestScore(threshold);
        }

        /// <summary>
        /// Get score gap to best score
        /// </summary>
        public int GetScoreGap()
        {
            return _scoreData.GetScoreGap();
        }

        /// <summary>
        /// Break current streak (e.g., on pause)
        /// </summary>
        public void BreakStreak()
        {
            _scoreData.BreakStreak();
        }

        /// <summary>
        /// Reset all scores including best score
        /// </summary>
        public void ResetAllScores()
        {
            _scoreData.ResetAll();
            
            if (_saveService != null)
            {
                _saveService.SaveInt(GameConstants.SAVE_KEY_BEST_SCORE, 0);
            }

            _onScoreChanged?.Raise(0);
        }
    }
}
