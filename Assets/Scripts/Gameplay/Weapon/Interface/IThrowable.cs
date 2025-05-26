namespace Gameplay.Weapon.Interface
{
    public interface IThrowable
    {
        public IWeaponStats WeaponStats { get; set; }
        public void Init(IWeaponStats weapon);
    }
}
