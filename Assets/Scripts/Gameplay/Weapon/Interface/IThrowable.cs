namespace Gameplay.Weapon.Interface
{
    public interface IThrowable
    {
        public IWeaponStats Weapon { get; set; }
        public void Init(IWeaponStats weapon);
    }
}
