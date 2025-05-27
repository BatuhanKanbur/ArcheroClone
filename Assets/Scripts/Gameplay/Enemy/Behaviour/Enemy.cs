using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gamecore.AssetManager.Constants;
using Gamecore.MobManager.Enums;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gamecore.ObjectManager;
using Gameplay.Character.Interface;
using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Skill.Structure;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Enemy.Behaviour
{
    using Gameplay.Character.Behaviour;
    public class Enemy : Character, IDamageable, IMob
    {
        private Action<IMob> _onMobDispose;
        private MobStats _mobStats;
        [SerializeField] private NavMeshAgent navMeshAgent;
        public Transform Transform => transform;
        public int EarnedScore => _mobStats.EarnedScore;
        private CancellationTokenSource _cts = new();
        private Vector3 _currentInput;
        private Vector3 TargetPosition => TargetManager.GetClosetTargetPositions(transform,1,500)[0];
        private bool TargetIsInRange()
        {
            var targetPosition = TargetPosition;
            targetPosition.y = 0;
            var currentPosition = transform.position;
            currentPosition.y = 0;
            var distance = Vector3.Distance(targetPosition, currentPosition);
            return distance <= _mobStats.AttackRange;
        }
        public void Initialize(StatsData statsData,ITargetManager targetManager, Action<IMob> onDispose)
        {
            _mobStats = statsData as MobStats;
            _cts = new CancellationTokenSource();
            TargetManager = targetManager;
            base.Initialize();
            _onMobDispose = onDispose;
            Status.OnDeath += OnDeath;
            Status.SetStats(statsData as ICharacterStats);
            if (_mobStats?.mobType != MobType.Attacker) return;
            AlignToNearestNavMesh();
            navMeshAgent.stoppingDistance = _mobStats.AttackRange;
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
        }
        private void AlignToNearestNavMesh()
        {
            if (NavMesh.SamplePosition(transform.position, out var hit, 10, NavMesh.AllAreas))
                navMeshAgent.Warp(hit.position);
        }
        private void FixedUpdate()
        {
            if(_mobStats.mobType != MobType.Attacker) return;
            _currentInput = Vector3.Lerp(_currentInput,TargetIsInRange() ? Vector3.zero : GetMovementDirection(),Time.deltaTime);
            Move(_currentInput);
        }

        private Vector3 GetMovementDirection()
        {
            navMeshAgent.SetDestination(TargetPosition);
            var nextPos = navMeshAgent.steeringTarget;
            var toTarget = (nextPos - transform.position).normalized;
            var signedAngle = Vector3.SignedAngle(transform.forward, toTarget, Vector3.up);
            var inputX = Mathf.Clamp(signedAngle / 90f, -1f, 1f);
            var forwardDot = Vector3.Dot(transform.forward, toTarget);
            var inputY = Mathf.Clamp(forwardDot, -1f, 1f);
            var aiDirection = new Vector3(inputX, 0f, inputY).normalized;
            var aiTargetAngle = Mathf.Atan2(aiDirection.x, aiDirection.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            var aiDir = Quaternion.Euler(0f, aiTargetAngle, 0f) * Vector3.forward;
            return aiDir.normalized;
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
