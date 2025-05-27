using Gameplay.Skill.Interface;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Skill.Structure
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
    public class SkillData : SkillModifier, ISkill
    {
        [SerializeField] private StatsData[] statsModifiers;
        public AssetReferenceSprite skillIcon;
        private float _skillTimer;
        public string Name => skillName;
        public AssetReferenceSprite Icon => skillIcon;
        public float GetCooldown => skillDuration - (skillDuration - _skillTimer);
        public float Duration => skillDuration;
        public float Timer => _skillTimer;
        public bool IsFinished => _skillTimer >= skillDuration;
        public StatsData[] Modifiers => statsModifiers;
        public void UseSkill(params ISkillable[] targets)
        {
            foreach (var target in targets)
                target.ApplySkill(Modifiers);
        }
        public void TickSkill() => _skillTimer += Time.deltaTime;
        public void RemoveSkill(params ISkillable[] targets)
        {
            _skillTimer = 0;
            foreach (var target in targets)
                target.RemoveSkill(Modifiers);
        }
    }
}
