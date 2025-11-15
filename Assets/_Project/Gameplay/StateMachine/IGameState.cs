using FlappyBird.Core.Entities;

namespace FlappyBird.Gameplay.StateMachine
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: State Pattern (Interface)
    /// ============================================
    /// Base interface for all game states
    /// 
    /// Benefits:
    /// - Eliminates complex if/else chains
    /// - Each state encapsulates its own behavior
    /// - Easy to add new states without modifying existing code
    /// - Follows Open/Closed Principle
    /// 
    /// States: Menu, Playing, Paused, GameOver
    /// ============================================
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Get the state type
        /// </summary>
        GameState StateType { get; }

        /// <summary>
        /// Called when entering this state
        /// </summary>
        void Enter();

        /// <summary>
        /// Called every frame while in this state
        /// </summary>
        void Update();

        /// <summary>
        /// Called when exiting this state
        /// </summary>
        void Exit();

        /// <summary>
        /// Handle input in this state
        /// </summary>
        void HandleInput();
    }
}
