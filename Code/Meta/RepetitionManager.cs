using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetitionManager : MonoBehaviour {
    const int MAX_TILES = 30;

    public GameObject TilePrefab;
    public Camera Main2DCamera;
    public float TileSpawnY = 0;
    public float TileWidth = 0;

    private readonly GameObject[] TilePool = new GameObject[MAX_TILES];
    private float CurrentMaxVisibleX = -10;

    private void Start() {
        // Populate pool
        for (int i=0; i < MAX_TILES; i++) {
            TilePool[i] = Instantiate(TilePrefab);
            TilePool[i].SetActive(false);
            TilePool[i].transform.position = new Vector3(-1000, 0, 0);
        }
    }

    private void Update() {
        UncoverPosition(Main2DCamera.transform.position.x + Main2DCamera.orthographicSize / 2);
    }

    private GameObject GetTile() {
        // Returns the leftmost tile in the pool
        // Introduce randomness here
        int leftmostTile = 0;
        float leftmostPos = float.MaxValue;
        for (int i=0; i < MAX_TILES; i++) {
            if (TilePool[i].transform.position.x < leftmostPos) {
                leftmostPos = TilePool[i].transform.position.x;
                leftmostTile = i;
            }
        }
        return TilePool[leftmostTile];
    }

    public void UncoverPosition(float maxVisibleX) {
        // Can be called by the camera
        while (maxVisibleX + TileWidth + 5 > CurrentMaxVisibleX) {
            GameObject tile = GetTile();
            tile.SetActive(true);
            tile.transform.position = new Vector3(CurrentMaxVisibleX, TileSpawnY, 0);  // Assumes that tile origin is at the left
            CurrentMaxVisibleX += TileWidth;
        }
    }
}
