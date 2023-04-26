using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = System.Random;
using System.Linq;
using TroopScripts;
using UnityEngine.EventSystems;


namespace PlayerScripts
{
    public class ShopUpgrade : MonoBehaviour
    {
        [SerializeField] public TMP_Text[] buttonLabels;

        private readonly Dictionary<string, System.Action> _upgradeActions = new Dictionary<string, System.Action>()
        {
            { "Guitar: +1 damage", () => TroopDamage.UpgradeWeaponDamage("Guitar", 1f) },
            { "Guitar: -0.5 cooldown", () => { } },
            { "Notes: +5 amount", () => { } },
            { "Notes: +0.2 damage", () => TroopDamage.UpgradeWeaponDamage("Note", 0.2f) },
            { "Beetle: +1 damage", () => TroopDamage.UpgradeAllyDamage("Beetle", 1) },
            { "Beetle: -0.5 cooldown", () => { } }
        };

        private string[] GetRandomUpgrades(int upgradeCount)
        {
            Random rnd = new Random();
            string[] upgradeList = _upgradeActions.Keys
                                                  .OrderBy(user => rnd.Next())
                                                  .Take(upgradeCount)
                                                  .ToArray();
            return upgradeList;
        }

        private void OnEnable()
        {
            string[] currentUpgrades = GetRandomUpgrades(buttonLabels.Length);
            for (int i = 0; i < buttonLabels.Length; i++)
            {
                buttonLabels[i].text = currentUpgrades[i];
            }
        }

        public void LevelUp()
        {
            GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
            TMP_Text buttonText = selectedButton.GetComponentInChildren<TMP_Text>();
            string buttonLabel = buttonText.text;
            _upgradeActions[buttonLabel]();

            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}