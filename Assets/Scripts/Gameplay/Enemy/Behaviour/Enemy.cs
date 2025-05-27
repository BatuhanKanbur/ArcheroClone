using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gamecore.AssetManager.Constants;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gamecore.ObjectManager;
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
        private CancellationTokenSource _cts = new();

        public void Initialize(StatsData statsData,ITargetManager targetManager, Action<IMob> onDispose)
        {
            _cts = new CancellationTokenSource();
            _mobStats = statsData as MobStats;
            TargetManager = targetManager;
            base.Initialize();
            _onMobDispose = onDispose;
            Status.OnDeath += OnDeath;
            Status.SetStats(statsData as ICharacterStats);
        }
        private void OnDeath()
        {
            _onMobDispose?.Invoke(this);
            _cts?.Cancel();
            Status.OnDeath -= OnDeath;
            _onMobDispose = null;
            DisableMob().Forget();
        }
        private async UniTaskVoid DisableMob()
        {
            await UniTask.Delay(2500);
            gameObject.SetActive(false);
        }

        public void TakeDamage(DamageStats damageStats)
        {
            Status.OnHit(damageStats.Damage);
            if(damageStats.BurnDuration> 0)
                DamageOverTime(damageStats, damageStats.BurnDuration).Forget();
        }
        public async UniTaskVoid DamageOverTime(DamageStats damageStats, float duration)
        {
            var elapsedTime = 0f;
            await ObjectManager.GetObject(AssetConstants.FireParticle, transform.position, Quaternion.identity);
            while (elapsedTime < duration && !_cts.IsCancellationRequested)
            {
                elapsedTime += 1;
                Status.OnHit(damageStats.Damage);
                await UniTask.Delay(1000 ,cancellationToken: _cts.Token);
            }
        }
    }
}
