using UnityEngine;
using UnityEngine.Animations;

namespace Gameplay.Player.Interface
{
    public interface IPlayerAnimator : IPlayerComponent
    {
        public Animator Animator { get;}
        public AimConstraint[] AimConstraints { get; }
        public void SetAimConstraintsActive(bool active);
        public void SetMovementSpeed(float speed);
        public void SetMovementInput(float input);
        public void SetAttack();
        public void SetHit();
        public void SetDie();
    }
}
