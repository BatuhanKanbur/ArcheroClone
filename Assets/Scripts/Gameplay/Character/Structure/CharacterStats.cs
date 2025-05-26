using Gameplay.Character.Interface;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gameplay.Character.Structure
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
    public class CharacterStats : StatsData ,ICharacterStats
    {
        public float Health => maxHealth;
        public float MovementSpeed => movementSpeed * multiplier;
        public float RotationSpeed => rotationSpeed * multiplier;
        public float AttackSpeed => attackSpeed * multiplier;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float attackSpeed;

        public CharacterStats(float movementSpeed, float rotationSpeed, float attackSpeed) : base(1)
        {
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
            this.attackSpeed = attackSpeed;
        }

        public static CharacterStats operator +(CharacterStats a, CharacterStats b)
        {
            return new CharacterStats(
                a.movementSpeed + b.movementSpeed,
                a.rotationSpeed + b.rotationSpeed,
                a.attackSpeed + b.attackSpeed
            );
        }
        public static CharacterStats operator -(CharacterStats a, CharacterStats b)
        {
            return new CharacterStats(
                a.movementSpeed - b.movementSpeed,
                a.rotationSpeed - b.rotationSpeed,
                a.attackSpeed - b.attackSpeed
            );
        }
    }
}
