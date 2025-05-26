using System;
using Gameplay.Skill.Structure;

namespace Gamecore.MobManager.Interface
{
    public interface IMob : IDisposable
    {
        public void Initialize(StatsData statsData,Action<IMob> onDispose);
    }
}
