using UnityEngine;

namespace FlappyBird.Utilities
{
    /// <summary>
    /// Central constants repository for the game
    /// Eliminates magic numbers and improves maintainability
    /// SOLID: Single Responsibility - Only stores constants
    /// </summary>
    public static class GameConstants
    {
        // ====================================
        // BIRD PHYSICS CONSTANTS
        // ====================================
        public const float BIRD_JUMP_FORCE = 150f;
        public const float BIRD_ROTATION_SPEED = 0.05f;
        public const float BIRD_FORWARD_ROTATION = 20f;
        public const float BIRD_FALLING_ROTATION = -90f;
        public const float BIRD_ROTATION_LERP_STRENGTH = 7f;
        public const float BIRD_AUTO_FLAP_INTERVAL = 0.1f;
        public const float BIRD_AUTO_FLAP_THRESHOLD_Y = 0f;
        
        // ====================================
        // CAMERA / WORLD MOVEMENT
        // ====================================
        public const float CAMERA_SCROLL_SPEED = 0.01f;
        
        // ====================================
        // SPAWN SETTINGS
        // ====================================
        public const float PIPE_SPAWN_DISTANCE = 2f;
        public const float PIPE_MIN_Y_POSITION = -1.34f;
        public const float PIPE_MAX_Y_POSITION = 1.03f;
        public const int PIPES_PER_BATCH = 22;
        
        public const float BACKGROUND_SPAWN_DISTANCE = 4.5f;
        public const int BACKGROUNDS_PER_BATCH = 10;
        public const float BACKGROUND_CLEANUP_THRESHOLD = 4f;
        
        // ====================================
        // INITIAL POSITIONS
        // ====================================
        public static readonly Vector3 BIRD_INITIAL_LOCAL_POSITION = new Vector3(-0.93f, 0f, 10f);
        public static readonly Vector3 CAMERA_INITIAL_POSITION = new Vector3(0f, -0.22f, -10f);
        
        // ====================================
        // TAGS
        // ====================================
        public const string TAG_PIPE = "Pipe";
        public const string TAG_GROUND = "Ground";
        public const string TAG_PLAYER = "Player";
        
        // ====================================
        // SAVE KEYS
        // ====================================
        public const string SAVE_KEY_BEST_SCORE = "BestScore";
        public const string SAVE_KEY_SOUND_ENABLED = "SoundEnabled";
        
        // ====================================
        // UI ELEMENT NAMES
        // ====================================
        public const string UI_SCORE_TEXT_NAME = "ScoreTextWhite";
    }
}
