namespace FlappyBird.Core.Interfaces
{
    /// <summary>
    /// ============================================
    /// INTERFACE: Input Service
    /// ============================================
    /// Contract for input handling across platforms
    /// 
    /// SOLID: Open/Closed Principle
    /// - New input providers can be added without modifying existing code
    /// 
    /// DESIGN PATTERN: Strategy Pattern
    /// - Different implementations for PC/Mobile
    /// - Swappable at runtime
    /// ============================================
    /// </summary>
    public interface IInputService
    {
        /// <summary>
        /// Check if jump input was pressed this frame
        /// </summary>
        bool GetJumpInput();

        /// <summary>
        /// Check if pause input was pressed this frame
        /// </summary>
        bool GetPauseInput();

        /// <summary>
        /// Check if any input was pressed (for starting game)
        /// </summary>
        bool GetAnyInput();
    }
}
