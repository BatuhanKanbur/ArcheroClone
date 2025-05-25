using System.Collections.Generic;
using System.Linq;
using Gameplay.Player.Interface;
using Gameplay.Skill.Enum;
using Gameplay.Skill.Interface;
using Gameplay.Skill.Structure;

namespace Gameplay.Player.Behaviour
{
    public class PlayerSkillController : IPlayerSkillController
    {
        public IPlayer Player { get; }
        private readonly List<ISkill> _skills = new();
        public PlayerSkillController(IPlayer player)
        {
            Player = player;
        }
        public void UseSkill(SkillData skill)
        {
            if (_skills.Contains(skill)) return;
            _skills.Add(skill);
            skill.UseSkill(Player.Status);
        }

        public void CancelSkill(SkillData skill)
        {
            if (_skills.Contains(skill))
                _skills.Remove(skill);
            skill.RemoveSkill(Player.Status);
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
