using System.Collections.Generic;
using FlappyBird.Core.Entities;

namespace FlappyBird.Infrastructure.Data
{
    /// <summary>
    /// ============================================
    /// INTERFACE: Player Repository
    /// ============================================
    /// Contract for player data persistence
    /// 
    /// SOLID: Dependency Inversion Principle
    /// - High-level code depends on this interface, not implementation
    /// 
    /// PATTERN: Repository Pattern
    /// - Abstracts data access
    /// ============================================
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Get a player by name
        /// </summary>
        Player GetPlayer(string playerName);

        /// <summary>
        /// Get all players
        /// </summary>
        List<Player> GetAllPlayers();

        /// <summary>
        /// Save or update a player
        /// </summary>
        void SavePlayer(Player player);

        /// <summary>
        /// Delete a player
        /// </summary>
        void DeletePlayer(string playerName);

        /// <summary>
        /// Check if a player exists
        /// </summary>
        bool PlayerExists(string playerName);

        /// <summary>
        /// Clear all player data
        /// </summary>
        void ClearAll();
    }
}
