using System;
using Gameplay.Player.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Behaviour
{
    public class Player : MonoBehaviour,IPlayer
    {
        public bool IsInitialized { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        public IPlayerAnimator Animation { get; private set; }
        public IPlayerCombat Combat { get; private set; }
        public IPlayerMovement Movement { get; private set; }
        public IPlayerStatus Status { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if(IsInitialized) return;
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            Animation = new PlayerAnimator(this);
            Combat = new PlayerCombat(this);
            if (Camera.main) Movement = new PlayerMovement(this, Camera.main.transform);
            Status = new PlayerStatus(this);
            IsInitialized = true;
        }
        public void OnEnable()
        {
            Status?.Reset();
            Movement?.Reset();
            Combat?.Reset();
            Animation?.Reset();
        }
        private void Update()
        {
            Movement?.Update();
            Combat?.Update();
            Animation?.Update();
        }
        public void OnDestroy()
        {
            Status?.Dispose();
            Movement?.Dispose();
            Combat?.Dispose();
            Animation?.Dispose();
        }
        public void OnMove(InputValue input)
        {
            Movement.SetMovementInput(input.Get<Vector2>());
        }
        public void OnAttack()
        {
            Animation.SetAttack();
        }
        public void Dispose()
        {
        }
    }
}
