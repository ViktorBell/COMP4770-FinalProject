
    [System.Serializable]

    public class ProjectWeapon
    {
        public enum WeaponType { Primary, Secondary, Melee }

        public string weaponName;
        public WeaponType type;
        public int magazineSize;
        public int currentAmmo;
        public float fireRate;
        public float range;
        public float damage;
    }