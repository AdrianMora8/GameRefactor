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
        /// Register a new player with password
        /// </summary>
        /// <param name="playerName">Player name</param>
        /// <param name="password">Player password</param>
        /// <returns>The registered player</returns>
        public Player Execute(string playerName, string password)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new System.ArgumentException("Player name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("Password cannot be empty");
            }

            // Check if player already exists
            var existingPlayer = _playerRepository.GetPlayer(playerName);
            
            if (existingPlayer != null)
            {
                throw new System.ArgumentException("Player already exists. Use login instead.");
            }

            // Create new player with password
            var newPlayer = new Player(playerName, password);
            _playerRepository.SavePlayer(newPlayer);
            
            return newPlayer;
        }

        /// <summary>
        /// Login existing player with password verification
        /// </summary>
        /// <param name="playerName">Player name</param>
        /// <param name="password">Player password</param>
        /// <returns>The authenticated player</returns>
        public Player Login(string playerName, string password)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new System.ArgumentException("Player name cannot be empty");
            }

            // Check if player exists
            var existingPlayer = _playerRepository.GetPlayer(playerName);
            
            if (existingPlayer == null)
            {
                throw new System.ArgumentException("Player not found. Please register first.");
            }

            // Verify password
            if (!existingPlayer.VerifyPassword(password))
            {
                throw new System.UnauthorizedAccessException("Incorrect password!");
            }

            return existingPlayer;
        }
    }
}
