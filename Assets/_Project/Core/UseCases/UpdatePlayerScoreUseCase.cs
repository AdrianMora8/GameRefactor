using FlappyBird.Core.Entities;
using FlappyBird.Infrastructure.Data;

namespace FlappyBird.Core.UseCases
{
    /// <summary>
    /// ============================================
    /// USE CASE: Update Player Score
    /// ============================================
    /// Business logic for updating player's score
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles score update logic
    /// 
    /// PATTERN: Use Case (Clean Architecture)
    /// ============================================
    /// </summary>
    public class UpdatePlayerScoreUseCase
    {
        private readonly IPlayerRepository _playerRepository;

        public UpdatePlayerScoreUseCase(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Update player's score if it's higher than current best
        /// </summary>
        /// <param name="playerName">Player name</param>
        /// <param name="newScore">New score achieved</param>
        /// <returns>True if it's a new record, false otherwise</returns>
        public bool Execute(string playerName, int newScore)
        {
            // Get player
            var player = _playerRepository.GetPlayer(playerName);
            
            if (player == null)
            {
                // Player doesn't exist - this shouldn't happen normally
                // but we handle it gracefully by not updating
                return false;
            }

            // Update score
            bool isNewRecord = player.UpdateScore(newScore);
            
            // Save updated player
            _playerRepository.SavePlayer(player);
            
            return isNewRecord;
        }
    }
}
