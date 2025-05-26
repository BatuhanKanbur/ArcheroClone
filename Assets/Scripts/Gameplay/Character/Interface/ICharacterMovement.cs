using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ICharacterMovement : ICharacterComponent
    {
        public bool HasMoving { get; }
        public void SetMovementInput(Vector3 input);
    }
}
