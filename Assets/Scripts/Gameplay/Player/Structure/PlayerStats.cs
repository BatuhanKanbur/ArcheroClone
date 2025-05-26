using Gameplay.Player.Interface;
using Gameplay.Skill.Structure;
using UnityEngine;

namespace Gameplay.Player.Structure
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
    public class PlayerStats : StatsData ,IPlayerStats
    {
        public float Health => maxHealth;
        public float MovementSpeed => movementSpeed * multiplier;
        public float RotationSpeed => rotationSpeed * multiplier;
        public float AttackSpeed => attackSpeed * multiplier;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float attackSpeed;

        public PlayerStats(float movementSpeed, float rotationSpeed, float attackSpeed) : base(1)
        {
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
            this.attackSpeed = attackSpeed;
        }

        public static PlayerStats operator +(PlayerStats a, PlayerStats b)
        {
            return new PlayerStats(
                a.movementSpeed + b.movementSpeed,
                a.rotationSpeed + b.rotationSpeed,
                a.attackSpeed + b.attackSpeed
            );
        }
        public static PlayerStats operator -(PlayerStats a, PlayerStats b)
        {
            return new PlayerStats(
                a.movementSpeed - b.movementSpeed,
                a.rotationSpeed - b.rotationSpeed,
                a.attackSpeed - b.attackSpeed
            );
        }
    }
}
