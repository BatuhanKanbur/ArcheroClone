using Cysharp.Threading.Tasks;
using Gamecore.MobManager.Interface;
using Gamecore.MobManager.Structure;
using Gameplay.Skill.Structure;
using UnityEngine;
using static Gamecore.ObjectManager.ObjectManager;

namespace Gamecore.MobManager.Behaviours
{
    public class DefaultMobFactory : IMobFactory
    {
        public async UniTask<(IMob,StatsData)> CreateMobAsync(MobData mobData, Vector3 position, Quaternion rotation)
        {
            var obj = await GetObject(mobData.GetMobPrefab, position, rotation);
            obj.transform.SetParent(null);
            var mob = obj.GetComponent<IMob>();
            return (mob,mobData.GetMobStats);
        }
    }
}
