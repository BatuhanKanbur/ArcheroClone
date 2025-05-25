using System;
using System.Collections.Generic;
using Gameplay.Player.Interface;
using Gameplay.Player.Structure;
using Gameplay.Skill.Interface;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gameplay.Player.Behaviour
{
    public class PlayerStatus : IPlayerStatus
    {
        public IPlayer Player { get; }
        public IPlayerStats Stats => _playerStats;
        private PlayerStats _playerStats;
        public PlayerStatus(IPlayer player, PlayerStats stats)
        {
            Player = player;
            _playerStats = stats;
        }
        public void ApplySkill(StatsData[] skillStats)
        {
            foreach (var modifier in skillStats) 
                if(modifier is PlayerStats playerStats)
                    _playerStats += playerStats;
        }
        public void RemoveSkill(StatsData[] skillStats)
        {
            foreach (var modifier in skillStats) 
                if(modifier is PlayerStats playerStats)
                    _playerStats -= playerStats;
        }
        public void Update() { }
        public void Reset() { }
        public void Dispose() { }
    }
}
