using Gameplay.Skill.Structure;
using UnityEngine.AddressableAssets;

namespace Gameplay.Skill.Interface
{
    public interface ISkill
    {
        public string Name { get; }
        public AssetReferenceSprite Icon { get; }
        public float GetCooldown { get; }
        public float Duration { get; }
        public float Timer { get; }
        public StatsData[] Modifiers { get; }
        public bool IsFinished { get; }
        public void UseSkill(params ISkillable[] targets);
        public void TickSkill();
        public void RemoveSkill(params ISkillable[] targets);
    }
}
