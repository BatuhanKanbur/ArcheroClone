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
            var newStats = CreateInstance<StatsData>();
            newStats.multiplier = a.multiplier + b.multiplier;
            return newStats;
        }
        public static StatsData operator -(StatsData a, StatsData b)
        {
            var newStats = CreateInstance<StatsData>();
            newStats.multiplier = a.multiplier + b.multiplier;
            return newStats;
        }
    }
}
