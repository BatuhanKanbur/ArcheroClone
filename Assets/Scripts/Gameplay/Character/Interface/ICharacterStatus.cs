using System;
using Gameplay.Damageable.Interface;
using Gameplay.Skill.Interface;

namespace Gameplay.Character.Interface
{
    public interface ICharacterStatus : ICharacterComponent , ISkillable
    {
        public bool IsStunned { get; }
        public Action OnDeath { get; set; }
        public ICharacterStats Stats { get; }
        public IHealthBar HealthBar { get; }
        public void OnHit(float damage);
        public void SetStats(ICharacterStats stats);
    }
}
