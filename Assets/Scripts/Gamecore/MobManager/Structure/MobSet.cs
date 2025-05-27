using UnityEngine;

namespace Gamecore.MobManager.Structure
{
    [CreateAssetMenu(fileName = "MobSet", menuName = "ScriptableObjects/Mob/MobSet")]
    public class MobSet : ScriptableObject
    {
        public int maxMobCount;
        public Vector2Int horizontalLimits, verticalLimits;
        public MobData[] mobs;
    }
}
