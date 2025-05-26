using Gameplay.Character.Interface;
using UnityEngine;

namespace Gameplay.Character.Behaviour
{
    public class CharacterMovement : ICharacterMovement
    {
        public ICharacter Character { get; }
        private readonly Transform _cameraTransform;
        private readonly CharacterController _controller;
        public bool HasMoving => _moveInput.magnitude > 0.01f;
        private float _targetSpeed,_currentSpeed,_rotationVelocity,_moveTime;
        private Vector3 _moveInput;

        public CharacterMovement(ICharacter character, Transform cameraTransform)
        {
            Character = character;
            _controller = character.CharacterController;
            _cameraTransform = cameraTransform;
        }
        public void Update()
        {
            Character.Animation.SetMovementInput(_moveInput.magnitude);
            if(!HasMoving) return;
            Character.Animation.SetMovementSpeed(_moveInput.magnitude * Character.Status.Stats.MovementSpeed);
            _controller.transform.rotation = Quaternion.LookRotation(_moveInput.normalized);
            _controller.Move(_moveInput * (Character.Status.Stats.MovementSpeed * Time.deltaTime));
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
