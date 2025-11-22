using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FlappyBird.Core.Entities;

namespace FlappyBird.Infrastructure.Data
{
    /// <summary>
    /// ============================================
    /// REPOSITORY: Player Data Persistence
    /// ============================================
    /// Implementation of player data storage using PlayerPrefs
    /// 
    /// SOLID: Single Responsibility
    /// - Only handles player data persistence
    /// 
    /// PATTERN: Repository Pattern
    /// - Encapsulates data access logic
    /// ============================================
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private const string PLAYERS_KEY = "FlappyBird_Players";
        private Dictionary<string, Player> _players;

        public PlayerRepository()
        {
            LoadPlayers();
        }

        public Player GetPlayer(string playerName)
        {
            if (_players.TryGetValue(playerName, out Player player))
            {
                return player;
            }
            return null;
        }

        public List<Player> GetAllPlayers()
        {
            return _players.Values.ToList();
        }

        public void SavePlayer(Player player)
        {
            if (player == null)
            {
                Debug.LogWarning("[PlayerRepository] Attempted to save null player");
                return;
            }

            _players[player.Name] = player;
            SaveToPlayerPrefs();
            Debug.Log($"[PlayerRepository] Saved player: {player.Name} with score: {player.BestScore}");
        }

        public void DeletePlayer(string playerName)
        {
            if (_players.Remove(playerName))
            {
                SaveToPlayerPrefs();
                Debug.Log($"[PlayerRepository] Deleted player: {playerName}");
            }
        }

        public bool PlayerExists(string playerName)
        {
            return _players.ContainsKey(playerName);
        }

        public void ClearAll()
        {
            _players.Clear();
            PlayerPrefs.DeleteKey(PLAYERS_KEY);
            PlayerPrefs.Save();
            Debug.Log("[PlayerRepository] Cleared all player data");
        }

        private void LoadPlayers()
        {
            _players = new Dictionary<string, Player>();

            if (!PlayerPrefs.HasKey(PLAYERS_KEY))
            {
                Debug.Log("[PlayerRepository] No saved players found");
                return;
            }

            try
            {
                string json = PlayerPrefs.GetString(PLAYERS_KEY);
                PlayerDataList dataList = JsonUtility.FromJson<PlayerDataList>(json);

                if (dataList != null && dataList.players != null)
                {
                    foreach (var data in dataList.players)
                    {
                        var player = new Player(
                            data.name,
                            data.bestScore,
                            DateTime.Parse(data.lastPlayedDate)
                        );
                        _players[player.Name] = player;
                    }

                    Debug.Log($"[PlayerRepository] Loaded {_players.Count} players");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[PlayerRepository] Error loading players: {e.Message}");
                _players.Clear();
            }
        }

        private void SaveToPlayerPrefs()
        {
            try
            {
                var dataList = new PlayerDataList
                {
                    players = _players.Values.Select(p => new PlayerData
                    {
                        name = p.Name,
                        bestScore = p.BestScore,
                        lastPlayedDate = p.LastPlayedDate.ToString("o") // ISO 8601 format
                    }).ToList()
                };

                string json = JsonUtility.ToJson(dataList);
                PlayerPrefs.SetString(PLAYERS_KEY, json);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"[PlayerRepository] Error saving players: {e.Message}");
            }
        }

        // Helper classes for JSON serialization
        [Serializable]
        private class PlayerDataList
        {
            public List<PlayerData> players;
        }

        [Serializable]
        private class PlayerData
        {
            public string name;
            public int bestScore;
            public string lastPlayedDate;
        }
    }
}
