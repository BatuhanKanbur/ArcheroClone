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
        public float AttackRange => attackRange * multiplier;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange = 500f;

        public CharacterStats(float movementSpeed, float rotationSpeed, float attackSpeed) : base(1)
        {
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
            this.attackSpeed = attackSpeed;
        }

        public static CharacterStats operator +(CharacterStats a, CharacterStats b)
        {
            var newStats = CreateInstance<CharacterStats>();
            newStats.multiplier = a.multiplier + b.multiplier;
            newStats.movementSpeed = a.movementSpeed + b.movementSpeed;
            newStats.rotationSpeed = a.rotationSpeed + b.rotationSpeed;
            newStats.attackSpeed = a.attackSpeed + b.attackSpeed;
            return newStats;
        }
        public static CharacterStats operator -(CharacterStats a, CharacterStats b)
        {
            var newStats = CreateInstance<CharacterStats>();
            newStats.multiplier = a.multiplier - b.multiplier;
            newStats.movementSpeed = a.movementSpeed - b.movementSpeed;
            newStats.rotationSpeed = a.rotationSpeed - b.rotationSpeed;
            newStats.attackSpeed = a.attackSpeed - b.attackSpeed;
            return newStats;
        }
    }
}
