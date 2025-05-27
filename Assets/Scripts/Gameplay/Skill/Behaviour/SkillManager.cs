using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gamecore.AssetManager.Behaviour;
using Gamecore.AssetManager.Constants;
using Gamecore.UIManager.Interface;
using Gameplay.Character.Enum;
using Gameplay.Character.Interface;
using Gameplay.Skill.Interface;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gameplay.Skill.Behaviour
{
    public class SkillManager : MonoBehaviour,ISkillManager
    {
        private readonly Dictionary<ISkill,ISkillButton> _skills = new();
        private ICharacter _mainCharacter;
        private IUIManager _uiManager;
        public async UniTaskVoid InitSkills(ICharacter character,IUIManager uiManager)
        {
            _mainCharacter = character;
            _uiManager = uiManager;
            var skillUIButton = await AssetManager<GameObject>.LoadObject(AssetConstants.SkillUIButtonAddress);
            var skillScriptables = await AssetManager<SkillData>.LoadObjects(AssetConstants.SkillLabelAddress);
            foreach (var skillScriptable in skillScriptables)
            {
                var skillInstance = Instantiate(skillScriptable);
                var skillUIObject = Instantiate(skillUIButton, _uiManager.SkillContainer).GetComponent<ISkillButton>();
                skillUIObject.Init(skillInstance,OnSkillButtonClicked);
                _skills.Add(skillInstance, skillUIObject);
            }
            
        }
        public void Update()
        {
            foreach (var skill in _skills)
                skill.Value.UpdateCooldown();
        }
        private void OnSkillButtonClicked(ISkill skillData)
        {
            switch (_mainCharacter.SkillController.GetSkillState(skillData))
            {
                case CharacterSkillState.Using:
                    _mainCharacter.SkillController.CancelSkill(skillData);
                    break;
                case CharacterSkillState.Usable:
                    _mainCharacter.SkillController.UseSkill(skillData);
                    break;
            }
        }
    }
}
