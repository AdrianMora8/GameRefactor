using UnityEngine;
using FlappyBird.Core.Entities;

namespace FlappyBird.Gameplay.StateMachine
{
    /// <summary>
    /// ============================================
    /// STATE: Playing State
    /// ============================================
    /// Main gameplay state
    /// - Player has control
    /// - Pipes spawning
    /// - Score counting
    /// - Physics active
    /// ============================================
    /// </summary>
    public class PlayingState : IGameState
    {
        public GameState StateType => GameState.Playing;

        public void Enter()
        {
            Debug.Log("[PlayingState] Entered Playing State");
            // Enable player control
            // Start pipe spawning
            // Enable score counting
            // Physics active
            // UI: Show score
        }

        public void Update()
        {
            // Pipe spawning logic
            // Background scrolling (normal speed)
            // Collision detection
            // Score zone detection
        }

        public void HandleInput()
        {
            // Jump input handled by BirdController
            // Pause input handled here
        }

        public void Exit()
        {
            Debug.Log("[PlayingState] Exiting Playing State");
            // Stop pipe spawning
        }
    }
}
