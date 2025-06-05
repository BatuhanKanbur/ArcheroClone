using System;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.AnimatorBehaviour.Structure;
using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ICharacterAnimator : ICharacterComponent
    {
        public Animator Animator { get;}
        public Transform LeftHand { get; }
        public Transform RightHand { get; }
        public Transform Spine { get; }
        public Transform IKTarget { get; }
        public void Subscribe(AnimationType animationType,Action<AnimatorEvent> callback);
        public void Unsubscribe(AnimationType animationType,Action<AnimatorEvent> callback);
        public void SetAimConstraintsActive(bool active,bool isLerp=true);
        public void SetIKTargetPosition(Vector3 position);
        public void SetMovementSpeed(float speed);
        public void SetMovementInput(float input);
        public void SetAttack();
        public void SetHit();
        public void SetDie();
    }
}
