using System.Collections.Generic;
using FlappyBird.Core.Entities;
using FlappyBird.Core.UseCases;
using FlappyBird.Infrastructure.Data;

namespace FlappyBird.Infrastructure.Services
{
    /// <summary>
    /// ============================================
    /// SERVICE: Player Management
    /// ============================================
    /// Facade for player operations
    /// Simplifies access to player use cases
    /// 
    /// SOLID: Facade Pattern
    /// - Provides simple interface to complex subsystem
    /// 
    /// PATTERN: Service Layer
    /// ============================================
    /// </summary>
    public class PlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly RegisterPlayerUseCase _registerPlayerUseCase;
        private readonly UpdatePlayerScoreUseCase _updateScoreUseCase;
        private readonly GetLeaderboardUseCase _getLeaderboardUseCase;

        private Player _currentPlayer;

        public PlayerService()
        {
            _repository = new PlayerRepository();
            _registerPlayerUseCase = new RegisterPlayerUseCase(_repository);
            _updateScoreUseCase = new UpdatePlayerScoreUseCase(_repository);
            _getLeaderboardUseCase = new GetLeaderboardUseCase(_repository);
        }

        /// <summary>
        /// Get the current active player
        /// </summary>
        public Player GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        /// <summary>
        /// Register or switch to a player
        /// </summary>
        public Player RegisterPlayer(string playerName)
        {
            _currentPlayer = _registerPlayerUseCase.Execute(playerName);
            return _currentPlayer;
        }

        /// <summary>
        /// Update current player's score
        /// </summary>
        public bool UpdateCurrentPlayerScore(int newScore)
        {
            if (_currentPlayer == null)
            {
                return false;
            }

            return _updateScoreUseCase.Execute(_currentPlayer.Name, newScore);
        }

        /// <summary>
        /// Get leaderboard (top players)
        /// </summary>
        public List<Player> GetLeaderboard(int maxPlayers = 10)
        {
            return _getLeaderboardUseCase.Execute(maxPlayers);
        }

        /// <summary>
        /// Check if there's a current player
        /// </summary>
        public bool HasCurrentPlayer()
        {
            return _currentPlayer != null;
        }

        /// <summary>
        /// Clear all player data (for testing)
        /// </summary>
        public void ClearAllData()
        {
            _repository.ClearAll();
            _currentPlayer = null;
        }
    }
}
