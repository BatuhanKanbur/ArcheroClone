using Gamecore.MobManager.Interface;
using Gameplay.Skill.Structure;

namespace Gamecore.MobManager.Structure
{
    public record MobInstance(IMob Mob, StatsData Stats)
    {
        public IMob Mob { get; } = Mob;
        public StatsData Stats { get; } = Stats;
    }
}
