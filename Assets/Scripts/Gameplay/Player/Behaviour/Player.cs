using Gameplay.Damageable.Behaviour;
using Gameplay.Damageable.Interface;
using Gameplay.Player.Interface;
using Gameplay.Player.Structure;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Behaviour
{
    using Weapon.Structure;
    public class Player : MonoBehaviour,IPlayer
    {
        public bool IsInitialized { get; private set; }
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private HealthBarManager healthBar;
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        public IPlayerAnimator Animation { get; private set; }
        public IPlayerCombat Combat { get; private set; }
        public IPlayerMovement Movement { get; private set; }
        public IPlayerStatus Status { get; private set; }
        public IPlayerSkillController SkillController { get; private set; }
        
        public void Initialize()
        {
            if(IsInitialized) return;
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            Animation = new PlayerAnimator(this);
            Combat = new PlayerCombat(this, defaultWeapon);
            if (Camera.main) Movement = new PlayerMovement(this, Camera.main.transform);
            Status = new PlayerStatus(this,playerStats,healthBar);
            SkillController = new PlayerSkillController(this);
            IsInitialized = true;
        }
        public void OnEnable()
        {
            Status?.Reset();
            Movement?.Reset();
            Combat?.Reset();
            Animation?.Reset();
            SkillController?.Reset();
        }
        private void Update()
        {
            Movement?.Update();
            Combat?.Update();
            Animation?.Update();
            SkillController?.Update();
        }

        public void OnDestroy() => Dispose();
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
            Status?.Dispose();
            Movement?.Dispose();
            Combat?.Dispose();
            Animation?.Dispose();
            SkillController?.Dispose();
        }
    }
}
