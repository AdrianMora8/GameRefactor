using UnityEngine;
using FlappyBird.Core.Entities;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Gameplay.StateMachine
{
    /// <summary>
    /// ============================================
    /// STATE: Menu State
    /// ============================================
    /// Initial state when game starts
    /// - Bird does auto-flap animation
    /// - Waiting for player input to start
    /// - Background scrolling active
    /// ============================================
    /// </summary>
    public class MenuState : IGameState
    {
        private readonly GameEvent _onGameStarted;

        public GameState StateType => GameState.Menu;

        public MenuState(GameEvent onGameStarted = null)
        {
            _onGameStarted = onGameStarted;
        }

        public void Enter()
        {
            // Reset game elements
            // Bird positioned at start
            // Auto-flap active
            // UI: Show "Tap to Start"
        }

        public void Update()
        {
            // Auto-flap logic handled by BirdController
            // Background scrolling (slow)
        }

        public void HandleInput()
        {
            // Any input triggers game start
            // Will be handled by game flow manager
        }

        public void Exit()
        {
            _onGameStarted?.Raise();
        }
    }
}
