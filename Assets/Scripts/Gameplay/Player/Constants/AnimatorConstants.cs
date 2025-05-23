using UnityEngine;

namespace Gameplay.Player.Constants
{
    public static class AnimatorConstants
    {
        public static readonly int MovementInputKey = Animator.StringToHash("Movement_Input");
        public static readonly int MovementSpeedKey = Animator.StringToHash("Movement_Speed");
        public static readonly int AttackKey = Animator.StringToHash("Attack");
        public static readonly int HitKey = Animator.StringToHash("Hit");
        public static readonly int DieKey = Animator.StringToHash("Die");
    }
}
