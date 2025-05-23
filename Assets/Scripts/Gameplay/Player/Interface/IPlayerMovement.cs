using UnityEngine;

namespace Gameplay.Player.Interface
{
    public interface IPlayerMovement : IPlayerComponent
    {
        public bool HasMoving { get; }
        public void SetMovementInput(Vector2 input);
    }
}
