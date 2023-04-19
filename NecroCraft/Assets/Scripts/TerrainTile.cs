using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2 tilePosition;

    private void Start(){
        GetComponentInParent<WorldScroll>().Add(gameObject, tilePosition);
    }
}