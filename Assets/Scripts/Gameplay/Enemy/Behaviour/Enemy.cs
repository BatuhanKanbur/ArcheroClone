using System;
using Cysharp.Threading.Tasks;
using Gamecore.MobManager.Interface;
using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gameplay.Enemy.Behaviour
{
    using Gameplay.Character.Behaviour;
    public class Enemy : Character, IDamageable, IMob
    {
        private Action<IMob> _onMobDispose;
        public void Initialize(StatsData statsData, Action<IMob> onDispose)
        {
            base.Initialize();
            _onMobDispose = onDispose;
            Status.OnDeath += OnDeath;
        }
        public new void Dispose()
        {
            Status.OnDeath -= OnDeath;
            _onMobDispose = null;
        }

        private void OnDeath() => _onMobDispose?.Invoke(this);
        public void TakeDamage(DamageStats damageStats)
        {
            Status.OnHit(damageStats.Damage);
            if(damageStats.BurnDuration> 0)
                DamageOverTime(damageStats, damageStats.BurnDuration).Forget();
        }
        public async UniTaskVoid DamageOverTime(DamageStats damageStats, float duration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += 1;
                Status.OnHit(damageStats.Damage);
                await UniTask.Delay(1000);
            }
        }
    }
}
