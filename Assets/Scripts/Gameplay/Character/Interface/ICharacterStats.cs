namespace Gameplay.Character.Interface
{
    public interface ICharacterStats
    {
        public float Health { get; }
        public float MovementSpeed { get; }
        public float RotationSpeed { get; }
        public float AttackSpeed { get; }
    }
}
