using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ITargetManager
    {
        public Vector3[] GetClosetTargetPositions(Transform originTransform,int targetCount,float range);
    }
}
