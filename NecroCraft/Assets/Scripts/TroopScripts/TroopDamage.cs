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
            {"Note", 1f}
        };
    
    
        public static float GetDamageForEnemy(string enemyTag)
        {
            if (EnemyDamage.ContainsKey(enemyTag))
                return EnemyDamage[enemyTag];
        
            return 0f;
        }
        public static float GetDamageForAlly(string allyTag)
        {
            if (AllyDamage.ContainsKey(allyTag))
                return AllyDamage[allyTag];
        
            return 0f;
        }
        public static float GetDamageForWeapon(string wpnTag)
        {
            if (WeaponDamage.ContainsKey(wpnTag))
                return WeaponDamage[wpnTag];
        
            return 0f;
        }

    }
}
