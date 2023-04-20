using UnityEngine;

namespace MapScripts
{
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
            _playerTileGridPosition = CalculatePositionOnAxis(playerTransform.position);
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
                    tile.transform.position = CalculateTilePosition(
                        (int)(_playerTilePosition.x + povx),
                        (int)(_playerTilePosition.y + povy)
                    );
                }
            }
        }

        private Vector3 CalculateTilePosition(int v1, int v2)
        {
            return new Vector3(v1 * tileSize, v2 * tileSize, 1f);
        }

        private Vector2 CalculatePositionOnAxis(Vector2 currentValue)
        {
            currentValue = new Vector2(Mathf.FloorToInt(currentValue.x / tileSize), Mathf.FloorToInt(currentValue.y / tileSize));
            currentValue.x = (currentValue.x + terrainTileHorizontalCount) % terrainTileHorizontalCount;
            currentValue.y = (currentValue.y + terrainTileVerticalCount) % terrainTileVerticalCount;
            return currentValue;
        }

        public void Add(GameObject tileGameObject, Vector2 tilePosition)
        {
            _terrainTiles[(int)tilePosition.x, (int)tilePosition.y] = tileGameObject;
        }
    }
}
