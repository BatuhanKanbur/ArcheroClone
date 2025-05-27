using System;
using Cysharp.Threading.Tasks;
using Gameplay.Character.Interface;
using UnityEngine;

namespace Gamecore.MobManager.Interface
{
    public interface IMobManager
    {
        public UniTask SpawnMobs(ITargetManager targetManager,Action<IMob> onMobDisposed);
        public void RemoveMob(IMob mob);
    }
}
