using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gamecore.MobManager.Interface
{
    public interface IMobManager
    {
        public UniTask SpawnMob();
        public void RemoveMob(IMob mob);
        public IMob GetClosetMob(Transform originPoint);
    }
}
