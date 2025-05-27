using System;
using Cysharp.Threading.Tasks;
using Gameplay.Character.Interface;

namespace Gamecore.MobManager.Interface
{
    public interface IMobManager
    {
        public UniTask SpawnMobs(ITargetManager targetManager,Action<IMob> onMobDisposed);
        public void RemoveMob(IMob mob);
    }
}
