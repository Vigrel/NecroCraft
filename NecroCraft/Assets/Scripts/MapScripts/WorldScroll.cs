using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroll : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;
    [SerializeField] float tileSize;


    private Vector2 _playerTilePosition;
    private Vector2 _playerTileGridPosition;
    private GameObject[,] _terrainTiles;

    private void Awake()
    {
        _terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        _playerTilePosition = playerTransform.position / tileSize;
        _playerTileGridPosition = CalculatePositionOnAxis(_playerTilePosition);
        // Debug.Log(_playerTileGridPosition);
        // Debug.Log(_playerTilePosition);
        UpdateTilesOnScreen();
    }

    private void UpdateTilesOnScreen()
    {
        Vector2 pos = _playerTileGridPosition;

        for (int povx = -(terrainTileHorizontalCount / 2); povx <= (terrainTileHorizontalCount / 2); povx++)
        {
            for (int povy = -(terrainTileVerticalCount / 2); povy <= (terrainTileVerticalCount / 2); povy++)
            {   

                pos.x = (_playerTileGridPosition.x + povx + terrainTileHorizontalCount) % terrainTileHorizontalCount;
                pos.y = (_playerTileGridPosition.y + povy + terrainTileVerticalCount) % terrainTileVerticalCount;
                GameObject tile = _terrainTiles[(int)pos.x, (int)pos.y];
                tile.transform.position = new Vector3(
                    (int)((int)_playerTilePosition.x + povx) * tileSize,
                    (int)((int)_playerTilePosition.y + povy) * tileSize,
                    1f
                );
            }
        }
    }

    private Vector2 CalculatePositionOnAxis(Vector2 currentValue)
    {  
        currentValue.x = 1+((int)currentValue.x + terrainTileHorizontalCount) % terrainTileHorizontalCount;
        currentValue.y = 1+((int)currentValue.y + terrainTileVerticalCount) % terrainTileVerticalCount;

        if (currentValue.x == 3) currentValue.x = 0;
        if (currentValue.y == 3) currentValue.y = 0;
        return currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2 tilePosition)
    {
        _terrainTiles[(int)tilePosition.x, (int)tilePosition.y] = tileGameObject;
    }
    
}
