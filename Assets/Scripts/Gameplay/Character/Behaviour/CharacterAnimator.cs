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
        public Transform IKTarget { get; private set; }
        private readonly CharacterAimController[] _aimConstraints;
        private readonly StateMachineBehaviour[] _stateMachineBehaviours;
        public CharacterAnimator(ICharacter character,Transform ikTarget)
        {
            Character = character;
            Animator = Character.Animator;
            LeftHand = Animator.GetBoneTransform(HumanBodyBones.LeftHand);
            RightHand = Animator.GetBoneTransform(HumanBodyBones.RightHand);
            IKTarget = ikTarget;
            _aimConstraints = Animator.GetComponentsInChildren<CharacterAimController>();
            _stateMachineBehaviours = Animator.GetBehaviours<StateMachineBehaviour>();
        }
        public void Subscribe(AnimationType animationType, Action<AnimatorEvent> callback)
        {
            foreach (var stateMachineBehaviour in _stateMachineBehaviours)
                if (stateMachineBehaviour is AnimatorStateMachine playerAnimationState)
                    if( playerAnimationState.animationType == animationType)
                        playerAnimationState.AddListener(callback);
        }
        public void Unsubscribe(AnimationType animationType, Action<AnimatorEvent> callback)
        {
            foreach (var stateMachineBehaviour in _stateMachineBehaviours)
                if (stateMachineBehaviour is AnimatorStateMachine playerAnimationState)
                    if( playerAnimationState.animationType == animationType)
                        playerAnimationState.RemoveListener(callback);
        }
        public void SetAimConstraintsActive(bool active)
        {
            foreach (var aimConstraint in _aimConstraints)
                aimConstraint.SetActive(active);
        }
        public void SetIKTargetPosition(Vector3 position)
        {
            if (!IKTarget) return;
            IKTarget.position = position;
        }
        public void SetMovementInput(float input) => Animator.SetFloat(MovementInputKey, input);
        
        public void SetMovementSpeed(float speed) => Animator.SetFloat(MovementSpeedKey, speed);

        public void SetAttack()
        {
            Animator.SetFloat(AttackSpeedKey, Character.Status.Stats.AttackSpeed);
            Animator.SetTrigger(AttackKey);
        }

        public void SetHit() => Animator.SetTrigger(HitKey);

        public void SetDie() =>  Animator.SetTrigger(DieKey);

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
