﻿using System;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.AnimatorBehaviour.Structure;
using Gameplay.Character.Interface;
using Gameplay.Character.Structure;
using Gameplay.Damageable.Interface;
using Gameplay.Skill.Structure;
using static UnityEngine.Object;
using EventType = Gamecore.AnimatorBehaviour.Enums.EventType;

namespace Gameplay.Character.Behaviour
{
    public class CharacterStatus : ICharacterStatus
    {
        private float _health;
        public Action OnDeath { get; set; }
        public bool IsStunned { get; private set; }
        public bool IsDead => _health <= 0;
        public ICharacter Character { get; }
        public IHealthBar HealthBar { get; }
        public ICharacterStats Stats => _characterStats;
        private CharacterStats _characterStats;
        private int _clipHash;
        public CharacterStatus(ICharacter character, CharacterStats stats,IHealthBar healthBar)
        {
            Character = character;
            _characterStats = Instantiate(stats);
            _health = stats.Health;
            HealthBar = healthBar;
            HealthBar?.Init(_health);
            Character.Animation.Subscribe(AnimationType.Hit,OnHitEvent);
        }

        private void OnHitEvent(AnimatorEvent animationEvent)
        {
            switch (animationEvent.eventType)
            {
                case EventType.Start:
                    _clipHash = animationEvent.ClipHash;
                    IsStunned = true;
                    break;
                case EventType.Update:
                    break;
                case EventType.End when _clipHash == animationEvent.ClipHash:
                    IsStunned = false;
                    break;
            }
        }
        
        public void OnHit(float damage)
        {
            if (_health <= 0) return;
            _health -= damage;
            Character.Animation.SetHit();
            HealthBar?.SetHealth(_health, _characterStats.Health);
            if (!(_health <= 0)) return;
            HealthBar?.SetActive(false);
            OnDeath?.Invoke();
            Character.Animation.SetDie();
        }
        public void SetStats(ICharacterStats stats)
        {
            _characterStats = stats as CharacterStats;
            if (!_characterStats) return;
            _health = _characterStats.Health;
            HealthBar?.SetHealth(_health, _characterStats.Health);
        }
        public void ApplySkill(StatsData[] skillStats)
        {
            foreach (var modifier in skillStats) 
                if(modifier is CharacterStats playerStats)
                    _characterStats += playerStats;
        }
        public void RemoveSkill(StatsData[] skillStats)
        {
            foreach (var modifier in skillStats) 
                if(modifier is CharacterStats playerStats)
                    _characterStats -= playerStats;
        }
        public void Update() { }
        public void Reset()
        {
            IsStunned = false;
            _health = _characterStats.Health;
            HealthBar?.Reset();
        }
        public void Dispose()
        {
            Character.Animation.Unsubscribe(AnimationType.Hit,OnHitEvent);
        }
    }
}
