using Cysharp.Threading.Tasks;
using Gamecore.UIManager.Interface;
using Gameplay.Character.Interface;

namespace Gameplay.Skill.Interface
{
    public interface ISkillManager
    {
        public UniTaskVoid InitSkills(ICharacter character,IUIManager uiManager);
    }
}
