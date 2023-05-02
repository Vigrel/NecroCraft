using System.Collections.Generic;
using UnityEngine;

namespace TroopScripts
{
    public class TroopDamage : MonoBehaviour
    {
        public static int MaxEnemyCount { get; } = 50;
        public static int EnemyCount { get; private set; } = 0;
        public static void IncrementEnemyCount() => EnemyCount++;
        public static void DecrementEnemyCount() => EnemyCount--;
        
        public static int MaxAllyCount { get; } = 10;
        public static int AllyCount { get; private set; } = 0;
        public static void IncrementAllyCount() => AllyCount++;
        public static void DecrementAllyCount() => AllyCount--;

        private static readonly Dictionary<string, float> EnemyDamage = new Dictionary<string, float>() {
            {"Villager", 1f},
            {"Peasant", 2f},
            {"Soldier", 0.2f},
            {"Knight", 5f}
        };
    
        private static readonly Dictionary<string, float> AllyDamage = new Dictionary<string, float>() {
            {"Hand", 0.5f},
            {"Mummy", 5f},
            {"Zombie", 2f}
        };
        private static Dictionary<string, float> _weaponDamage = new Dictionary<string, float>() {
            {"Note", 0.5f},
            {"Guitar", 3f}
        };
        
        public static float GetDamageForEnemy(string enemyTag)
        {
            return EnemyDamage.ContainsKey(enemyTag) ? EnemyDamage[enemyTag] : 0f;
        }
        public static float GetDamageForAlly(string allyTag)
        {
            return AllyDamage.ContainsKey(allyTag) ? AllyDamage[allyTag] : 0f;
        }
        public static float GetDamageForWeapon(string wpnTag)
        {
            return _weaponDamage.ContainsKey(wpnTag) ? _weaponDamage[wpnTag] : 0f;
        }
        
        public static void UpgradeWeaponDamage(string wpnTag, float damageIncrease)
        {
            _weaponDamage[wpnTag] += damageIncrease;
        }
        
        public static void UpgradeAllyDamage(string allyTag, float damageIncrease)
        {
            AllyDamage[allyTag] += damageIncrease;
        }
        
    }
}
