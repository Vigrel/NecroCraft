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

        private UpgradeKey[] _availableUpgrades;


        private UpgradeKey[] GetRandomUpgrades(int upgradeCount)
        {
            Random rnd = new Random();
            UpgradeKey[] upgradeList = UpgradeInfo.GetUpgrades()
                .OrderBy(user => rnd.Next())
                .ToArray();

            return upgradeList.Take(upgradeCount).ToArray();
        }


        private void OnEnable()
        {
            _availableUpgrades = new UpgradeKey[buttonLabels.Length];
            
            UpgradeKey[] currentUpgrades = GetRandomUpgrades(buttonLabels.Length);
            for (int i = 0; i < buttonLabels.Length; i++)
            {
                buttonLabels[i].text = currentUpgrades[i].GetFullDescription();
                _availableUpgrades[i] = currentUpgrades[i];
            }
        }

        public void LevelUp()
        {
            GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
            TMP_Text buttonText = selectedButton.GetComponentInChildren<TMP_Text>();
            string buttonLabel = buttonText.text;
            UpgradeKey selectedUpgradeKey =
                _availableUpgrades.First(upgrade => upgrade.GetFullDescription() == buttonLabel);
            UpgradeInfo.UpgradeWeapon(selectedUpgradeKey);

            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}