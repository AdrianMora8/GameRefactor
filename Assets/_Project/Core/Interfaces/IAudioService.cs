namespace FlappyBird.Core.Interfaces
{
    /// <summary>
    /// ============================================
    /// INTERFACE: Audio Service
    /// ============================================
    /// Contract for audio management
    /// 
    /// SOLID: Interface Segregation
    /// - Small, focused interface
    /// - Only audio-related methods
    /// 
    /// SOLID: Dependency Inversion
    /// - High-level modules depend on this interface
    /// - AudioManager implements this interface
    /// ============================================
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Play a sound effect by name
        /// </summary>
        void PlaySFX(string soundName);

        /// <summary>
        /// Play background music
        /// </summary>
        void PlayMusic(string musicName);

        /// <summary>
        /// Stop current music
        /// </summary>
        void StopMusic();

        /// <summary>
        /// Set master volume (0-1)
        /// </summary>
        void SetMasterVolume(float volume);

        /// <summary>
        /// Set SFX volume (0-1)
        /// </summary>
        void SetSFXVolume(float volume);

        /// <summary>
        /// Set music volume (0-1)
        /// </summary>
        void SetMusicVolume(float volume);

        /// <summary>
        /// Mute/unmute all audio
        /// </summary>
        void SetMuted(bool muted);
    }
}
