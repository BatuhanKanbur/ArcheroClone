using Gameplay.Skill.Enum;
using Gameplay.Skill.Structure;

namespace Gameplay.Skill.Interface
{
    public interface ISkillController
    {
        public void UseSkill(SkillData skill);
        public void CancelSkill(SkillData skill);
        public SkillState GetSkillState(SkillData skill);
    }
}
