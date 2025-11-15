using UnityEngine;
using FlappyBird.Core.Entities;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Gameplay.StateMachine
{
    /// <summary>
    /// ============================================
    /// STATE: Paused State
    /// ============================================
    /// Game is temporarily paused
    /// - Physics frozen
    /// - No input except unpause
    /// - Pause menu visible
    /// ============================================
    /// </summary>
    public class PausedState : IGameState
    {
        private readonly GameEvent _onPauseToggled;

        public GameState StateType => GameState.Paused;

        public PausedState(GameEvent onPauseToggled = null)
        {
            _onPauseToggled = onPauseToggled;
        }

        public void Enter()
        {
            Debug.Log("[PausedState] Entered Paused State");
            // Freeze physics
            Time.timeScale = 0f;
            // Show pause menu
            // Mute/dim audio
            _onPauseToggled?.Raise();
        }

        public void Update()
        {
            // Nothing updates while paused
        }

        public void HandleInput()
        {
            // Only unpause input accepted
            // Will be handled by pause menu
        }

        public void Exit()
        {
            Debug.Log("[PausedState] Exiting Paused State");
            // Unfreeze physics
            Time.timeScale = 1f;
            // Hide pause menu
            // Restore audio
            _onPauseToggled?.Raise();
        }
    }
}
