using Gamecore.AnimatorBehaviour.Behaviour;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.Character.Structure;
using Gameplay.Player.Interface;
using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;
using static Gameplay.Player.Constants.AnimatorConstants;

namespace Gameplay.Player.Behaviour
{
    public class PlayerAnimator : IPlayerAnimator
    {
        public IPlayer Player { get; }
        public Animator Animator { get; }
        private readonly AimConstraint[] _aimConstraints;
        private readonly StateMachineBehaviour[] _stateMachineBehaviours;
        public PlayerAnimator(IPlayer player)
        {
            Player = player;
            Animator = Player.Animator;
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
            Animator.SetFloat(AttackSpeedKey, Player.Status.Stats.AttackSpeed);
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
