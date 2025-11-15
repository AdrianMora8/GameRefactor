# SOLUCIONES A PROBLEMAS - FASE 13

## 🔧 PROBLEMAS Y SOLUCIONES

### 1. BACKGROUND SOBREPUESTO (Ciudad encima de pipes)

**En Unity:**
1. Selecciona el sprite del **background (ciudad)**
2. En **Sprite Renderer**:
   - **Sorting Layer**: Default
   - **Order in Layer**: `-10` (lo pone muy atrás)

3. Selecciona los **Pipes** (en el prefab)
4. En **Sprite Renderer** de PipeTop y PipeBottom:
   - **Sorting Layer**: Default
   - **Order in Layer**: `0` (encima del background)

5. Selecciona el **Bird**
6. En **Sprite Renderer**:
   - **Sorting Layer**: Default
   - **Order in Layer**: `10` (encima de todo)

---

### 2. PIPES MUY SEPARADOS (Gap demasiado grande)

**En Unity:**
1. Abre el prefab **Pipe** (doble clic en `Assets/_Project/Prefabs/Pipe`)
2. Selecciona **PipeTop**:
   - **Position Y**: Cambia de `4` a `2.5`
3. Selecciona **PipeBottom**:
   - **Position Y**: Cambia de `-4` a `-2.5`
4. Ahora el gap será de **5 unidades** (más razonable)
5. Ajusta **ScoreZone**:
   - **Box Collider 2D → Size Y**: `2` (para que cubra el gap)

---

### 3. NO HAY COLISIÓN CON PIPES

**Verifica en Unity:**

**En el Prefab Pipe:**
1. Selecciona **PipeTop** y **PipeBottom**
2. Verifica que tengan:
   - ✓ **Box Collider 2D** agregado
   - ✓ **Is Trigger**: DESACTIVADO (sin check)
   - ✓ **Tag**: `Pipe`

**En el Bird:**
1. Selecciona el **Bird**
2. Verifica que tenga:
   - ✓ **Rigidbody2D** con **Body Type**: Dynamic
   - ✓ **Collider2D** (Circle o Box)
   - ✓ **Tag**: `Player`
3. En **Rigidbody2D**:
   - **Simulated**: Debe estar ACTIVADO
   - **Gravity Scale**: Mayor a 0

---

### 4. BOTÓN PLAY VIEJO INTERFIERE

**Solución Temporal:**

Opción A - Desactivar UI vieja:
1. En Hierarchy, busca **StartScreenCanvas**
2. Desactívalo (checkbox a la izquierda)

Opción B - Desactivar Game Manager viejo:
1. En Hierarchy, busca **Game Manager**
2. Desactívalo

**NOTA**: En Fase 17 eliminaremos TODO el código viejo. Por ahora es normal que coexistan.

---

### 5. SCORE DUPLICADO (Centro vs Debug)

**Normal** - hay 2 sistemas activos:
- Sistema viejo: Muestra score en centro
- Sistema nuevo: Muestra debug arriba izquierda

**Solución Temporal:**

Puedes ocultar el debug UI:
1. Selecciona **GameFlowManager**
2. En Inspector, desmarca **Show Debug UI**

O desactiva el Canvas viejo del score:
1. Busca **InGameCanvas** (o similar)
2. Desactívalo

---

## 📋 PASOS A SEGUIR AHORA

1. **Arregla Sorting Layers** (background atrás)
2. **Ajusta gap de pipes** (PipeTop Y=2.5, PipeBottom Y=-2.5)
3. **Verifica colliders** en pipes y bird
4. **Desactiva StartScreenCanvas** (UI vieja)
5. **Oculta Debug UI** si quieres (Show Debug UI = false)

---

## ⚠️ IMPORTANTE

Los problemas de UI vieja (botones, score centro, etc.) son **ESPERADOS**.

**Estamos en transición**:
- Sistema VIEJO: Todavía activo
- Sistema NUEVO: Funcionando en paralelo

En **Fase 17 (Cleanup)** eliminaremos TODO el código viejo y solo quedará el nuevo sistema limpio.

Por ahora, **desactiva los canvas viejos** para evitar confusión.
