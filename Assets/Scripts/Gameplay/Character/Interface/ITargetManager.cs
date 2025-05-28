using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ITargetManager
    {
        public Transform[] GetClosetTargetPositions(Transform originTransform,int targetCount,float range);
    }
}
