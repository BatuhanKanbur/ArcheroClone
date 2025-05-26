using Gameplay.Character.Interface;
using Gameplay.Character.Structure;
using Gameplay.Damageable.Behaviour;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Character.Behaviour
{
    using Weapon.Structure;
    public abstract class Character : MonoBehaviour,ICharacter
    {
        public bool IsInitialized { get; private set; }
        [SerializeField] private CharacterStats characterStats;
        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private HealthBarManager healthBar;
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        public ICharacterAnimator Animation { get; private set; }
        public ICharacterCombat Combat { get; private set; }
        public ICharacterMovement Movement { get; private set; }
        public ICharacterStatus Status { get; private set; }
        public ICharacterSkillController SkillController { get; private set; }
        
        public void Initialize()
        {
            if(IsInitialized) return;
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            Animation = new CharacterAnimator(this);
            Combat = new CharacterCombat(this, defaultWeapon);
            if (Camera.main) Movement = new CharacterMovement(this, Camera.main.transform);
            Status = new CharacterStatus(this,characterStats,healthBar);
            SkillController = new CharacterSkillController(this);
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
        public void Move(InputValue input)
        {
            Movement.SetMovementInput(input.Get<Vector2>());
        }
        public void Attack()
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
