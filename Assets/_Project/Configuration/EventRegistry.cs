using UnityEngine;
using FlappyBird.Utilities.Events;

namespace FlappyBird.Configuration
{
    /// <summary>
    /// Central registry for all game events
    /// Create this as a ScriptableObject asset in Unity
    /// </summary>
    [CreateAssetMenu(fileName = "EventRegistry", menuName = "Flappy Bird/Configuration/Event Registry")]
    public class EventRegistry : ScriptableObject
    {
        [Header("Gameplay Events")]
        public GameEvent onGameStarted;
        public GameEvent onGameOver;
        public GameEvent onScoreChanged;
        
        [Header("Bird Events")]
        public GameEvent onBirdDied;
        public GameEvent onBirdJumped;
        
        [Header("UI Events")]
        public GameEvent onShowGameOver;
        public GameEvent onShowPause;
    }
}
