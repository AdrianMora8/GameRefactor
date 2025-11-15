# 📱 FASE 14: UI SYSTEM (MVP PATTERN)

## ✅ **COMPLETADO - CÓDIGO**

Sistema de UI modular usando el patrón **Model-View-Presenter (MVP)** para separar completamente la lógica de la visualización.

---

## 📂 **ESTRUCTURA CREADA**

```
Assets/_Project/UI/
├── Views/
│   ├── MainMenuView.cs         // Vista menú principal
│   ├── GameplayHUDView.cs      // Vista HUD en juego
│   └── GameOverView.cs         // Vista pantalla game over
├── Presenters/
│   ├── MainMenuPresenter.cs    // Lógica menú principal
│   ├── GameplayHUDPresenter.cs // Lógica HUD
│   └── GameOverPresenter.cs    // Lógica game over
└── Managers/
    └── UIManager.cs             // Coordinador central de UI
```

---

## 🎯 **PATRÓN MVP - SEPARACIÓN DE RESPONSABILIDADES**

### **View (Vista)**
- **Responsabilidad**: Solo visualización, NO lógica
- **Contiene**: Referencias a UI elements (Text, Button, Image)
- **Métodos**: Show(), Hide(), UpdateScore(), etc.
- **NO hace**: Decisiones de negocio, cálculos, validaciones

### **Presenter (Presentador)**
- **Responsabilidad**: Lógica de UI, coordina View
- **Contiene**: Lógica de botones, eventos, decisiones simples
- **NO hace**: Lógica de gameplay (eso es del GameFlowManager)

### **UIManager**
- **Responsabilidad**: Coordina todos los presenters
- **Patrón**: Facade - simplifica el acceso a UI
- **Conecta**: Presenters con GameFlowManager

---

## 🔧 **CONFIGURACIÓN EN UNITY**

### **PASO 1: Crear Canvas Principal**

1. Click derecho en Hierarchy → **UI → Canvas**
2. Renombrar a `UICanvas`
3. Configurar Canvas:
   - Render Mode: `Screen Space - Overlay`
   - Canvas Scaler: `Scale with Screen Size`
   - Reference Resolution: `1080 x 1920` (vertical) o `1920 x 1080` (horizontal)
   - Match: `0.5` (balance width/height)

---

### **PASO 2: Crear Main Menu View**

1. Crear objeto vacío dentro de `UICanvas`: **MainMenuView**
2. Agregar componente `MainMenuView.cs`
3. **Estructura**:
   ```
   MainMenuView
   ├── TitlePanel
   │   └── Title (TextMeshProUGUI) "FLAPPY BIRD"
   ├── PlayButton (Button)
   │   └── Text: "PLAY"
   ├── SettingsButton (Button - opcional)
   │   └── Text: "SETTINGS"
   └── QuitButton (Button - opcional)
       └── Text: "QUIT"
   ```
4. **Asignar en Inspector**:
   - Title Panel → campo `titlePanel`
   - Play Button → campo `playButton`
   - Settings Button → campo `settingsButton`
   - Quit Button → campo `quitButton`

---

### **PASO 3: Crear Gameplay HUD View**

1. Crear objeto vacío: **GameplayHUDView**
2. Agregar componente `GameplayHUDView.cs`
3. **Estructura**:
   ```
   GameplayHUDView
   ├── ScoreText (TextMeshProUGUI)
   │   └── Font Size: 72, Center Aligned
   └── GetReadyPanel
       ├── GetReadyText "GET READY"
       └── TapInstructionText "TAP TO FLY"
   ```
4. **Asignar en Inspector**:
   - Score Text → campo `scoreText`
   - Get Ready Panel → campo `getReadyPanel`

---

### **PASO 4: Crear Game Over View**

1. Crear objeto vacío: **GameOverView**
2. Agregar componente `GameOverView.cs`
3. **Estructura**:
   ```
   GameOverView
   ├── Background (Image - semi-transparente)
   ├── GameOverText "GAME OVER"
   ├── ScorePanel
   │   ├── CurrentScoreText
   │   ├── BestScoreText
   │   ├── NewBestIcon (Image - opcional)
   │   └── MedalImage (opcional)
   ├── RestartButton
   │   └── Text: "RESTART"
   └── MenuButton
       └── Text: "MENU"
   ```
4. **Asignar en Inspector**:
   - Current Score Text → campo `currentScoreText`
   - Best Score Text → campo `bestScoreText`
   - New Best Icon → campo `newBestIcon`
   - Medal Image → campo `medalImage`
   - Restart Button → campo `restartButton`
   - Menu Button → campo `menuButton`
5. **Sprites de Medallas** (opcional):
   - Bronze Medal → campo `bronzeMedal` (10+ puntos)
   - Silver Medal → campo `silverMedal` (20+ puntos)
   - Gold Medal → campo `goldMedal` (40+ puntos)

---

### **PASO 5: Crear UI Manager**

1. Crear GameObject vacío en la escena: **UIManager**
2. Agregar componente `UIManager.cs`
3. **Asignar referencias**:
   
   **Views:**
   - Main Menu View → arrastra `MainMenuView`
   - Gameplay HUD View → arrastra `GameplayHUDView`
   - Game Over View → arrastra `GameOverView`
   
   **Events** (buscar en Assets/_Project/Events):
   - onScoreChanged → `IntGameEvent`
   - onGameStarted → `GameEvent`
   - onGameOver → `GameEvent`

---

### **PASO 6: Conectar con GameFlowManager**

El UIManager ya está preparado para encontrar automáticamente al GameFlowManager con:
```csharp
_gameFlowManager = FindObjectOfType<GameFlowManager>();
```

**Asegurar que exista en la escena**:
- GameObject `GameFlowManager` debe estar presente
- Debe tener todos sus campos asignados (BirdController, PipeSpawner, etc.)

---

## 🎨 **ESTILO RECOMENDADO (UI)**

### **Fuente**
- Usar **TextMeshPro** (mejor que Text normal)
- Fuente: FlappyBirdy.ttf (si existe) o similar pixelada
- Outline para mayor contraste

### **Colores**
- Background: Semi-transparente negro (#000000, Alpha 180)
- Texto Principal: Blanco (#FFFFFF)
- Texto Secundario: Amarillo (#FFD700)
- Botones: Naranja (#FF6B35), Verde (#4CAF50)

### **Botones**
- Transition: Color Tint
- Normal: Color original
- Highlighted: +20% brillo
- Pressed: -20% brillo
- Disabled: 50% alpha

---

## 🔗 **FLUJO DE EVENTOS**

```
1. MENU STATE
   ┌─────────────┐
   │ MainMenuView│ (visible)
   │  - Play Btn │──┐
   └─────────────┘  │
                    ↓
   ┌──────────────────────┐
   │ MainMenuPresenter    │
   │ OnPlayRequested()    │
   └──────────────────────┘
                    ↓
   ┌──────────────────────┐
   │ UIManager            │
   │ HandlePlayRequested()│
   └──────────────────────┘
                    ↓
   ┌──────────────────────┐
   │ GameFlowManager      │
   │ StartGame()          │
   └──────────────────────┘

2. PLAYING STATE
   ┌─────────────────┐
   │ GameplayHUDView │ (visible)
   │  - Score: 0     │
   │  - Get Ready    │──┐
   └─────────────────┘  │
                        │ (score changes)
                        ↓
   ┌──────────────────────┐
   │ ScoreService         │
   │ fires onScoreChanged │
   └──────────────────────┘
                        ↓
   ┌────────────────────────┐
   │ GameplayHUDPresenter   │
   │ OnScoreChanged(int)    │
   └────────────────────────┘
                        ↓
   ┌─────────────────┐
   │ GameplayHUDView │
   │ UpdateScore(5)  │ → "5"
   └─────────────────┘

3. GAME OVER STATE
   ┌──────────────┐
   │ Bird Dies    │
   └──────────────┘
                ↓
   ┌──────────────────────┐
   │ GameFlowManager      │
   │ OnBirdDied()         │
   │ → EndGame()          │
   │ → fires onGameOver   │
   └──────────────────────┘
                ↓
   ┌──────────────────────┐
   │ UIManager            │
   │ OnGameOver()         │
   └──────────────────────┘
                ↓
   ┌──────────────────────┐
   │ GameOverView         │ (visible)
   │ Score: 5 | Best: 10  │
   │ [Restart] [Menu]     │
   └──────────────────────┘
```

---

## ✅ **VERIFICACIÓN**

### **Checklist de Configuración**

- [ ] Canvas creado y configurado
- [ ] MainMenuView con todos los elementos asignados
- [ ] GameplayHUDView con score text y get ready panel
- [ ] GameOverView con score displays y botones
- [ ] UIManager con todas las referencias asignadas
- [ ] GameFlowManager existe en escena
- [ ] Events (onScoreChanged, onGameStarted, onGameOver) asignados
- [ ] TextMeshPro importado (si no existe, Unity lo pedirá)

### **Test Funcional**

1. **Play Mode**:
   - Debe mostrarse Main Menu
   - Click Play → inicia juego
   - Get Ready panel desaparece al primer flap
   - Score incrementa al pasar pipes
   - Game Over muestra scores correctos
   - Restart reinicia juego
   - Menu vuelve al menú

2. **Consola**:
   ```
   [UIManager] Play requested
   [GameFlowManager] Game started!
   [UIManager] Game started - showing gameplay HUD
   [GameFlowManager] Score: 1
   [GameFlowManager] Game over!
   [UIManager] Game over - showing game over screen
   ```

---

## 🚨 **PROBLEMAS COMUNES**

### **Error: "TMP_Text not found"**
**Solución**: Importar TextMeshPro
- Window → TextMeshPro → Import TMP Essential Resources

### **Error: "NullReferenceException in UIManager"**
**Causa**: Falta asignar una View o un Event
**Solución**: Revisar Inspector del UIManager, todos los campos deben estar llenos

### **Score no actualiza**
**Causa**: onScoreChanged event no asignado
**Solución**: Asignar el ScriptableObject IntGameEvent al campo

### **Botones no responden**
**Causa**: Falta EventSystem
**Solución**: Unity lo crea automáticamente con el Canvas, verificar que exista

---

## 🎯 **BENEFICIOS DEL PATRÓN MVP**

1. **Separación clara**: View solo UI, Presenter solo lógica
2. **Testeable**: Presenters son C# puro, se pueden unit test
3. **Reutilizable**: Views se pueden cambiar sin tocar Presenters
4. **Mantenible**: Cada componente tiene una responsabilidad clara
5. **Escalable**: Fácil agregar nuevas pantallas (Pause, Settings, etc.)

---

## 📝 **SIGUIENTE FASE**

Una vez configurado todo en Unity:
- **Fase 15**: Integration & Polish
- **Fase 16**: Platform-specific Input Testing
- **Fase 17**: Cleanup (eliminar código viejo)
- **Fase 18**: Final Testing & Documentation

---

## 🔄 **INTEGRACIÓN CON SISTEMA ANTERIOR**

Por ahora **coexisten ambos sistemas**:
- ❌ Old: Game_Manager, FlappyBirdController, UI antigua
- ✅ New: GameFlowManager, BirdController, UIManager

**En Fase 17** eliminaremos completamente el sistema viejo.

---

**Estado**: ✅ Código completo - ⚙️ Requiere configuración en Unity
