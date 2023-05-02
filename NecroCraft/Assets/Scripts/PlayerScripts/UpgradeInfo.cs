using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TroopScripts;
using Projectile;
using PlayerScripts;

namespace PlayerScripts
{
    public class UpgradeInfo : MonoBehaviour
    {
        [SerializeField] private GameObject guitarController;
        [SerializeField] private GameObject noteController;
        [SerializeField] private GameObject handController;
        [SerializeField] private GameObject zombieController;
        [SerializeField] private GameObject mummyController;

        private static GameObject _guitarController;
        private static GameObject _noteController;
        private static GameObject _handController;
        private static GameObject _zombieController;
        private static GameObject _mummyController;

        private static Note _noteScript;
        private static ProjectileSpawner _guitarScript;
        private static AllySpawner _handScript;
        private static AllySpawner _zombieScript;
        private static AllySpawner _mummyScript;

        private const int MaxWeapons = 3;
        private const int MaxUpgrades = 6;

        private static int _weaponCount = 0;

        // dictionary of weapons and their controllers
        private static Dictionary<string, GameObject> _weaponControllers;

        // dictionary of weapons upgrade level
        private static Dictionary<string, int> _weaponUpgrades;

        private static Dictionary<UpgradeKey, System.Action> _upgradeActions;


        private void InitializeAttributes()
        {
            // Initialize Controller GameObjects
            _guitarController = guitarController;
            _noteController = noteController;
            _handController = handController;
            _zombieController = zombieController;
            _mummyController = mummyController;

            // Initialize WeaponControllers dictionary
            _weaponControllers = new Dictionary<string, GameObject>()
            {
                { "Guitar", _guitarController },
                { "Note", _noteController },
                { "Hand", _handController },
                { "Zombie", _zombieController },
                { "Mummy", _mummyController },
            };

            // Initialize WeaponUpgrades dictionary
            _weaponUpgrades = new Dictionary<string, int>()
            {
                { "Guitar", 1 },
                { "Note", 0 },
                { "Hand", 0 },
                { "Zombie", 0 },
                { "Mummy", 0 },
            };

            // Initialize UpgradeActions dictionary
            _upgradeActions = new Dictionary<UpgradeKey, System.Action>()
            {
                {
                    new UpgradeKey("Guitar", "+1 damage"),
                    () => TroopDamage.UpgradeWeaponDamage("Guitar", 1f)
                },
                {
                    new UpgradeKey("Guitar", "10% cooldown reduction"),
                    () => _guitarScript.UpdateSpawnDelay(0.1f)
                },
                {
                    new UpgradeKey("Note", "Get new weapon"),
                    () => GetNewWeapon("Note")
                },
                {
                    new UpgradeKey("Note", "+2 amount"),
                    () => _noteScript.UpdateAmount(2)
                },
                {
                    new UpgradeKey("Note", "+0.2 damage"),
                    () => TroopDamage.UpgradeWeaponDamage("Note", 0.2f)
                },
                {
                    new UpgradeKey("Note", "5% cooldown reduction"),
                    () => _noteScript.UpdateCooldown(0.05f)
                },
                {
                    new UpgradeKey("Note", "+2 speed"),
                    () => _noteScript.UpdateSpeed(2f)
                },
                {
                    new UpgradeKey("Note", "+0.5s duration"),
                    () => _noteScript.UpdateLifetime(0.5f)
                },
                {
                    new UpgradeKey("Hand", "Get new weapon"),
                    () => GetNewWeapon("Hand")
                },
                {
                    new UpgradeKey("Hand", "+1 damage"),
                    () => TroopDamage.UpgradeAllyDamage("Hand", 1)
                },
                {
                    new UpgradeKey("Hand", "10% cooldown reduction"),
                    () => _handScript.UpdateSpawnDelay(0.1f)
                },
                {
                    new UpgradeKey("Zombie", "Get new weapon"),
                    () => GetNewWeapon("Zombie")
                },
                {
                    new UpgradeKey("Zombie", "+1 damage"),
                    () => TroopDamage.UpgradeAllyDamage("Zombie", 1)
                },
                {
                    new UpgradeKey("Zombie", "10% cooldown reduction"),
                    () => _zombieScript.UpdateSpawnDelay(0.1f)
                },
                {
                    new UpgradeKey("Mummy", "Get new weapon"),
                    () => GetNewWeapon("Mummy")
                },
                {
                    new UpgradeKey("Mummy", "+1 damage"),
                    () => TroopDamage.UpgradeAllyDamage("Mummy", 1)
                },
                {
                    new UpgradeKey("Mummy", "10% cooldown reduction"),
                    () => _mummyScript.UpdateSpawnDelay(0.1f)
                },
                {
                    new UpgradeKey("Health", "Recover 10% lost health"),
                    () => PlayerController.Instance.RecoverHealth(0.1f)
                },
                {
                    new UpgradeKey("Health", "Increase max health by 5%"),
                    () => PlayerController.Instance.UpgradeMaxHealth()
                },
                {
                    new UpgradeKey("Speed", "Increase speed by 5%"),
                    () => PlayerController.Instance.UpgradeSpeed()
                }
            };
        }

        private void Start()
        {
            InitializeAttributes();

            foreach (KeyValuePair<string, int> pair in _weaponUpgrades)
            {
                if (pair.Value > 0)
                {
                    _weaponCount++;
                    _weaponControllers[pair.Key].SetActive(true);
                    continue;
                }

                _weaponControllers[pair.Key].SetActive(false);
            }

            _guitarScript = _guitarController.GetComponent<ProjectileSpawner>();
            _noteScript = _noteController.GetComponent<Note>();
            _handScript = _handController.GetComponent<AllySpawner>();
            _zombieScript = _zombieController.GetComponent<AllySpawner>();
            _mummyScript = _mummyController.GetComponent<AllySpawner>();
        }

        private static void GetNewWeapon(string weaponName)
        {
            if (_weaponUpgrades[weaponName] > 0)
            {
                throw new Exception("Error: Weapon already upgraded!");
            }

            if (_weaponCount > MaxWeapons)
            {
                throw new Exception("Error: Max weapons reached!");
            }

            _weaponControllers[weaponName].SetActive(true);
            _weaponUpgrades[weaponName]++;
            _weaponCount++;
        }

        private static List<UpgradeKey> AddFixedUpgrades()
        {
            List<UpgradeKey> fixedUpgrades = new List<UpgradeKey>
            {
                new UpgradeKey("Health", "Recover 10% lost health"),
                new UpgradeKey("Health", "Increase max health by 5%"),
                new UpgradeKey("Speed", "Increase speed by 5%")
            };
            return fixedUpgrades;
        }

        public static UpgradeKey[] GetUpgrades()
        {
            List<UpgradeKey> possibleUpgrades = AddFixedUpgrades();
            if (_weaponCount < MaxWeapons)
            {
                foreach (UpgradeKey upgrade in _upgradeActions.Keys)
                {
                    if (upgrade.WeaponName is "Health" or "Speed") continue;
                    switch (_weaponUpgrades[upgrade.WeaponName])
                    {
                        case 0 when upgrade.UpgradeDescription == "Get new weapon":
                        case > 0 when upgrade.UpgradeDescription != "Get new weapon"
                                      && _weaponUpgrades[upgrade.WeaponName] < MaxUpgrades:
                            possibleUpgrades.Add(upgrade);
                            break;
                    }
                }

                return possibleUpgrades.ToArray();
            }

            foreach (UpgradeKey upgrade in _upgradeActions.Keys.Where(upgrade =>
                         upgrade.UpgradeDescription != "Get new weapon"))
            {
                if (upgrade.WeaponName is "Health" or "Speed") continue;
                switch (_weaponUpgrades[upgrade.WeaponName])
                {
                    case MaxUpgrades:
                    case 0:
                        continue;
                    default:
                        possibleUpgrades.Add(upgrade);
                        break;
                }
            }

            return possibleUpgrades.ToArray();
        }

        public static void UpgradeWeapon(UpgradeKey weapon)
        {
            if (weapon.WeaponName is "Health" or "Speed")
            {
                _upgradeActions[weapon]();
                return;
            }

            if (_weaponUpgrades[weapon.WeaponName] >= MaxUpgrades)
                throw new Exception("Error: Weapon already upgraded to max!");

            if (_weaponUpgrades[weapon.WeaponName] == 0 && _weaponCount == MaxWeapons)
                throw new Exception("Error: Max weapons reached!");

            if (_weaponUpgrades[weapon.WeaponName] == 0 && weapon.UpgradeDescription != "Get new weapon")
                throw new Exception("Error: Weapon not upgraded to level 1!");

            if (weapon.UpgradeDescription == "Get new weapon")
            {
                GetNewWeapon(weapon.WeaponName);
                return;
            }

            _upgradeActions[weapon]();
            _weaponUpgrades[weapon.WeaponName]++;
        }
    }
}