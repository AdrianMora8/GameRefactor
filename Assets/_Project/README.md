# 🎮 Flappy Bird Refactored - Project Structure

## 📁 Architecture Overview

This project follows a **Clean Architecture Lite** pattern adapted for Unity, with emphasis on **SOLID principles** and **Design Patterns**.

---

## 🗂️ Folder Structure

```
Assets/_Project/
│
├── Core/                          # Business Logic (Framework-agnostic)
│   ├── Entities/                  # Pure data classes
│   ├── Interfaces/                # Contracts for services
│   └── Services/                  # Business logic services
│
├── Infrastructure/                # Framework implementations
│   ├── Audio/                     # Audio management
│   ├── DI/                        # Dependency Injection (Service Locator)
│   ├── Input/                     # Input handling (PC/Mobile)
│   ├── Pooling/                   # Object pooling system
│   └── Save/                      # Data persistence
│
├── Gameplay/                      # Game-specific MonoBehaviours
│   ├── Environment/               # Pipes, backgrounds, etc.
│   ├── Player/                    # Bird controller
│   └── StateMachine/              # Game state management
│
├── UI/                            # User Interface
│   ├── Presenters/                # UI logic (MVP pattern)
│   └── Views/                     # UI components (dumb views)
│
├── Configuration/                 # ScriptableObject configs
│   ├── GameConfig.cs
│   ├── BirdConfig.cs
│   ├── AudioConfig.cs
│   └── PoolConfig.cs
│
└── Utilities/                     # Shared utilities
    ├── Events/                    # Event system
    └── GameConstants.cs           # Constants repository
```

---

## 🎨 Design Patterns Applied

### 1. **Singleton + Service Locator** (Infrastructure/DI/)
- Centralized service access
- Better than static singletons everywhere

### 2. **Observer Pattern** (Utilities/Events/)
- Event-driven communication
- Decouples game systems

### 3. **Object Pool Pattern** (Infrastructure/Pooling/)
- Reuses pipes and backgrounds
- Eliminates GC spikes

### 4. **State Machine** (Gameplay/StateMachine/)
- Clean game state management
- Menu → Playing → Paused → GameOver

### 5. **Strategy Pattern** (Infrastructure/Input/)
- Platform-specific input (PC vs Mobile)
- Runtime-swappable strategies

### 6. **Facade Pattern** (Infrastructure/Audio/, Pooling/)
- Simplifies complex subsystems
- Single point of access

### 7. **MVP Pattern** (UI/)
- Separates UI logic from views
- Testable presentation layer

---

## ✅ SOLID Principles

### Single Responsibility
- Each class has ONE reason to change
- `BirdConfig` only stores bird settings
- `AudioManager` only handles audio

### Open/Closed
- Open for extension, closed for modification
- New input providers can be added without changing existing code

### Liskov Substitution
- `IInputProvider` implementations are interchangeable
- Strategy pattern enables this

### Interface Segregation
- Small, specific interfaces
- `IAudioService`, `IPoolService`, etc.

### Dependency Inversion
- High-level modules depend on abstractions
- Services registered via interfaces

---

## 🔄 Dependency Flow

```
UI/Presenters  ────────┐
                       ├──→  Core/Services  ──→  Core/Interfaces
Gameplay/      ────────┘              ↑
                                      │
Infrastructure/  ─────────────────────┘
                 (implements interfaces)
```

**Rule**: Dependencies point INWARD
- Core never depends on Infrastructure or UI
- Infrastructure implements Core interfaces
- UI and Gameplay consume services via interfaces

---

## 🚀 Getting Started

### Phase 1: Configuration ✅
- [x] Create folder structure
- [x] ScriptableObject configs
- [x] Constants class

### Phase 2: Event System (Next)
- [ ] GameEvent ScriptableObject
- [ ] GameEventListener component
- [ ] Specific events (OnScoreChanged, etc.)

### Phase 3-18: See main refactoring plan

---

## 📝 Naming Conventions

- **Classes**: PascalCase (`BirdController`)
- **Methods**: PascalCase (`StartGame()`)
- **Private fields**: _camelCase (`_audioService`)
- **Public fields/properties**: PascalCase (`JumpForce`)
- **Constants**: UPPER_SNAKE_CASE (`BIRD_JUMP_FORCE`)
- **Interfaces**: IPascalCase (`IAudioService`)

---

## 🔧 How to Create Config Assets

1. Right-click in Project window
2. Create → Flappy Bird → Configuration → [Type]
3. Configure values in Inspector
4. Reference in GameBootstrapper

---

## 📚 Resources

- Unity 2019.4.11f2
- Target: PC (Windows, Mac, Linux) + Mobile (Android, iOS)
- C# 7.3+

---

**Author**: Refactored with Clean Architecture principles
**Date**: November 2025
