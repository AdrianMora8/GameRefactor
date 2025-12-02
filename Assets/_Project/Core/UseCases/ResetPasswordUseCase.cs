using FlappyBird.Core.Entities;
using FlappyBird.Infrastructure.Data;

namespace FlappyBird.Core.UseCases
{
    /// <summary>
    /// ============================================
    /// USE CASE: Reset Password
    /// ============================================
    /// Business logic for resetting a player's password
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles password reset logic
    /// 
    /// PATTERN: Use Case (Clean Architecture)
    /// ============================================
    /// </summary>
    public class ResetPasswordUseCase
    {
        private readonly IPlayerRepository _playerRepository;

        public ResetPasswordUseCase(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Reset player's password (must be different from current)
        /// </summary>
        /// <param name="playerName">Player name</param>
        /// <param name="newPassword">New password</param>
        public void Execute(string playerName, string newPassword)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new System.ArgumentException("Player name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new System.ArgumentException("New password cannot be empty");
            }

            // Check if player exists
            var existingPlayer = _playerRepository.GetPlayer(playerName);
            
            if (existingPlayer == null)
            {
                throw new System.ArgumentException("Player not found");
            }

            // Check if new password is same as old (case-sensitive)
            if (existingPlayer.VerifyPassword(newPassword))
            {
                throw new System.ArgumentException("New password must be different from current password");
            }

            // Create updated player with new password
            var updatedPlayer = new Player(
                existingPlayer.Name,
                newPassword,
                existingPlayer.BestScore,
                existingPlayer.LastPlayedDate
            );

            // Save updated player
            _playerRepository.SavePlayer(updatedPlayer);
        }
    }
}
