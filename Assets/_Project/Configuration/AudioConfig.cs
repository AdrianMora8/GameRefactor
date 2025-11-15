using UnityEngine;

namespace FlappyBird.Configuration
{
    /// <summary>
    /// ============================================
    /// SCRIPTABLE OBJECT: Audio Configuration
    /// ============================================
    /// Contains all audio clips and settings
    /// SOLID: Single Responsibility - Only audio data
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Flappy Bird/Configuration/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        [Header("Sound Effects")]
        [Tooltip("Sound played when bird jumps/flaps")]
        public AudioClip flapSound;
        
        [Tooltip("Sound played when bird hits pipe or ground")]
        public AudioClip hitSound;
        
        [Tooltip("Sound played when passing through a pipe")]
        public AudioClip scoreSound;
        
        [Tooltip("Sound played when game starts")]
        public AudioClip gameStartSound;
        
        [Tooltip("Sound played on game over")]
        public AudioClip gameOverSound;
        
        [Header("Background Music")]
        [Tooltip("Main menu background music")]
        public AudioClip menuMusic;
        
        [Tooltip("Gameplay background music")]
        public AudioClip gameplayMusic;
        
        [Header("Volume Settings")]
        [Tooltip("Master volume multiplier")]
        [Range(0f, 1f)]
        public float masterVolume = 1f;
        
        [Tooltip("Sound effects volume")]
        [Range(0f, 1f)]
        public float sfxVolume = 1f;
        
        [Tooltip("Music volume")]
        [Range(0f, 1f)]
        public float musicVolume = 0.5f;
    }
}
