using FlappyBird.Core.Interfaces;

namespace FlappyBird.Infrastructure.Input
{
    /// <summary>
    /// ============================================
    /// NULL INPUT PROVIDER (Null Object Pattern)
    /// ============================================
    /// Used when no input should be processed
    /// Useful for:
    /// - Cutscenes
    /// - Loading screens
    /// - Tutorial pauses
    /// 
    /// DESIGN PATTERN: Null Object Pattern
    /// - Avoids null checks
    /// - Safe default behavior
    /// ============================================
    /// </summary>
    public class NullInputProvider : IInputService
    {
        public bool GetJumpInput() => false;
        public bool GetPauseInput() => false;
        public bool GetAnyInput() => false;
    }
}
