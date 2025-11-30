using System.Collections.Generic;
using UnityEngine;
using FlappyBird.Core.Interfaces;
using FlappyBird.Configuration;

namespace FlappyBird.Infrastructure.Audio
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: Facade Pattern
    /// ============================================
    /// Centralizes all audio management
    /// Simplifies complex audio system into simple interface
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles audio playback and management
    /// 
    /// SOLID: Dependency Inversion
    /// - Implements IAudioService interface
    /// - Game logic depends on interface, not this class
    /// ============================================
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour, IAudioService
    {
        [Header("Configuration")]
        [SerializeField] private AudioConfig audioConfig;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private Dictionary<string, AudioClip> _sfxClips;
        private Dictionary<string, AudioClip> _musicClips;

        private bool _isMuted = false;

        private void Awake()
        {
            InitializeAudioSources();
            LoadAudioClips();
            ApplyConfiguredVolumes();
        }

        private void InitializeAudioSources()
        {
            // Create music source if not assigned
            if (musicSource == null)
            {
                GameObject musicObj = new GameObject("MusicSource");
                musicObj.transform.SetParent(transform);
                musicSource = musicObj.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }

            // Create SFX source if not assigned
            if (sfxSource == null)
            {
                GameObject sfxObj = new GameObject("SFXSource");
                sfxObj.transform.SetParent(transform);
                sfxSource = sfxObj.AddComponent<AudioSource>();
                sfxSource.loop = false;
                sfxSource.playOnAwake = false;
            }
        }

        private void LoadAudioClips()
        {
            _sfxClips = new Dictionary<string, AudioClip>();
            _musicClips = new Dictionary<string, AudioClip>();

            if (audioConfig == null)
            {
                return;
            }

            // Load SFX clips
            if (audioConfig.flapSound != null)
                _sfxClips.Add("flap", audioConfig.flapSound);
            
            if (audioConfig.hitSound != null)
                _sfxClips.Add("hit", audioConfig.hitSound);
            
            if (audioConfig.scoreSound != null)
                _sfxClips.Add("score", audioConfig.scoreSound);
            
            if (audioConfig.gameStartSound != null)
                _sfxClips.Add("gamestart", audioConfig.gameStartSound);
            
            if (audioConfig.gameOverSound != null)
                _sfxClips.Add("gameover", audioConfig.gameOverSound);

            // Load music clips
            if (audioConfig.menuMusic != null)
                _musicClips.Add("menu", audioConfig.menuMusic);
            
            if (audioConfig.gameplayMusic != null)
                _musicClips.Add("gameplay", audioConfig.gameplayMusic);
        }

        private void ApplyConfiguredVolumes()
        {
            if (audioConfig == null) return;

            SetMasterVolume(audioConfig.masterVolume);
            SetSFXVolume(audioConfig.sfxVolume);
            SetMusicVolume(audioConfig.musicVolume);
        }

        #region IAudioService Implementation

        public void PlaySFX(string soundName)
        {
            if (_isMuted) return;
            if (string.IsNullOrEmpty(soundName)) return;

            string key = soundName.ToLower();
            
            if (_sfxClips != null && _sfxClips.TryGetValue(key, out AudioClip clip))
            {
                sfxSource.PlayOneShot(clip);
            }
            // Silently ignore missing sounds - no warning needed
        }

        public void PlayMusic(string musicName)
        {
            if (_isMuted) return;
            if (string.IsNullOrEmpty(musicName)) return;

            string key = musicName.ToLower();
            
            if (_musicClips != null && _musicClips.TryGetValue(key, out AudioClip clip))
            {
                if (musicSource.clip == clip && musicSource.isPlaying)
                {
                    // Already playing this music
                    return;
                }

                musicSource.clip = clip;
                musicSource.Play();
            }
            // Silently ignore missing music
        }

        public void StopMusic()
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
        }

        public void SetMasterVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            AudioListener.volume = volume;
            
            if (audioConfig != null)
            {
                audioConfig.masterVolume = volume;
            }
        }

        public void SetSFXVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            sfxSource.volume = volume;
            
            if (audioConfig != null)
            {
                audioConfig.sfxVolume = volume;
            }
        }

        public void SetMusicVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            musicSource.volume = volume;
            
            if (audioConfig != null)
            {
                audioConfig.musicVolume = volume;
            }
        }

        public void SetMuted(bool muted)
        {
            _isMuted = muted;
            
            if (muted)
            {
                sfxSource.mute = true;
                musicSource.mute = true;
            }
            else
            {
                sfxSource.mute = false;
                musicSource.mute = false;
            }
        }

        #endregion

        #region Additional Helper Methods

        /// <summary>
        /// Pause currently playing music
        /// </summary>
        public void PauseMusic()
        {
            if (musicSource.isPlaying)
            {
                musicSource.Pause();
            }
        }

        /// <summary>
        /// Resume paused music
        /// </summary>
        public void ResumeMusic()
        {
            if (!musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.UnPause();
            }
        }

        /// <summary>
        /// Fade out music over time
        /// </summary>
        public void FadeOutMusic(float duration)
        {
            StartCoroutine(FadeOutCoroutine(duration));
        }

        /// <summary>
        /// Fade in music over time
        /// </summary>
        public void FadeInMusic(float duration)
        {
            StartCoroutine(FadeInCoroutine(duration));
        }

        private System.Collections.IEnumerator FadeOutCoroutine(float duration)
        {
            float startVolume = musicSource.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
                yield return null;
            }

            musicSource.volume = 0f;
            musicSource.Stop();
            musicSource.volume = startVolume;
        }

        private System.Collections.IEnumerator FadeInCoroutine(float duration)
        {
            float targetVolume = musicSource.volume;
            musicSource.volume = 0f;
            
            if (!musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.Play();
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(0f, targetVolume, elapsed / duration);
                yield return null;
            }

            musicSource.volume = targetVolume;
        }

        #endregion
    }
}
