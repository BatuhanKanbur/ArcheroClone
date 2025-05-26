using System.Collections.Generic;
using System.Linq;
using Gameplay.Character.Interface;
using Gameplay.Skill.Enum;
using Gameplay.Skill.Interface;
using Gameplay.Skill.Structure;

namespace Gameplay.Character.Behaviour
{
    public class CharacterSkillController : ICharacterSkillController
    {
        public ICharacter Character { get; }
        private readonly List<ISkill> _skills = new();
        public CharacterSkillController(ICharacter character)
        {
            Character = character;
        }
        public void UseSkill(SkillData skill)
        {
            if (_skills.Contains(skill)) return;
            _skills.Add(skill);
            skill.UseSkill(Character.Status);
        }

        public void CancelSkill(SkillData skill)
        {
            if (_skills.Contains(skill))
                _skills.Remove(skill);
            skill.RemoveSkill(Character.Status);
        }

        public SkillState GetSkillState(SkillData skill) => _skills.Contains(skill) ? SkillState.Using : SkillState.Usable;
    
        public void Update()
        {
            foreach (var skill in _skills.ToList())
            {
                skill.TickSkill();
                if (skill.IsFinished)
                    CancelSkill(skill as SkillData);
            }
        }

        public void Reset() => Dispose();
        public void Dispose()
        {
            foreach (var skill in _skills.ToList())
                CancelSkill(skill as SkillData);
        }
    }
}
