using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gameplay.Character.Interface;
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
        private MobStats _mobStats;
        public Transform Transform => transform;
        public int EarnedScore => _mobStats.EarnedScore;
        private readonly CancellationTokenSource _cts = new();

        public void Initialize(StatsData statsData,ITargetManager targetManager, Action<IMob> onDispose)
        {
            _mobStats = statsData as MobStats;
            TargetManager = targetManager;
            base.Initialize();
            _onMobDispose = onDispose;
            Status.OnDeath += OnDeath;
            Status.SetStats(statsData as ICharacterStats);
        }
        public new void Dispose()
        {
            base.Dispose();
            Status.OnDeath -= OnDeath;
            _onMobDispose = null;
            _cts?.Cancel();
            gameObject.SetActive(false);
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
            while (elapsedTime < duration && !_cts.IsCancellationRequested)
            {
                elapsedTime += 1;
                Status.OnHit(damageStats.Damage);
                await UniTask.Delay(1000 ,cancellationToken: _cts.Token);
            }
        }
    }
}
