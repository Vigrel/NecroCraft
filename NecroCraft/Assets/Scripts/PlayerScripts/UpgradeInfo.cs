﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TroopScripts;
using Projectile;

namespace PlayerScripts
{
    public class UpgradeInfo : MonoBehaviour
    {
        [SerializeField] private GameObject guitarController;
        [SerializeField] private GameObject noteController;
        [SerializeField] private GameObject beetleController;

        private static GameObject _guitarController;
        private static GameObject _noteController;
        private static GameObject _beetleController;

        private static Note _noteScript;
        private static ProjectileSpawner _guitarScript;
        private static AllySpawner _beetleScript;

        private const int MaxWeapons = 4;
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
            _beetleController = beetleController;

            // Initialize WeaponControllers dictionary
            _weaponControllers = new Dictionary<string, GameObject>()
            {
                { "Guitar", _guitarController },
                { "Note", _noteController },
                { "Beetle", _beetleController}
            };

            // Initialize WeaponUpgrades dictionary
            _weaponUpgrades = new Dictionary<string, int>()
            {
                { "Guitar", 1},
                { "Note", 0 },
                { "Beetle", 0},
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
                    new UpgradeKey("Beetle", "Get new weapon"),
                    () => GetNewWeapon("Beetle")
                },
                {
                    new UpgradeKey("Beetle", "+1 damage"),
                    () => TroopDamage.UpgradeAllyDamage("Beetle", 1)
                },
                {
                    new UpgradeKey("Beetle", "10% cooldown reduction"),
                    () => _beetleScript.UpdateSpawnDelay(0.1f)
                },
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
            _beetleScript = _beetleController.GetComponent<AllySpawner>();
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

        public static UpgradeKey[] GetUpgrades()
        {
            List<UpgradeKey> possibleUpgrades = new List<UpgradeKey>();
            if (_weaponCount < MaxWeapons)
            {
                foreach (UpgradeKey upgrade in _upgradeActions.Keys)
                {
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
            if (_weaponUpgrades[weapon.WeaponName] >= MaxUpgrades)
            {
                throw new Exception("Error: Weapon already upgraded to max!");
            }

            if (_weaponUpgrades[weapon.WeaponName] == 0 && _weaponCount == MaxWeapons)
            {
                throw new Exception("Error: Max weapons reached!");
            }

            if (_weaponUpgrades[weapon.WeaponName] == 0 && weapon.UpgradeDescription != "Get new weapon")
            {
                throw new Exception("Error: Weapon not upgraded to level 1!");
            }

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