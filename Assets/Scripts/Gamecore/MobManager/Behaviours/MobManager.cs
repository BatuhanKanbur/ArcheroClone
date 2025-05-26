using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gamecore.AssetManager.Behaviour;
using Gamecore.AssetManager.Constants;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gameplay.Skill.Structure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static Gamecore.ObjectManager.ObjectManager;

namespace Gamecore.MobManager.Behaviours
{
    public class MobManager : MonoBehaviour, IMobManager
    {
        [SerializeField] private AssetReference playerPrefab;
        [SerializeField] private MobSet mobArea;
        private readonly List<IMob> _mobs = new List<IMob>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private MobSpawner _mobSpawner;
        private void Start()
        {
            _mobSpawner = new MobSpawner();
            _ = StartSpawning();
        }
        private async UniTaskVoid StartSpawning()
        {
            await SpawnPlayer();
            _ = SpawnMob();
        }

        private async UniTask SpawnPlayer()
        {
            var createdPlayer = await GetObject(playerPrefab,Vector3.zero,Quaternion.identity);
            // createdPlayer.GetComponent<>()
            createdPlayer.transform.SetParent(null);
        }

        public async UniTask SpawnMob()
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.Delay(1000, cancellationToken: _cts.Token);
                var targetMobCount = mobArea.maxMobCount - _mobs.Count;
                if (targetMobCount <= 0 || mobArea.mobs.Length <= 0) continue;
                var spawnedMobs = await _mobSpawner.SpawnMobsAsync(targetMobCount,mobArea);
                foreach (var spawnedMob in spawnedMobs)
                {
                    // spawnedMob.Item1.Init(MobType.Enemy,spawnedMob.Item2,this);
                    // _mobs.Add(spawnedMob.Item1);
                }
                Debug.Log(spawnedMobs.Length + " mobs spawned.");
            }
        }
        public void RemoveMob(IMob mob)
        {
            if (!_mobs.Contains(mob)) return;
            _mobs.Remove(mob);
        }

        public IMob GetClosetMob(Transform originPoint)
        {
            // var origin = originPoint.position;
            // var forward = originPoint.forward;
            // var attackRadius = currentWeapon.GetRange;
            // var attackAngle = currentWeapon.GetAngle;
            // IMob closetMob=null;
            // var closestDistance = float.MaxValue;
            // foreach (var mob in _mobs)
            // {
            //     var distance = Vector3.Distance(origin, mob.Damageable.Transform.position);
            //     if (mob.MobType == ignoredMobType) continue;
            //     if (distance > attackRadius) continue;
            //     if (!(distance < closestDistance)) continue;
            //     var directionToTarget = (mob.Damageable.Transform.position - origin).normalized;
            //     var angle = Vector3.Angle(forward, directionToTarget);
            //     if (!(angle < attackAngle / 2f)) continue;
            //     closestDistance = distance;
            //     closetMob = mob;
            // }
            // return closetMob;
            return null;
        }
    }
}
