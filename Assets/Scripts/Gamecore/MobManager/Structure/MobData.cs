using Gameplay.Skill.Structure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gamecore.MobManager.Structure
{
    [CreateAssetMenu(fileName = "MobData", menuName = "ScriptableObjects/Mob/MobData")]
    public class MobData : ScriptableObject
    {
        [Range(0,100)] public int spawnChance;
        public AssetReference[] mobPrefabs;
        public StatsData[] mobStats;
        public bool IsSpawnable => IsExistMobRequipment && Random.Range(0, 100) < spawnChance;
        private bool IsExistMobRequipment => mobPrefabs.Length > 0 && mobStats.Length > 0;
        public AssetReference GetMobPrefab => mobPrefabs[Random.Range(0, mobPrefabs.Length)];
        public StatsData GetMobStats => mobStats[Random.Range(0, mobStats.Length)];
    }
}
