using Gameplay.Player.Structure;
using Gameplay.Skill.Interface;

namespace Gameplay.Player.Interface
{
    public interface IPlayerStatus : IPlayerComponent , ISkillable
    {
        public IPlayerStats Stats { get; }
    }
}
