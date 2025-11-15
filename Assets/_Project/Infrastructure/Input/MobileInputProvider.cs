using UnityEngine;
using FlappyBird.Core.Interfaces;

namespace FlappyBird.Infrastructure.Input
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Strategy Pattern
    /// ============================================
    /// Mobile Input Provider - Touch implementation
    /// 
    /// Supports:
    /// - Single tap for jump
    /// - Two-finger tap for pause
    /// 
    /// SOLID: Liskov Substitution Principle
    /// - Can be swapped with PCInputProvider seamlessly
    /// ============================================
    /// </summary>
    public class MobileInputProvider : IInputService
    {
        public bool GetJumpInput()
        {
            // Check for single touch that just began
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                return touch.phase == TouchPhase.Began;
            }
            
            // Fallback to mouse for testing in editor
            #if UNITY_EDITOR
            return UnityEngine.Input.GetMouseButtonDown(0);
            #else
            return false;
            #endif
        }

        public bool GetPauseInput()
        {
            // Two-finger tap for pause
            if (UnityEngine.Input.touchCount == 2)
            {
                Touch touch1 = UnityEngine.Input.GetTouch(0);
                Touch touch2 = UnityEngine.Input.GetTouch(1);
                
                return touch1.phase == TouchPhase.Began || 
                       touch2.phase == TouchPhase.Began;
            }
            
            return false;
        }

        public bool GetAnyInput()
        {
            // Any touch counts as input
            if (UnityEngine.Input.touchCount > 0)
            {
                return UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began;
            }
            
            // Fallback for editor
            #if UNITY_EDITOR
            return UnityEngine.Input.GetMouseButtonDown(0);
            #else
            return false;
            #endif
        }
    }
}
