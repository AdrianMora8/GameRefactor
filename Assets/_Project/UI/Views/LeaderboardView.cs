using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FlappyBird.Core.Entities;

namespace FlappyBird.UI.Views
{
    /// <summary>
    /// ============================================
    /// VIEW: Leaderboard Screen
    /// ============================================
    /// Displays top players and their scores
    /// 
    /// DESIGN PATTERN: MVP (View)
    /// - Only UI, no logic
    /// ============================================
    /// </summary>
    public class LeaderboardView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Transform rowsContainer;
        [SerializeField] private GameObject rowPrefab;

        [Header("Buttons")]
        [SerializeField] private Button replayButton;
        [SerializeField] private Button exitButton;

        [Header("Settings")]
        [SerializeField] private int maxRowsToShow = 10;

        // Pool of instantiated rows
        private List<GameObject> _instantiatedRows = new List<GameObject>();

        /// <summary>
        /// Get replay button (for presenter)
        /// </summary>
        public Button GetReplayButton() => replayButton;

        /// <summary>
        /// Get exit button (for presenter)
        /// </summary>
        public Button GetExitButton() => exitButton;

        /// <summary>
        /// Show leaderboard with player data
        /// </summary>
        public void Show(List<Player> players)
        {
            gameObject.SetActive(true);
            Clear();
            Populate(players);
        }

        /// <summary>
        /// Hide leaderboard
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Clear all rows
        /// </summary>
        public void Clear()
        {
            foreach (var row in _instantiatedRows)
            {
                if (row != null)
                {
                    Destroy(row);
                }
            }
            _instantiatedRows.Clear();
        }

        /// <summary>
        /// Populate leaderboard with players
        /// </summary>
        private void Populate(List<Player> players)
        {
            if (rowsContainer == null)
            {
                Debug.LogError("[LeaderboardView] Rows container not assigned!");
                return;
            }

            int count = Mathf.Min(players.Count, maxRowsToShow);

            for (int i = 0; i < count; i++)
            {
                var player = players[i];
                CreateRow(i + 1, player.Name, player.BestScore);
            }

            // If no players, show message
            if (players.Count == 0)
            {
                CreateEmptyMessage();
            }
        }

        /// <summary>
        /// Create a single row
        /// </summary>
        private void CreateRow(int rank, string playerName, int score)
        {
            GameObject row;

            if (rowPrefab != null)
            {
                row = Instantiate(rowPrefab, rowsContainer);
            }
            else
            {
                // Fallback: create simple text row
                row = new GameObject($"Row_{rank}");
                row.transform.SetParent(rowsContainer, false);
                
                var text = row.AddComponent<TextMeshProUGUI>();
                text.text = $"{rank}. {playerName} - {score}";
                text.fontSize = 24;
                text.alignment = TextAlignmentOptions.Left;
            }

            // Try to find and set text components in prefab
            var texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length >= 3)
            {
                // Assuming order: Rank, Name, Score
                texts[0].text = rank.ToString();
                texts[1].text = playerName;
                texts[2].text = score.ToString();
            }
            else if (texts.Length >= 2)
            {
                // Assuming order: Name, Score
                texts[0].text = $"{rank}. {playerName}";
                texts[1].text = score.ToString();
            }
            else if (texts.Length == 1)
            {
                texts[0].text = $"{rank}. {playerName} - {score}";
            }

            _instantiatedRows.Add(row);
        }

        /// <summary>
        /// Show empty message when no players
        /// </summary>
        private void CreateEmptyMessage()
        {
            var row = new GameObject("EmptyMessage");
            row.transform.SetParent(rowsContainer, false);
            
            var text = row.AddComponent<TextMeshProUGUI>();
            text.text = "No scores yet. Be the first!";
            text.fontSize = 20;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.gray;

            _instantiatedRows.Add(row);
        }

        /// <summary>
        /// Set title text
        /// </summary>
        public void SetTitle(string title)
        {
            if (titleText != null)
            {
                titleText.text = title;
            }
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}
