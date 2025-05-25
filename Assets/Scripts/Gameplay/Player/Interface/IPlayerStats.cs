namespace Gameplay.Player.Interface
{
    public interface IPlayerStats
    {
        public float MovementSpeed { get; }
        public float RotationSpeed { get; }
        public float AttackSpeed { get; }
    }
}
