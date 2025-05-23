using System.Collections;
using System.Collections.Generic;
using Gameplay.Player.Interface;
using UnityEngine;

public class PlayerMovement : IPlayerMovement
{
    public IPlayer Player { get; }
    private readonly Transform _cameraTransform;
    private readonly CharacterController _controller;
    public bool HasMoving => _moveInput.magnitude > 0.01f;
    private float _targetSpeed,_currentSpeed,_rotationVelocity,_moveTime;
    private Vector2 _moveInput;

    public PlayerMovement(IPlayer player, Transform cameraTransform)
    {
        Player = player;
        _controller = player.CharacterController;
        _cameraTransform = cameraTransform;
    }
    public void Update()
    {
        var moveDir = GetMovementDirection();
        Player.Animation.SetMovementInput(moveDir.magnitude);
        if(!HasMoving) return;
        Player.Animation.SetMovementSpeed(moveDir.magnitude * Player.Status.Stats.MovementSpeed);
        _controller.transform.rotation = Quaternion.LookRotation(moveDir);
        _controller.Move(moveDir * (Player.Status.Stats.MovementSpeed * Time.deltaTime));
    }
    public void SetMovementInput(Vector2 input) => _moveInput = input;
    private Vector3 GetMovementDirection()
    {
        var camForward = _cameraTransform.transform.forward;
        var camRight = _cameraTransform.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        var desiredMove = camForward * _moveInput.y + camRight * _moveInput.x;
        return desiredMove;
    }
    public void Reset()
    {
    }
    public void Dispose()
    {
    }
}
