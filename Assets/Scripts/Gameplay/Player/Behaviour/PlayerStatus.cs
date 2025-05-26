using System;
using System.Collections.Generic;
using Gameplay.Damageable.Interface;
using Gameplay.Player.Interface;
using Gameplay.Player.Structure;
using Gameplay.Skill.Interface;
using Gameplay.Skill.Structure;
using UnityEngine;
using static UnityEngine.Object;

namespace Gameplay.Player.Behaviour
{
    public class PlayerStatus : IPlayerStatus
    {
        private float _health;
        public Action OnDeath { get; set; }
        public IPlayer Player { get; }
        public IHealthBar HealthBar { get; }
        public IPlayerStats Stats => _playerStats;
        private PlayerStats _playerStats;
        public PlayerStatus(IPlayer player, PlayerStats stats,IHealthBar healthBar)
        {
            Player = player;
            _playerStats = Instantiate(stats);
            _health = stats.Health;
            HealthBar = healthBar;
            HealthBar?.Init(_health);
        }

        public void OnHit(float damage)
        {
            _health -= damage;
            Player.Animation.SetHit();
            HealthBar?.SetHealth(_health, _playerStats.Health);
            if(_health <= 0)
                OnDeath?.Invoke();
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

        public void Reset()
        {
            _health = _playerStats.Health;
            HealthBar?.Reset();
        }
        public void Dispose() { }
    }
}
