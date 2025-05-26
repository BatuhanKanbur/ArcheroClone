using System;
using Gamecore.AnimatorBehaviour.Behaviour;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.Character.Structure;
using Gameplay.Character.Interface;
using UnityEngine;
using UnityEngine.Animations;
using static Gameplay.Character.Constants.AnimatorConstants;

namespace Gameplay.Character.Behaviour
{
    public class CharacterAnimator : ICharacterAnimator
    {
        public ICharacter Character { get; }
        public Animator Animator { get; }
        public Transform LeftHand { get; }
        public Transform RightHand { get; }
        private readonly AimConstraint[] _aimConstraints;
        private readonly StateMachineBehaviour[] _stateMachineBehaviours;
        public CharacterAnimator(ICharacter character)
        {
            Character = character;
            Animator = Character.Animator;
            LeftHand = Animator.GetBoneTransform(HumanBodyBones.LeftHand);
            RightHand = Animator.GetBoneTransform(HumanBodyBones.RightHand);
            _aimConstraints = Animator.GetComponentsInChildren<AimConstraint>();
            _stateMachineBehaviours = Animator.GetBehaviours<StateMachineBehaviour>();
            SetAimConstraintsActive(false);
        }
        public void Subscribe(AnimationType animationType, Action<AnimatorEvent> callback)
        {
            foreach (var stateMachineBehaviour in _stateMachineBehaviours)
                if (stateMachineBehaviour is AnimatorStateMachine playerAnimationState)
                    playerAnimationState.AddListener(callback);
        }
        public void Unsubscribe(AnimationType animationType, Action<AnimatorEvent> callback)
        {
            foreach (var stateMachineBehaviour in _stateMachineBehaviours)
                if (stateMachineBehaviour is AnimatorStateMachine playerAnimationState)
                    playerAnimationState.RemoveListener(callback);
        }
        public void SetAimConstraintsActive(bool active)
        {
            foreach (var aimConstraint in _aimConstraints)
                aimConstraint.constraintActive = active;
        }
        public void SetMovementInput(float input) => Animator.SetFloat(MovementInputKey, input);
        
        public void SetMovementSpeed(float speed) => Animator.SetFloat(MovementSpeedKey, speed);

        public void SetAttack()
        {
            Animator.SetFloat(AttackSpeedKey, Character.Status.Stats.AttackSpeed);
            Animator.SetTrigger(AttackKey);
        }

        public void SetHit() => Animator.SetTrigger(HitKey);

        public void SetDie() => Animator.SetTrigger(DieKey);

        public void Update()
        {
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
