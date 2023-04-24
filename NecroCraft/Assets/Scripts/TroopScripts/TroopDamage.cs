using System.Collections.Generic;
using UnityEngine;

namespace TroopScripts
{
    public class TroopDamage : MonoBehaviour
    {
        private static readonly Dictionary<string, float> EnemyDamage = new Dictionary<string, float>() {
            {"Villager", 1f},
            {"Knight", 2f}
        };
    
        private static readonly Dictionary<string, float> AllyDamage = new Dictionary<string, float>() {
            {"Beetle", 1f}
        };
        private static readonly Dictionary<string, float> WeaponDamage = new Dictionary<string, float>() {
            {"Note", 0.5f},
            {"Guitar", 1f}
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
            return WeaponDamage.ContainsKey(wpnTag) ? WeaponDamage[wpnTag] : 0f;
        }

    }
}
