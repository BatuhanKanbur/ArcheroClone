using System.Collections.Generic;
using System.Linq;
using Gameplay.Character.Enum;
using Gameplay.Character.Interface;
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
        public void UseSkill(ISkill skill)
        {
            if (_skills.Contains(skill)) return;
            _skills.Add(skill);
            skill.UseSkill(Character.Status,Character.CurrentWeapon as ISkillable);
        }

        public void CancelSkill(ISkill skill)
        {
            if (_skills.Contains(skill))
                _skills.Remove(skill);
            skill.RemoveSkill(Character.Status,Character.CurrentWeapon as ISkillable);
        }

        public CharacterSkillState GetSkillState(ISkill skill) => _skills.Contains(skill) ? CharacterSkillState.Using : CharacterSkillState.Usable;
    
        public void Update()
        {
            foreach (var skill in _skills.ToList())
            {
                skill.TickSkill();
                if (skill.IsFinished)
                    CancelSkill(skill);
            }
        }

        public void Reset() => Dispose();
        public void Dispose()
        {
            for (var i = _skills.Count - 1; i >= 0; i--)
                CancelSkill(_skills[i]);
        }
    }
}
