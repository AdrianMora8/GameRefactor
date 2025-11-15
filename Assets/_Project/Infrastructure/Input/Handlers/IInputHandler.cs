namespace FlappyBird.Infrastructure.Input.Handlers
{
    /// <summary>
    /// ============================================
    /// INPUT HANDLER INTERFACE
    /// ============================================
    /// Common interface for all input handlers
    /// Allows platform-specific implementations
    /// 
    /// SOLID: Dependency Inversion
    /// - Depend on abstraction, not concrete classes
    /// ============================================
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Update input state (called every frame)
        /// </summary>
        void UpdateInput();

        /// <summary>
        /// Check if jump was requested this frame
        /// </summary>
        bool GetJumpInput();
    }
}
