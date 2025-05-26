using Cysharp.Threading.Tasks;
using Gamecore.MobManager.Structure;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gamecore.MobManager.Interface
{
    public interface IMobFactory
    {
        UniTask<(IMob,StatsData)> CreateMobAsync(MobData mobData, Vector3 position, Quaternion rotation);
    }
}
