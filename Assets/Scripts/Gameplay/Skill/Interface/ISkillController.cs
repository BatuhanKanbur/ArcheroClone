using Gameplay.Character.Enum;

namespace Gameplay.Skill.Interface
{
    public interface ISkillController
    {
        public void UseSkill(ISkill skill);
        public void CancelSkill(ISkill skill);
        public CharacterSkillState GetSkillState(ISkill skill);
    }
}
