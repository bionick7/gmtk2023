using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlacement : MonoBehaviour
{
    public bool isInsideGhostField;
    public bool isInsideTerrain;
    public GameObject targetMinion;

    private int ghostFieldLayer;
    private int terrainLayer;
    private int minionLayer;

    private void Awake() {
        ghostFieldLayer = LayerMask.NameToLayer("GhostField");
        terrainLayer = LayerMask.NameToLayer("Terrain");
        minionLayer = LayerMask.NameToLayer("Minion");
    }

    private void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        int layer = collision.gameObject.layer;

        // Check is inside of GhostField
        if (layer == ghostFieldLayer)
            isInsideGhostField = true;

        // Check is inside of Terrain
        if (layer == terrainLayer)
            isInsideTerrain = true;

        // Check is overlapping a Minion
        if (layer == minionLayer)
            targetMinion = collision.gameObject;

        // Check if inside of Terrain
        // Check if inside of Minion
    }

    private void OnTriggerExit2D(Collider2D collision) {
        int layer = collision.gameObject.layer;

        // Check is outside of GhostField
        if (layer == ghostFieldLayer)
            isInsideGhostField = false;

        // Check is outside of Terrain
        if (layer == terrainLayer)
            isInsideTerrain = false;

        // Check is not overlapping a Minion
        if (layer == minionLayer)
            targetMinion = null;
    }
}
