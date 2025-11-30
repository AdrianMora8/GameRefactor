using UnityEngine;
using FlappyBird.Core.Entities;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Gameplay.StateMachine
{
    /// <summary>
    /// ============================================
    /// STATE: Game Over State
    /// ============================================
    /// Player died, game ended
    /// - Show game over screen
    /// - Display scores
    /// - Save best score
    /// - Options: Restart or Menu
    /// ============================================
    /// </summary>
    public class GameOverState : IGameState
    {
        private readonly GameEvent _onShowGameOver;
        private readonly GameEvent _onBirdDied;

        public GameState StateType => GameState.GameOver;

        public GameOverState(GameEvent onShowGameOver = null, GameEvent onBirdDied = null)
        {
            _onShowGameOver = onShowGameOver;
            _onBirdDied = onBirdDied;
        }

        public void Enter()
        {
            // Stop all gameplay
            // Save score
            // Show game over UI
            // Play game over sound/animation
            
            _onBirdDied?.Raise();
            _onShowGameOver?.Raise();
        }

        public void Update()
        {
            // Wait for player input (restart/menu)
            // Optional: particle effects, animations
        }

        public void HandleInput()
        {
            // Restart button → Menu State (then back to Playing)
            // Menu button → Menu State
        }

        public void Exit()
        {
            // Hide game over UI
            // Reset game elements for next game
        }
    }
}
