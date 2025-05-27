using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ITargetManager
    {
        public Vector3[] GetClosetMobPositions(Transform originTransform,int targetCount,float range);
    }
}
