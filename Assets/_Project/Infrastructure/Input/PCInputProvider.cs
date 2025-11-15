using UnityEngine;
using FlappyBird.Core.Interfaces;

namespace FlappyBird.Infrastructure.Input
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Strategy Pattern
    /// ============================================
    /// PC Input Provider - Desktop implementation
    /// 
    /// Supports:
    /// - Spacebar for jump
    /// - Left mouse click for jump
    /// - Escape key for pause
    /// 
    /// SOLID: Open/Closed Principle
    /// - Can add new input methods without modifying this class
    /// ============================================
    /// </summary>
    public class PCInputProvider : IInputService
    {
        public bool GetJumpInput()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space) || 
                   UnityEngine.Input.GetMouseButtonDown(0);
        }

        public bool GetPauseInput()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Escape) ||
                   UnityEngine.Input.GetKeyDown(KeyCode.P);
        }

        public bool GetAnyInput()
        {
            return UnityEngine.Input.anyKeyDown || 
                   UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}
