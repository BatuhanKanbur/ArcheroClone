using System;
using Gameplay.Character.Interface;
using Gameplay.Character.Structure;
using Gameplay.Damageable.Interface;
using Gameplay.Skill.Structure;
using static UnityEngine.Object;

namespace Gameplay.Character.Behaviour
{
    public class CharacterStatus : ICharacterStatus
    {
        private float _health;
        public Action OnDeath { get; set; }
        public ICharacter Character { get; }
        public IHealthBar HealthBar { get; }
        public ICharacterStats Stats => _characterStats;
        private CharacterStats _characterStats;
        public CharacterStatus(ICharacter character, CharacterStats stats,IHealthBar healthBar)
        {
            Character = character;
            _characterStats = Instantiate(stats);
            _health = stats.Health;
            HealthBar = healthBar;
            HealthBar?.Init(_health);
        }

        public void OnHit(float damage)
        {
            _health -= damage;
            Character.Animation.SetHit();
            HealthBar?.SetHealth(_health, _characterStats.Health);
            if(_health <= 0)
                OnDeath?.Invoke();
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
            _health = _characterStats.Health;
            HealthBar?.Reset();
        }
        public void Dispose() { }
    }
}
