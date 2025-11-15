using UnityEngine;
using System.Collections.Generic;

namespace FlappyBird.Infrastructure.Optimization
{
    /// <summary>
    /// ============================================
    /// PERFORMANCE MONITOR
    /// ============================================
    /// Tracks game performance metrics
    /// 
    /// METRICS:
    /// - FPS (Frames Per Second)
    /// - Frame time (milliseconds)
    /// - Memory usage
    /// - Object count
    /// 
    /// USAGE:
    /// Add to a GameObject in the scene
    /// Toggle with F12 key
    /// ============================================
    /// </summary>
    public class PerformanceMonitor : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool showOnStart = false;
        [SerializeField] private KeyCode toggleKey = KeyCode.F12;
        [SerializeField] private float updateInterval = 0.5f;

        // Display state
        private bool _showStats = false;

        // FPS tracking
        private float _fps = 0f;
        private float _frameTime = 0f;
        private int _frameCount = 0;
        private float _elapsed = 0f;

        // Memory tracking
        private float _memoryUsedMB = 0f;
        private float _memoryAllocatedMB = 0f;

        // Object tracking
        private int _totalObjects = 0;
        private int _activeObjects = 0;

        private void Start()
        {
            _showStats = showOnStart;
        }

        private void Update()
        {
            // Toggle display
            if (UnityEngine.Input.GetKeyDown(toggleKey))
            {
                _showStats = !_showStats;
            }

            if (!_showStats) return;

            // Update metrics
            _frameCount++;
            _elapsed += Time.unscaledDeltaTime;

            if (_elapsed >= updateInterval)
            {
                _fps = _frameCount / _elapsed;
                _frameTime = (_elapsed / _frameCount) * 1000f;
                
                UpdateMemoryStats();
                UpdateObjectStats();

                _frameCount = 0;
                _elapsed = 0f;
            }
        }

        private void UpdateMemoryStats()
        {
            _memoryUsedMB = System.GC.GetTotalMemory(false) / (1024f * 1024f);
            _memoryAllocatedMB = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f);
        }

        private void UpdateObjectStats()
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            _totalObjects = allObjects.Length;
            
            _activeObjects = 0;
            foreach (var obj in allObjects)
            {
                if (obj.activeInHierarchy)
                    _activeObjects++;
            }
        }

        private void OnGUI()
        {
            if (!_showStats) return;

            // Background panel
            GUI.Box(new Rect(10, 50, 250, 180), "");

            // Style
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 14;
            labelStyle.normal.textColor = Color.white;

            GUIStyle headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontSize = 16;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = Color.yellow;

            int yPos = 60;
            int lineHeight = 20;

            // Header
            GUI.Label(new Rect(20, yPos, 230, lineHeight), "⚡ PERFORMANCE STATS", headerStyle);
            yPos += lineHeight + 5;

            // FPS
            Color fpsColor = _fps >= 55 ? Color.green : (_fps >= 30 ? Color.yellow : Color.red);
            labelStyle.normal.textColor = fpsColor;
            GUI.Label(new Rect(20, yPos, 230, lineHeight), $"FPS: {_fps:F1}", labelStyle);
            yPos += lineHeight;

            // Frame Time
            labelStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(20, yPos, 230, lineHeight), $"Frame Time: {_frameTime:F2} ms", labelStyle);
            yPos += lineHeight;

            // Memory
            GUI.Label(new Rect(20, yPos, 230, lineHeight), $"Memory (GC): {_memoryUsedMB:F1} MB", labelStyle);
            yPos += lineHeight;

            GUI.Label(new Rect(20, yPos, 230, lineHeight), $"Memory (Unity): {_memoryAllocatedMB:F1} MB", labelStyle);
            yPos += lineHeight;

            // Objects
            GUI.Label(new Rect(20, yPos, 230, lineHeight), $"Objects: {_activeObjects}/{_totalObjects}", labelStyle);
            yPos += lineHeight;

            // Toggle hint
            labelStyle.fontSize = 12;
            labelStyle.normal.textColor = Color.gray;
            GUI.Label(new Rect(20, yPos, 230, lineHeight), $"[{toggleKey}] to toggle", labelStyle);
        }
    }
}
