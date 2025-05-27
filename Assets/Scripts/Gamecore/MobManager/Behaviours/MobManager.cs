using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gamecore.GameManager.Interface;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gameplay.Character.Interface;
using UnityEngine;

namespace Gamecore.MobManager.Behaviours
{
    public class MobManager : MonoBehaviour, IMobManager, ITargetManager
    {
        [SerializeField] private MobSet mobArea;
        private readonly List<IMob> _mobs = new();
        private readonly CancellationTokenSource _cts = new();
        private MobSpawner _mobSpawner;
        private bool _firstSpawned;
        private ITargetManager _targetManager;
        private Action<IMob> _onMobDisposed;
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
                var targetMobCount = mobArea.maxMobCount - _mobs.Count;
                if (targetMobCount <= 0 || mobArea.mobs.Length <= 0) continue;
                var spawnedMobs = await _mobSpawner.SpawnMobsAsync(targetMobCount,mobArea);
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
            _mobs.Remove(mobObject);
            _onMobDisposed?.Invoke(mobObject);
            mobObject.Dispose();
        }

        public void RemoveMob(IMob mob)
        {
            if (!_mobs.Contains(mob)) return;
            _mobs.Remove(mob);
        }
        public Vector3[] GetClosetMobPositions(Transform originTransform, int targetCount)
        {
            var positions = new List<Vector3>();
            var visitedMobs = new HashSet<IMob>();
            var currentReferencePoint = originTransform.position;
            for (var i = 0; i <= targetCount; i++)
            {
                var closestMob = FindClosestMob(currentReferencePoint, visitedMobs);
                if (closestMob == null)
                    break;
                visitedMobs.Add(closestMob);
                var mobPosition = closestMob.Transform.position;
                mobPosition.y+=1.5f;
                positions.Add(mobPosition);
                currentReferencePoint = mobPosition;
            }
            return positions.ToArray();
        }
        private IMob FindClosestMob(Vector3 referencePoint, HashSet<IMob> excludedMobs)
        {
            IMob closest = null;
            var minDistance = Mathf.Infinity;
            foreach (var mob in _mobs)
            {
                if (excludedMobs.Contains(mob)) continue;
                var distance = Vector3.Distance(referencePoint, mob.Transform.position);
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closest = mob;
            }
            return closest;
        }
    }
}
