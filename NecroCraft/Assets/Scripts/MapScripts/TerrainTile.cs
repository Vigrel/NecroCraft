using UnityEngine;

namespace MapScripts
{
    public class TerrainTile : MonoBehaviour
    {
        [SerializeField] Vector2 tilePosition;

        private void Start(){
            GetComponentInParent<WorldScroll>().Add(gameObject, tilePosition);
        }
    }
}