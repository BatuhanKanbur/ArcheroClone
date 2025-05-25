using Gameplay.Skill.Structure;

namespace Gameplay.Skill.Interface
{
    public interface ISkillable
    {
        public void ApplySkill(StatsData[] skill);
        public void RemoveSkill(StatsData[] skill);
    }
}
