using UnityEngine;

namespace TroopScripts
{
    public class EnemyHealth: MonoBehaviour
    {
        public GameObject villagerPrefab;
        public GameObject peasantPrefab;
        public GameObject soldierPrefab;
        public GameObject knightPrefab;
        
        private Enemy _villagerScript;
        private Enemy _peasantScript;
        private Enemy _soldierScript;
        private Enemy _knightScript;
        
        
        [SerializeField] float healthIncreaseInterval = 45f;
        [SerializeField] public float healthIncreaseAmount = 1.05f;

        private void Start()
        {
            InvokeRepeating(
                "IncreaseEnemyHealth", healthIncreaseInterval, healthIncreaseInterval);
            _villagerScript = villagerPrefab.GetComponent<Enemy>();
            _peasantScript = peasantPrefab.GetComponent<Enemy>();
            _soldierScript = soldierPrefab.GetComponent<Enemy>();
            _knightScript = knightPrefab.GetComponent<Enemy>();

            _villagerScript.maxHp = 5;
            _peasantScript.maxHp = 10;
            _soldierScript.maxHp = 3;
            _knightScript.maxHp = 30;
        }

        private void IncreaseEnemyHealth()
        {
            _villagerScript.maxHp *= healthIncreaseAmount;
            _peasantScript.maxHp *= healthIncreaseAmount;
            _soldierScript.maxHp *= healthIncreaseAmount;
            _knightScript.maxHp *= healthIncreaseAmount;
        }
    }
}