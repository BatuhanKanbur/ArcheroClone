using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gameplay.Character.Interface;
using UnityEngine;

namespace Gamecore.MobManager.Behaviours
{
    public class MobManager : MonoBehaviour, IMobManager, ITargetManager
    {
        [SerializeField] private MobSet mobArea;
        public Transform GetTargetTransform => transform;
        private readonly List<IMob> _mobs = new();
        private readonly CancellationTokenSource _cts = new();
        private MobSpawner _mobSpawner;
        private bool _firstSpawned;
        private ITargetManager _targetManager;
        private Action<IMob> _onMobDisposed;
        private int _disposedCount;
        public async UniTask SpawnMobs(ITargetManager targetManager, Action<IMob> onMobDisposed)
        {
            _targetManager = targetManager;
            _onMobDisposed = onMobDisposed;
            _mobSpawner = new MobSpawner();
            while (!_cts.IsCancellationRequested)
            {
                if(_firstSpawned)
                    await UniTask.Delay(1000, cancellationToken: _cts.Token);
                else
                    await UniTask.Yield(cancellationToken: _cts.Token);
                var targetMobCount = (mobArea.maxMobCount + _disposedCount) - _mobs.Count;
                if (targetMobCount <= 0 || mobArea.mobs.Length <= 0) continue;
                var spawnOrigin = _targetManager.GetTargetTransform.position;
                var spawnedMobs = await _mobSpawner.SpawnMobsAsync(targetMobCount,mobArea,spawnOrigin);
                foreach (var spawnedMob in spawnedMobs)
                {
                    spawnedMob.Mob.Initialize(spawnedMob.Stats,_targetManager,OnMobDispose);
                    _mobs.Add(spawnedMob.Mob);
                }
                _firstSpawned = true;
            }
        }

        private void OnMobDispose(IMob mobObject)
        {
            _disposedCount++;
            _mobs.Remove(mobObject);
            _onMobDisposed?.Invoke(mobObject);
        }

        public void RemoveMob(IMob mob)
        {
            if (!_mobs.Contains(mob)) return;
            _mobs.Remove(mob);
        }
        public Transform[] GetClosetTargetPositions(Transform originTransform, int targetCount,float range)
        {
            var positions = new List<Transform>();
            var visitedMobs = new HashSet<IMob>();
            var currentReferencePoint = originTransform.position;
            for (var i = 0; i <= targetCount; i++)
            {
                var closestMob = FindClosestMob(currentReferencePoint,range, visitedMobs);
                if (closestMob == null)
                    break;
                visitedMobs.Add(closestMob);
                var mobPosition = closestMob.Transform.position;
                positions.Add(closestMob.Transform);
                currentReferencePoint = mobPosition;
            }
            return positions.ToArray();
        }
        private IMob FindClosestMob(Vector3 referencePoint,float range, HashSet<IMob> excludedMobs)
        {
            IMob closest = null;
            var minDistance = Mathf.Infinity;
            foreach (var mob in _mobs)
            {
                if (excludedMobs.Contains(mob)) continue;
                var distance = Vector3.Distance(referencePoint, mob.Transform.position);
                if (distance > range) continue;
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closest = mob;
            }
            return closest;
        }
    }
}
