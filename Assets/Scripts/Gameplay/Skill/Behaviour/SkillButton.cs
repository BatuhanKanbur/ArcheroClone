using System;
using Cysharp.Threading.Tasks;
using Gamecore.AssetManager.Behaviour;
using Gameplay.Skill.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Skill.Behaviour
{
    public class SkillButton : MonoBehaviour,ISkillButton
    {
        [SerializeField] private Image iconImage,cooldownImage;
        [SerializeField] private Text skillNameText;
        [SerializeField] private Button skillButton;
        private Action<ISkill> _onButtonCallback;
        private ISkill _skill;
        public async UniTaskVoid Init(ISkill skillData,Action<ISkill> onButtonCallback)
        {
            _skill = skillData;
            iconImage.sprite = await AssetManager<Sprite>.LoadObject(skillData.Icon);
            skillNameText.text = skillData.Name;
            _onButtonCallback = onButtonCallback;
            skillButton.onClick.AddListener(OnButtonClick);
        }

        public void UpdateCooldown()
        {
            cooldownImage.fillAmount = _skill.Timer > 0 ? 1 -_skill.Timer / _skill.Duration : 0;
        }

        private void OnDestroy()
        {
            skillButton.onClick.RemoveAllListeners();
        }

        private void OnButtonClick()
        {
            _onButtonCallback?.Invoke(_skill);
        }
    }
}
