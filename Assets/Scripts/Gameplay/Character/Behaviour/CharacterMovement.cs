using Gameplay.Character.Interface;
using UnityEngine;

namespace Gameplay.Character.Behaviour
{
    public class CharacterMovement : ICharacterMovement
    {
        public ICharacter Character { get; }
        private readonly CharacterController _controller;
        public bool HasMoving => _moveInput.magnitude > 0.01f;
        private float _targetSpeed,_currentSpeed,_rotationVelocity,_moveTime;
        private Vector3 _moveInput;
        private Vector3 _velocity;
        private const float Gravity = -9.81f;
        private float _verticalVelocity;

        public CharacterMovement(ICharacter character)
        {
            Character = character;
            _controller = character.CharacterController;
        }
        public void Update()
        {
            if(Character.Status.IsStunned)return;
            Character.Animation.SetMovementInput(_moveInput.magnitude);
            if(!HasMoving) return;
            Character.Animation.SetMovementSpeed(_moveInput.magnitude * Character.Status.Stats.MovementSpeed);
            if (_controller.isGrounded && _velocity.y < 0)
                _velocity.y = -2f;
            _velocity.y += Gravity * Time.deltaTime;
            _controller.transform.rotation = Quaternion.LookRotation(_moveInput.normalized);
            var horizontalMovement = _moveInput * Character.Status.Stats.MovementSpeed;
            var finalMovement = (horizontalMovement + _velocity) * Time.deltaTime;
            _controller.Move(finalMovement);
        }
        public void SetMovementInput(Vector3 input) => _moveInput = input;
       
        public void Reset()
        {
        }
        public void Dispose()
        {
        }
    }
}
