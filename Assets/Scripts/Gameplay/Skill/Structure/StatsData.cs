using UnityEngine;

namespace Gameplay.Skill.Structure
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "ScriptableObjects/StatsData", order = 1)]
    public class StatsData : ScriptableObject
    {
        [SerializeField] protected int multiplier;
        protected StatsData(int multiplier)
        {
            this.multiplier = multiplier;
        }
        public static StatsData operator +(StatsData a, StatsData b)
        {
            return new StatsData(
                a.multiplier + b.multiplier
            );
        }
        public static StatsData operator -(StatsData a, StatsData b)
        {
            return new StatsData(
                a.multiplier - b.multiplier
            );
        }
    }
}
