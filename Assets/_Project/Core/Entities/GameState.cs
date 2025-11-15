namespace FlappyBird.Core.Entities
{
    /// <summary>
    /// ============================================
    /// CORE ENTITY: Game State
    /// ============================================
    /// Represents the different states of the game
    /// 
    /// STATE PATTERN foundation
    /// Each enum value will have a corresponding state class
    /// 
    /// SOLID: Single Responsibility
    /// - Only defines game states, no behavior
    /// ============================================
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Main menu, waiting for player to start
        /// Bird does auto-flap animation
        /// </summary>
        Menu,

        /// <summary>
        /// Game is actively being played
        /// Player has control, pipes are spawning
        /// </summary>
        Playing,

        /// <summary>
        /// Game is paused
        /// Physics frozen, no input processed
        /// </summary>
        Paused,

        /// <summary>
        /// Player died, showing game over screen
        /// Score is saved, waiting for restart/menu
        /// </summary>
        GameOver
    }
}
