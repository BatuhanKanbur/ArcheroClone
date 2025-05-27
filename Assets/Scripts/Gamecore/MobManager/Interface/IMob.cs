using System;
using Gameplay.Character.Interface;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gamecore.MobManager.Interface
{
    public interface IMob : IDisposable
    {
        public Transform Transform { get; }
        public int EarnedScore { get; }
        public void Initialize(StatsData statsData,ITargetManager player,Action<IMob> onDispose);
    }
}
