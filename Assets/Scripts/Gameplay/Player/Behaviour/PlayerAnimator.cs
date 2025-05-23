using Gameplay.Player.Interface;
using UnityEngine;
using UnityEngine.Animations;
using static Gameplay.Player.Constants.AnimatorConstants;

namespace Gameplay.Player.Behaviour
{
    public class PlayerAnimator : IPlayerAnimator
    {
        public IPlayer Player { get; }
        public Animator Animator { get; }
        public AimConstraint[] AimConstraints { get; private set; }
        public PlayerAnimator(IPlayer player)
        {
            Player = player;
            Animator = Player.Animator;
            AimConstraints = Animator.GetComponentsInChildren<AimConstraint>();
            SetAimConstraintsActive(false);
        }
        public void SetAimConstraintsActive(bool active)
        {
            foreach (var aimConstraint in AimConstraints)
                aimConstraint.constraintActive = active;
        }
        public void SetMovementInput(float input) => Animator.SetFloat(MovementInputKey, input);
        
        public void SetMovementSpeed(float speed) => Animator.SetFloat(MovementSpeedKey, speed);

        public void SetAttack() => Animator.SetTrigger(AttackKey);

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
