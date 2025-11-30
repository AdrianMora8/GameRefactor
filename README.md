# 🎮 Flappy Parrot Flying - Proyecto Refactorizado

## 📋 Información del Proyecto

| Aspecto          | Detalle                                         |
| ---------------- | ----------------------------------------------- |
| **Motor**        | Unity 2019.4.41f2                               |
| **Lenguaje**     | C# 7.3+                                         |
| **Plataformas**  | PC (Windows, Mac, Linux) + Móvil (Android, iOS) |
| **Arquitectura** | Clean Architecture Lite + MVP                   |
| **Fecha**        | Noviembre 2025                                  |

---

## 📁 Estructura del Proyecto

```
Assets/_Project/
│
├── Configuration/                    # ScriptableObjects de configuración
│   ├── AudioConfig.cs               # Configuración de audio
│   ├── BirdConfig.cs                # Configuración del pájaro
│   ├── DifficultyConfig.cs          # Niveles de dificultad
│   ├── EventRegistry.cs             # Registro central de eventos
│   ├── GameConfig.cs                # Configuración general del juego
│   └── PoolConfig.cs                # Configuración del Object Pool
│
├── Core/                             # Lógica de negocio (agnóstico de Unity)
│   ├── Entities/                     # Entidades de datos puros
│   │   ├── Bird.cs                  # Entidad del pájaro
│   │   ├── DifficultyLevel.cs       # Entidad de nivel de dificultad
│   │   ├── GameState.cs             # Enumeración de estados
│   │   ├── Player.cs                # Entidad del jugador
│   │   └── ScoreData.cs             # Datos de puntuación
│   │
│   ├── Interfaces/                   # Contratos (interfaces)
│   │   ├── IAudioService.cs         # Servicio de audio
│   │   ├── IInputService.cs         # Servicio de entrada
│   │   ├── IPoolService.cs          # Servicio de pooling
│   │   └── ISaveService.cs          # Servicio de guardado
│   │
│   ├── Services/                     # Servicios de lógica de negocio
│   │   └── ScoreService.cs          # Gestión de puntuación
│   │
│   └── UseCases/                     # Casos de uso (extensible)
│
├── Debugging/                        # Herramientas de depuración
│   └── CollisionDebugger.cs         # Depurador de colisiones
│
├── Gameplay/                         # MonoBehaviours específicos del juego
│   ├── Bird/                         # Sistema del pájaro
│   │   ├── BirdController.cs        # Lógica del pájaro
│   │   └── BirdView.cs              # Visuales del pájaro
│   │
│   ├── Effects/                      # Sistema de efectos visuales
│   │   ├── CameraShake.cs           # Sacudida de cámara
│   │   ├── EffectsManager.cs        # Coordinador de efectos
│   │   ├── FloatingText.cs          # Texto flotante (+1)
│   │   ├── FloatingTextSpawner.cs   # Spawner de texto flotante
│   │   ├── ParticleManager.cs       # Gestor de partículas
│   │   └── ScreenFlash.cs           # Flash de pantalla
│   │
│   ├── Environment/                  # Elementos del entorno
│   │   ├── BackgroundScaler.cs      # Escalador de fondo
│   │   ├── BackgroundScroller.cs    # Scroll de fondo
│   │   ├── GroundScroller.cs        # Scroll del suelo
│   │   ├── Pipe.cs                  # Lógica de tubería
│   │   ├── PipeSpawner.cs           # Generador de tuberías
│   │   └── ScoreZone.cs             # Zona de puntuación
│   │
│   ├── Managers/                     # Managers del juego
│   │   ├── DifficultyManager.cs     # Gestión de dificultad
│   │   └── GameFlowManager.cs       # Orquestador principal
│   │
│   └── StateMachine/                 # Máquina de estados
│       ├── IGameState.cs            # Interfaz de estado
│       ├── GameStateMachine.cs      # Máquina de estados
│       ├── MenuState.cs             # Estado: Menú
│       ├── PlayingState.cs          # Estado: Jugando
│       ├── PausedState.cs           # Estado: Pausado
│       └── GameOverState.cs         # Estado: Game Over
│
├── Infrastructure/                   # Implementaciones de framework
│   ├── Audio/                        # Sistema de audio
│   │   ├── AudioManager.cs          # Gestor de audio
│   │   └── AudioEventBridge.cs      # Puente eventos-audio
│   │
│   ├── Bootstrapping/                # Inicialización
│   │   └── GameBootstrapper.cs      # Inicializador del juego
│   │
│   ├── Data/                         # Capa de datos
│   │   ├── IPlayerRepository.cs     # Interfaz del repositorio
│   │   └── PlayerRepository.cs      # Repositorio de jugadores
│   │
│   ├── DI/                           # Inyección de dependencias
│   │   └── ServiceLocator.cs        # Localizador de servicios
│   │
│   ├── Input/                        # Sistema de entrada
│   │   ├── InputManager.cs          # Gestor de entrada
│   │   ├── PCInputProvider.cs       # Proveedor PC (teclado/mouse)
│   │   ├── MobileInputProvider.cs   # Proveedor móvil (touch)
│   │   └── NullInputProvider.cs     # Proveedor nulo (Null Object Pattern)
│   │
│   ├── Optimization/                 # Optimizaciones
│   │   └── GenericObjectPool.cs     # Pool genérico
│   │
│   ├── Pooling/                      # Sistema de Object Pool
│   │   ├── IPoolable.cs             # Interfaz pooleable
│   │   ├── ObjectPool.cs            # Pool de objetos
│   │   └── PoolManager.cs           # Gestor de pools
│   │
│   ├── Save/                         # Sistema de guardado
│   │   ├── GameData.cs              # Estructura de datos
│   │   └── SaveService.cs           # Servicio de guardado
│   │
│   └── Services/                     # Servicios de infraestructura
│       └── PlayerService.cs         # Servicio de jugadores
│
├── Prefabs/                          # Prefabs del juego
│   ├── FloatingText.prefab          # Prefab texto +1
│   ├── LeaderboardRow.prefab        # Fila del leaderboard
│   ├── Pipe.prefab                  # Prefab de tubería
│   └── bg.prefab                    # Prefab de fondo
│
├── Sprites/                          # Imágenes y sprites
│
├── UI/                               # Sistema de interfaz de usuario
│   ├── Managers/                     # Managers de UI
│   │   └── UIManager.cs             # Coordinador de UI
│   │
│   ├── Presenters/                   # Presenters (lógica de UI)
│   │   ├── GameOverPresenter.cs     # Presenter Game Over
│   │   ├── GameplayHUDPresenter.cs  # Presenter HUD
│   │   ├── LeaderboardPresenter.cs  # Presenter Leaderboard
│   │   ├── MainMenuPresenter.cs     # Presenter Menú Principal
│   │   └── PlayerRegistrationPresenter.cs  # Presenter Registro
│   │
│   └── Views/                        # Views (vistas tontas)
│       ├── GameOverView.cs          # Vista Game Over
│       ├── GameplayHUDView.cs       # Vista HUD
│       ├── LeaderboardView.cs       # Vista Leaderboard
│       ├── MainMenuView.cs          # Vista Menú Principal
│       └── PlayerRegistrationView.cs # Vista Registro
│
└── Utilities/                        # Utilidades compartidas
    ├── AutoReturnToPool.cs          # Auto-retorno al pool
    ├── GameConstants.cs             # Constantes del juego
    └── Events/                       # Sistema de eventos
        ├── GameEvent.cs             # Evento genérico
        ├── GameEventListener.cs     # Listener de eventos
        ├── IntGameEvent.cs          # Evento con int
        ├── IntGameEventListener.cs  # Listener con int
        └── [Assets de eventos .asset]
```

---

## 🎨 Patrones de Diseño Implementados

### 1. **Service Locator** (Inyección de Dependencias)

| Archivo                                            | Descripción                                     |
| -------------------------------------------------- | ----------------------------------------------- |
| `Infrastructure/DI/ServiceLocator.cs`              | Registro y resolución centralizada de servicios |
| `Infrastructure/Bootstrapping/GameBootstrapper.cs` | Registra todos los servicios al inicio          |

**Uso:**

```csharp
// Registrar servicio
ServiceLocator.Register<IAudioService>(audioManager);

// Obtener servicio
var audio = ServiceLocator.Get<IAudioService>();
```

---

### 2. **Observer Pattern** (Sistema de Eventos)

| Archivo                                    | Descripción                      |
| ------------------------------------------ | -------------------------------- |
| `Utilities/Events/GameEvent.cs`            | ScriptableObject evento genérico |
| `Utilities/Events/GameEventListener.cs`    | Componente que escucha eventos   |
| `Utilities/Events/IntGameEvent.cs`         | Evento que pasa un entero        |
| `Infrastructure/Audio/AudioEventBridge.cs` | Conecta eventos con audio        |
| `Gameplay/Effects/EffectsManager.cs`       | Escucha eventos para efectos     |

**Eventos disponibles:**

- `OnBirdFlap` - Cuando el pájaro aletea
- `OnBirdDied` - Cuando el pájaro muere
- `OnBirdCollision` - Colisión detectada
- `OnScoreChanged` - Puntuación cambia
- `OnGameStarted` - Juego inicia
- `OnPauseToggled` - Pausa activada/desactivada

---

### 3. **State Machine** (Máquina de Estados)

| Archivo                                     | Descripción        |
| ------------------------------------------- | ------------------ |
| `Gameplay/StateMachine/IGameState.cs`       | Interfaz de estado |
| `Gameplay/StateMachine/GameStateMachine.cs` | Máquina de estados |
| `Gameplay/StateMachine/MenuState.cs`        | Estado Menú        |
| `Gameplay/StateMachine/PlayingState.cs`     | Estado Jugando     |
| `Gameplay/StateMachine/PausedState.cs`      | Estado Pausado     |
| `Gameplay/StateMachine/GameOverState.cs`    | Estado Game Over   |

**Flujo de estados:**

```
Menu → Playing → Paused (opcional) → GameOver → Menu
```

---

### 4. **Strategy Pattern** (Estrategia de Input)

| Archivo                                       | Descripción                      |
| --------------------------------------------- | -------------------------------- |
| `Core/Interfaces/IInputService.cs`            | Interfaz de entrada              |
| `Infrastructure/Input/InputManager.cs`        | Gestor que selecciona estrategia |
| `Infrastructure/Input/PCInputProvider.cs`     | Estrategia PC (Space/Click)      |
| `Infrastructure/Input/MobileInputProvider.cs` | Estrategia Móvil (Touch)         |
| `Infrastructure/Input/NullInputProvider.cs`   | Estrategia Nula                  |

**Intercambio en runtime:**

```csharp
// Detecta automáticamente la plataforma
if (Application.isMobilePlatform)
    _inputProvider = new MobileInputProvider();
else
    _inputProvider = new PCInputProvider();
```

---

### 5. **Object Pool Pattern**

| Archivo                                   | Descripción                      |
| ----------------------------------------- | -------------------------------- |
| `Infrastructure/Pooling/IPoolable.cs`     | Interfaz para objetos pooleables |
| `Infrastructure/Pooling/ObjectPool.cs`    | Pool genérico de objetos         |
| `Infrastructure/Pooling/PoolManager.cs`   | Gestor de múltiples pools        |
| `Gameplay/Environment/PipeSpawner.cs`     | Usa el pool para tuberías        |
| `Gameplay/Effects/FloatingTextSpawner.cs` | Usa pool para texto +1           |

**Beneficio:** Elimina picos de Garbage Collection al reutilizar objetos.

---

### 6. **MVP Pattern** (Model-View-Presenter)

| Capa          | Archivos                                                              |
| ------------- | --------------------------------------------------------------------- |
| **Model**     | `Core/Entities/Player.cs`, `Core/Services/ScoreService.cs`            |
| **View**      | `UI/Views/MainMenuView.cs`, `LeaderboardView.cs`, etc.                |
| **Presenter** | `UI/Presenters/MainMenuPresenter.cs`, `LeaderboardPresenter.cs`, etc. |

**Flujo:**

```
Usuario → View (detecta click) → Presenter (lógica) → Model (datos) → View (actualiza UI)
```

---

### 7. **Facade Pattern**

| Archivo                                 | Descripción                     |
| --------------------------------------- | ------------------------------- |
| `Infrastructure/Audio/AudioManager.cs`  | Fachada para todo el audio      |
| `Gameplay/Managers/GameFlowManager.cs`  | Fachada para el flujo del juego |
| `UI/Managers/UIManager.cs`              | Fachada para toda la UI         |
| `Infrastructure/Pooling/PoolManager.cs` | Fachada para el pooling         |

**Simplifica:** Subsistemas complejos detrás de una interfaz simple.

---

### 8. **Repository Pattern**

| Archivo                                    | Descripción                     |
| ------------------------------------------ | ------------------------------- |
| `Infrastructure/Data/IPlayerRepository.cs` | Interfaz del repositorio        |
| `Infrastructure/Data/PlayerRepository.cs`  | Implementación con PlayerPrefs  |
| `Infrastructure/Services/PlayerService.cs` | Servicio que usa el repositorio |

**Abstrae:** El acceso a datos del almacenamiento concreto.

---

### 9. **Singleton Pattern**

| Archivo                                   | Descripción                                   |
| ----------------------------------------- | --------------------------------------------- |
| `Infrastructure/DI/ServiceLocator.cs`     | Instancia única del localizador               |
| `Gameplay/Effects/CameraShake.cs`         | Acceso global: `CameraShake.Instance`         |
| `Gameplay/Effects/ScreenFlash.cs`         | Acceso global: `ScreenFlash.Instance`         |
| `Gameplay/Effects/ParticleManager.cs`     | Acceso global: `ParticleManager.Instance`     |
| `Gameplay/Effects/FloatingTextSpawner.cs` | Acceso global: `FloatingTextSpawner.Instance` |

---

### 10. **Null Object Pattern**

| Archivo                                     | Descripción                |
| ------------------------------------------- | -------------------------- |
| `Infrastructure/Input/NullInputProvider.cs` | Proveedor que no hace nada |

**Evita:** Checks de null en todo el código.

---

## ✅ Principios SOLID Aplicados

### S - Single Responsibility (Responsabilidad Única)

| Archivo                   | Responsabilidad Única        |
| ------------------------- | ---------------------------- |
| `BirdController.cs`       | Solo lógica del pájaro       |
| `BirdView.cs`             | Solo visuales del pájaro     |
| `ScoreService.cs`         | Solo gestión de puntuación   |
| `AudioManager.cs`         | Solo reproducción de audio   |
| `SaveService.cs`          | Solo persistencia de datos   |
| `PipeSpawner.cs`          | Solo generación de tuberías  |
| `ScoreZone.cs`            | Solo detección de puntuación |
| `DifficultyManager.cs`    | Solo gestión de dificultad   |
| `LeaderboardView.cs`      | Solo mostrar el leaderboard  |
| `LeaderboardPresenter.cs` | Solo lógica del leaderboard  |

---

### O - Open/Closed (Abierto/Cerrado)

| Implementación      | Archivo               | Cómo aplica                                      |
| ------------------- | --------------------- | ------------------------------------------------ |
| Nuevos estados      | `IGameState.cs`       | Agregar estados sin modificar `GameStateMachine` |
| Nuevos inputs       | `IInputService.cs`    | Agregar proveedores sin modificar `InputManager` |
| Nuevos eventos      | `GameEvent.cs`        | Crear nuevos eventos sin modificar el sistema    |
| Nuevas dificultades | `DifficultyConfig.cs` | Agregar niveles en el ScriptableObject           |

---

### L - Liskov Substitution (Sustitución de Liskov)

| Interfaz            | Implementaciones Intercambiables                              |
| ------------------- | ------------------------------------------------------------- |
| `IInputService`     | `PCInputProvider`, `MobileInputProvider`, `NullInputProvider` |
| `IAudioService`     | `AudioManager`                                                |
| `ISaveService`      | `SaveService`                                                 |
| `IPoolService`      | `PoolManager`                                                 |
| `IGameState`        | `MenuState`, `PlayingState`, `PausedState`, `GameOverState`   |
| `IPlayerRepository` | `PlayerRepository`                                            |

---

### I - Interface Segregation (Segregación de Interfaces)

| Interfaz           | Métodos Específicos                                      |
| ------------------ | -------------------------------------------------------- |
| `IAudioService.cs` | `PlaySFX()`, `PlayMusic()`, `StopMusic()`, `SetVolume()` |
| `IInputService.cs` | `GetJumpInput()`, `GetPauseInput()`                      |
| `ISaveService.cs`  | `Save()`, `Load()`, `Delete()`                           |
| `IPoolService.cs`  | `Get()`, `Return()`, `PreWarm()`                         |
| `IPoolable.cs`     | `OnSpawn()`, `OnDespawn()`                               |
| `IGameState.cs`    | `Enter()`, `Update()`, `HandleInput()`, `Exit()`         |

---

### D - Dependency Inversion (Inversión de Dependencias)

| Módulo Alto Nivel    | Depende de          | NO de                      |
| -------------------- | ------------------- | -------------------------- |
| `GameFlowManager`    | `IAudioService`     | `AudioManager`             |
| `BirdController`     | `IInputService`     | `PCInputProvider`          |
| `ScoreService`       | `ISaveService`      | `SaveService`              |
| `PlayerService`      | `IPlayerRepository` | `PlayerRepository`         |
| Todos los Presenters | Interfaces          | Implementaciones concretas |

**Registro en `GameBootstrapper.cs`:**

```csharp
ServiceLocator.Register<IAudioService>(audioManager);
ServiceLocator.Register<IInputService>(inputManager);
ServiceLocator.Register<ISaveService>(saveService);
```

---

## 🔄 Flujo de Dependencias

```
┌─────────────────────────────────────────────────────────────────┐
│                        CAPA DE PRESENTACIÓN                      │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────────┐  │
│  │ UI/Views    │  │UI/Presenters│  │ Gameplay/Managers       │  │
│  │             │←─│             │  │ (GameFlowManager)       │  │
│  └─────────────┘  └──────┬──────┘  └───────────┬─────────────┘  │
│                          │                      │                │
└──────────────────────────┼──────────────────────┼────────────────┘
                           │                      │
                           ▼                      ▼
┌─────────────────────────────────────────────────────────────────┐
│                        CAPA DE DOMINIO                           │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐  │
│  │ Core/Entities   │  │ Core/Services   │  │ Core/Interfaces │  │
│  │ (Bird, Player)  │  │ (ScoreService)  │  │ (IAudioService) │  │
│  └─────────────────┘  └─────────────────┘  └────────┬────────┘  │
│                                                      │           │
└──────────────────────────────────────────────────────┼───────────┘
                                                       │
                                                       ▼
┌─────────────────────────────────────────────────────────────────┐
│                     CAPA DE INFRAESTRUCTURA                      │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────────┐   │
│  │Infrastructure│  │Infrastructure│  │ Infrastructure       │   │
│  │ /Audio       │  │ /Input       │  │ /Data, /Save, /DI    │   │
│  │(AudioManager)│  │(InputManager)│  │(PlayerRepository)    │   │
│  └──────────────┘  └──────────────┘  └──────────────────────┘   │
│                                                                  │
│                    IMPLEMENTA INTERFACES DE CORE                 │
└─────────────────────────────────────────────────────────────────┘
```

**Regla Principal:** Las dependencias apuntan hacia ADENTRO (hacia Core).

- Core **nunca** depende de Infrastructure o UI
- Infrastructure **implementa** interfaces de Core
- UI y Gameplay **consumen** servicios vía interfaces

---

## 🎮 Características del Juego

| Característica            | Descripción                            |
| ------------------------- | -------------------------------------- |
| **Registro de Jugador**   | Sistema de registro con nombre         |
| **Leaderboard**           | Top 10 jugadores con persistencia      |
| **Sistema de Dificultad** | 4 niveles que ciclan cada 10 puntos    |
| **Audio Dinámico**        | Sonidos de flap, hit, score            |
| **Efectos Visuales**      | Camera shake, screen flash, partículas |
| **Texto Flotante**        | "+1" al pasar tuberías                 |
| **Multi-plataforma**      | Input adaptativo PC/Móvil              |
| **Object Pooling**        | Optimización de memoria                |

---

## 📝 Convenciones de Código

| Tipo            | Formato     | Ejemplo          |
| --------------- | ----------- | ---------------- |
| Clases          | PascalCase  | `BirdController` |
| Métodos         | PascalCase  | `StartGame()`    |
| Campos privados | \_camelCase | `_audioService`  |
| Propiedades     | PascalCase  | `JumpForce`      |
| Constantes      | UPPER_SNAKE | `MAX_SCORE`      |
| Interfaces      | IPascalCase | `IAudioService`  |

---

## 🚀 Cómo Ejecutar

1. Abrir el proyecto en **Unity 2019.4.41f2**
2. Abrir la escena `Assets/Scenes/Main.unity`
3. Presionar **Play**

---

## 👤 Autor

Proyecto refactorizado aplicando principios de Clean Architecture, SOLID y patrones de diseño de software.

**Fecha:** Noviembre 2025
