using UnityEngine;

namespace FlappyBird.Core.Entities
{
    /// <summary>
    /// ============================================
    /// CORE ENTITY: Bird Data
    /// ============================================
    /// Pure data class representing the bird's state
    /// No Unity dependencies, no behavior - just data
    /// 
    /// SOLID: Single Responsibility
    /// - Only stores bird state data
    /// 
    /// This is NOT a MonoBehaviour - it's pure C#
    /// The actual Unity component will reference this
    /// ============================================
    /// </summary>
    public class Bird
    {
        // State
        public bool IsDead { get; private set; }
        public bool IsFlying { get; private set; }
        
        // Physics properties
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        
        // Stats
        public int JumpCount { get; private set; }
        public float FlightTime { get; private set; }
        public float DistanceTraveled { get; private set; }

        /// <summary>
        /// Initialize bird with default state
        /// </summary>
        public Bird()
        {
            Reset();
        }

        /// <summary>
        /// Reset bird to initial state
        /// </summary>
        public void Reset()
        {
            IsDead = false;
            IsFlying = false;
            Position = Vector2.zero;
            Velocity = Vector2.zero;
            Rotation = 0f;
            JumpCount = 0;
            FlightTime = 0f;
            DistanceTraveled = 0f;
        }

        /// <summary>
        /// Mark bird as dead
        /// </summary>
        public void Die()
        {
            IsDead = true;
            IsFlying = false;
        }

        /// <summary>
        /// Start flying (game started)
        /// </summary>
        public void StartFlying()
        {
            IsFlying = true;
        }

        /// <summary>
        /// Record a jump
        /// </summary>
        public void RecordJump()
        {
            JumpCount++;
        }

        /// <summary>
        /// Update flight statistics
        /// </summary>
        public void UpdateStats(float deltaTime, float speed)
        {
            if (IsFlying && !IsDead)
            {
                FlightTime += deltaTime;
                DistanceTraveled += speed * deltaTime;
            }
        }

        /// <summary>
        /// Get bird statistics as formatted string
        /// </summary>
        public string GetStatsString()
        {
            return $"Jumps: {JumpCount}, Flight Time: {FlightTime:F1}s, Distance: {DistanceTraveled:F1}";
        }
    }
}
