using System;
using Cysharp.Threading.Tasks;

namespace Gameplay.Skill.Interface
{
    public interface ISkillButton
    {
        public UniTaskVoid Init(ISkill skillData, Action<ISkill> onButtonCallback);
        public void UpdateCooldown();
    }
}
