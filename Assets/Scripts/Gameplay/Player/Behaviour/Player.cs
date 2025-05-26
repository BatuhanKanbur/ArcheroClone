using System;
using Gameplay.Player.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Behaviour
{
    using Gameplay.Character.Behaviour;
    public class Player : Character, IPlayer
    {
        private Camera _mainCamera;
        public new void Initialize()
        {
            _mainCamera = Camera.main;
            base.Initialize();
        }
        public void OnMove(InputValue input)
        {
            Movement?.SetMovementInput(GetMovementDirection(input.Get<Vector2>()));
        }
        private Vector3 GetMovementDirection(Vector2 moveInput)
        {
            var camForward = _mainCamera.transform.forward;
            var camRight = _mainCamera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();
            var desiredMove = camForward * moveInput.y + camRight * moveInput.x;
            return desiredMove;
        }
    }
}
