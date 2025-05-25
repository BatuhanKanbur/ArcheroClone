using Gameplay.Skill.Structure;

namespace Gameplay.Skill.Interface
{
    public interface ISkill
    {
        public StatsData[] Modifiers { get; }
        public bool IsFinished { get; }
        public void UseSkill(params ISkillable[] targets);
        public void TickSkill();
        public void RemoveSkill(params ISkillable[] targets);
    }
}
