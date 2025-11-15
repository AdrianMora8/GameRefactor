using UnityEngine;

namespace FlappyBird.Utilities.Events
{
    /// <summary>
    /// ============================================
    /// EVENT REGISTRY
    /// ============================================
    /// Central ScriptableObject containing references to all game events
    /// Allows easy access to events without hard references
    /// Acts as a registry/catalog of all game events
    /// ============================================
    /// </summary>
    [CreateAssetMenu(fileName = "EventRegistry", menuName = "Flappy Bird/Events/Event Registry")]
    public class EventRegistry : ScriptableObject
    {
        [Header("Gameplay Events")]
        [Tooltip("Raised when the game starts (player first input)")]
        public GameEvent onGameStarted;

        [Tooltip("Raised when the bird dies")]
        public GameEvent onBirdDied;

        [Tooltip("Raised when returning to main menu")]
        public GameEvent onReturnToMenu;

        [Header("Score Events")]
        [Tooltip("Raised when player passes through a pipe")]
        public IntGameEvent onScoreChanged;

        [Tooltip("Raised when a new best score is achieved")]
        public IntGameEvent onNewBestScore;

        [Header("UI Events")]
        [Tooltip("Raised when pause button is pressed")]
        public GameEvent onPauseToggled;

        [Tooltip("Raised when game over screen should be shown")]
        public GameEvent onShowGameOver;

        [Header("Audio Events")]
        [Tooltip("Raised when bird jumps/flaps")]
        public GameEvent onBirdFlap;

        [Tooltip("Raised when bird collides with obstacle")]
        public GameEvent onBirdCollision;

        [Tooltip("Raised when passing through score zone")]
        public GameEvent onScoreSound;

        [Header("System Events")]
        [Tooltip("Raised when settings change")]
        public GameEvent onSettingsChanged;
    }
}
