﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using UnityEngine;

namespace Gamecore.MobManager.Behaviours
{
    public class MobSpawner
    {
        private readonly IMobFactory _mobFactory;
        private readonly ISpawnPositionStrategy _spawnPositionStrategy;

        public MobSpawner()
        {
            _mobFactory = new DefaultMobFactory();
            _spawnPositionStrategy = new SpawnStrategy();
        }
        public MobSpawner(IMobFactory mobFactory, ISpawnPositionStrategy spawnPositionStrategy)
        {
            _mobFactory = mobFactory;
            _spawnPositionStrategy = spawnPositionStrategy;
        }

        public async UniTask<MobInstance[]> SpawnMobsAsync(int targetMobCount, MobSet mobSet,Vector3 origin)
        {
            targetMobCount = Mathf.Clamp(targetMobCount, 0, mobSet.maxMobCount);
            var spawnedMobs = new List<MobInstance>();
            var spawnedCount = 0;
            for (var i = 0; spawnedCount < targetMobCount; i++)
            {
                foreach (var mobType in mobSet.mobs)
                {
                    if(!mobType.IsSpawnable) continue;
                    var spawnPosition = mobSet.nonRendedSpawn ?
                        _spawnPositionStrategy.GetSpawnPosition(origin) :
                        _spawnPositionStrategy.GetSpawnPosition(mobSet.horizontalLimits, mobSet.verticalLimits);
                    var spawnedMob = await _mobFactory.CreateMobAsync(mobType, spawnPosition, Quaternion.identity);
                    spawnedMobs.Add(new MobInstance(spawnedMob.Item1, spawnedMob.Item2));
                    spawnedCount++;
                    if (spawnedCount >= targetMobCount)
                        break;
                }
            }
            return spawnedMobs.ToArray();
        }
    }

}
