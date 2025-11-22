using FlappyBird.Core.Entities;
using FlappyBird.Infrastructure.Data;

namespace FlappyBird.Core.UseCases
{
    /// <summary>
    /// ============================================
    /// USE CASE: Register/Change Player
    /// ============================================
    /// Business logic for registering a new player or switching to existing one
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles player registration logic
    /// 
    /// PATTERN: Use Case (Clean Architecture)
    /// ============================================
    /// </summary>
    public class RegisterPlayerUseCase
    {
        private readonly IPlayerRepository _playerRepository;

        public RegisterPlayerUseCase(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Register or load an existing player
        /// </summary>
        /// <param name="playerName">Player name</param>
        /// <returns>The registered player</returns>
        public Player Execute(string playerName)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new System.ArgumentException("Player name cannot be empty");
            }

            // Check if player already exists
            var existingPlayer = _playerRepository.GetPlayer(playerName);
            
            if (existingPlayer != null)
            {
                // Player exists, return it
                return existingPlayer;
            }

            // Create new player
            var newPlayer = new Player(playerName);
            _playerRepository.SavePlayer(newPlayer);
            
            return newPlayer;
        }
    }
}
