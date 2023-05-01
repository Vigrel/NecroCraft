using Unity.VisualScripting;

namespace PlayerScripts
{
    public class UpgradeKey
    {
        public string WeaponName { get; }
        public string UpgradeDescription { get; }
        
        public UpgradeKey(string weaponName, string upgradeDescription)
        {
            WeaponName = weaponName;
            UpgradeDescription = upgradeDescription;
        }
        
        public string GetFullDescription()
        {
            return WeaponName + ": " + UpgradeDescription;
        }

        public override int GetHashCode()
        {
            return (WeaponName + UpgradeDescription).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is UpgradeKey other)
            {
                return WeaponName == other.WeaponName && UpgradeDescription == other.UpgradeDescription;
            }

            return false;
        }
    }
}