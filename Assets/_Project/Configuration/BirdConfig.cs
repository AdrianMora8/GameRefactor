using UnityEngine;

namespace FlappyBird.Configuration
{
    /// <summary>
    /// ============================================
    /// SCRIPTABLE OBJECT: Bird Configuration
    /// ============================================
    /// Contains all bird-specific settings
    /// Separated from GameConfig for better organization
    /// SOLID: Single Responsibility - Only bird settings
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "BirdConfig", menuName = "Flappy Bird/Configuration/Bird Config")]
    public class BirdConfig : ScriptableObject
    {
        [Header("Movement")]
        [Tooltip("Upward force applied when jumping")]
        [Range(100f, 300f)]
        public float jumpForce = 150f;
        
        [Tooltip("How fast the bird moves forward (camera scroll speed)")]
        [Range(0.005f, 0.02f)]
        public float moveSpeed = 0.01f;

        [Header("Physics")]
        [Tooltip("Gravity scale applied to bird")]
        [Range(0.5f, 3f)]
        public float gravityScale = 1f;
        
        [Header("Rotation")]
        [Tooltip("Rotation angle when jumping (in degrees)")]
        [Range(0f, 45f)]
        public float forwardRotationAngle = 20f;
        
        [Tooltip("Rotation angle when falling (in degrees)")]
        [Range(-90f, 0f)]
        public float fallingRotationAngle = -90f;
        
        [Tooltip("Speed of rotation interpolation")]
        [Range(0.01f, 0.1f)]
        public float rotationSpeed = 0.05f;
        
        [Tooltip("Strength of rotation lerp on jump")]
        [Range(1f, 10f)]
        public float rotationLerpStrength = 7f;
        
        [Header("Auto-Flap (Menu State)")]
        [Tooltip("Interval between auto-flaps in menu")]
        [Range(0.05f, 0.5f)]
        public float autoFlapInterval = 0.1f;
        
        [Tooltip("Y position threshold to trigger auto-flap")]
        public float autoFlapThresholdY = 0f;
        
        [Header("Initial Setup")]
        [Tooltip("Bird's initial local position")]
        public Vector3 initialLocalPosition = new Vector3(-0.93f, 0f, 10f);
        
        [Tooltip("Camera's initial position")]
        public Vector3 cameraInitialPosition = new Vector3(0f, -0.22f, -10f);
    }
}
