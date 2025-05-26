using System;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.Character.Structure;
using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ICharacterAnimator : ICharacterComponent
    {
        public Animator Animator { get;}
        public Transform LeftHand { get; }
        public Transform RightHand { get; }
        public void Subscribe(AnimationType animationType,Action<AnimatorEvent> callback);
        public void Unsubscribe(AnimationType animationType,Action<AnimatorEvent> callback);
        public void SetAimConstraintsActive(bool active);
        public void SetMovementSpeed(float speed);
        public void SetMovementInput(float input);
        public void SetAttack();
        public void SetHit();
        public void SetDie();
    }
}
