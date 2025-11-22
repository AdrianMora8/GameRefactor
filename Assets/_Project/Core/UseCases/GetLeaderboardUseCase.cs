using System.Collections.Generic;
using System.Linq;
using FlappyBird.Core.Entities;
using FlappyBird.Infrastructure.Data;

namespace FlappyBird.Core.UseCases
{
    /// <summary>
    /// ============================================
    /// USE CASE: Get Leaderboard
    /// ============================================
    /// Business logic for retrieving and sorting leaderboard
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles leaderboard retrieval logic
    /// 
    /// PATTERN: Use Case (Clean Architecture)
    /// ============================================
    /// </summary>
    public class GetLeaderboardUseCase
    {
        private readonly IPlayerRepository _playerRepository;

        public GetLeaderboardUseCase(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Get all players sorted by best score (descending)
        /// </summary>
        /// <param name="maxPlayers">Maximum number of players to return (0 = all)</param>
        /// <returns>List of players sorted by score</returns>
        public List<Player> Execute(int maxPlayers = 0)
        {
            var allPlayers = _playerRepository.GetAllPlayers();
            
            // Sort by best score (descending)
            var sortedPlayers = allPlayers
                .OrderByDescending(p => p.BestScore)
                .ThenBy(p => p.Name)
                .ToList();

            // Limit if requested
            if (maxPlayers > 0 && sortedPlayers.Count > maxPlayers)
            {
                sortedPlayers = sortedPlayers.Take(maxPlayers).ToList();
            }

            return sortedPlayers;
        }
    }
}
