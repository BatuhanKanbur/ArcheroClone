using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ITargetManager
    {
        public Transform GetTargetTransform { get; }
        public Transform[] GetClosetTargetPositions(Transform originTransform,int targetCount,float range);
    }
}
