using System;
using Gameplay.Damageable.Interface;
using Gameplay.Player.Structure;
using Gameplay.Skill.Interface;

namespace Gameplay.Player.Interface
{
    public interface IPlayerStatus : IPlayerComponent , ISkillable
    {
        public Action OnDeath { get; set; }
        public IPlayerStats Stats { get; }
        public IHealthBar HealthBar { get; }
        public void OnHit(float damage);
    }
}
