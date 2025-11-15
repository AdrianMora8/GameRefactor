using System;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird.Core.Entities;

namespace FlappyBird.Gameplay.StateMachine
{
    /// <summary>
    /// ============================================
    /// DESIGN PATTERN: State Machine
    /// ============================================
    /// Manages game state transitions
    /// 
    /// SOLID: Single Responsibility
    /// - Only manages state transitions
    /// 
    /// SOLID: Open/Closed
    /// - New states can be added without modifying this class
    /// ============================================
    /// </summary>
    public class GameStateMachine
    {
        private readonly Dictionary<GameState, IGameState> _states;
        private IGameState _currentState;

        public GameState CurrentStateType => _currentState?.StateType ?? GameState.Menu;
        public IGameState CurrentState => _currentState;

        /// <summary>
        /// Event raised when state changes
        /// </summary>
        public event Action<GameState, GameState> OnStateChanged;

        public GameStateMachine()
        {
            _states = new Dictionary<GameState, IGameState>();
        }

        /// <summary>
        /// Register a state in the machine
        /// </summary>
        public void RegisterState(IGameState state)
        {
            if (!_states.ContainsKey(state.StateType))
            {
                _states.Add(state.StateType, state);
                Debug.Log($"[StateMachine] Registered state: {state.StateType}");
            }
            else
            {
                Debug.LogWarning($"[StateMachine] State {state.StateType} already registered");
            }
        }

        /// <summary>
        /// Change to a new state
        /// </summary>
        public void ChangeState(GameState newStateType)
        {
            if (!_states.TryGetValue(newStateType, out IGameState newState))
            {
                Debug.LogError($"[StateMachine] State {newStateType} not registered!");
                return;
            }

            // Same state, ignore
            if (_currentState == newState)
            {
                return;
            }

            GameState previousStateType = _currentState?.StateType ?? GameState.Menu;

            // Exit current state
            _currentState?.Exit();

            // Change state
            _currentState = newState;

            // Enter new state
            _currentState.Enter();

            // Raise event
            OnStateChanged?.Invoke(previousStateType, newStateType);

            Debug.Log($"[StateMachine] State changed: {previousStateType} → {newStateType}");
        }

        /// <summary>
        /// Initialize with a starting state
        /// </summary>
        public void Initialize(GameState startingState)
        {
            if (_states.TryGetValue(startingState, out IGameState state))
            {
                _currentState = state;
                _currentState.Enter();
                Debug.Log($"[StateMachine] Initialized with state: {startingState}");
            }
            else
            {
                Debug.LogError($"[StateMachine] Starting state {startingState} not registered!");
            }
        }

        /// <summary>
        /// Update current state
        /// </summary>
        public void Update()
        {
            _currentState?.Update();
        }

        /// <summary>
        /// Handle input for current state
        /// </summary>
        public void HandleInput()
        {
            _currentState?.HandleInput();
        }

        /// <summary>
        /// Check if currently in a specific state
        /// </summary>
        public bool IsInState(GameState stateType)
        {
            return CurrentStateType == stateType;
        }

        /// <summary>
        /// Get state by type
        /// </summary>
        public IGameState GetState(GameState stateType)
        {
            _states.TryGetValue(stateType, out IGameState state);
            return state;
        }
    }
}
