using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlacement : MonoBehaviour {

    public bool isInsideGhostField;
    public bool isInsideTerrain;
    public GameObject targetMinion;

    private int ghostFieldLayer;
    private int terrainLayer;
    private int minionLayer;

    [HideInInspector] public Vector2 spawnPosition;
    [HideInInspector] public Vector2 spawnVelocity;
    public bool allowVelocity;
    public float maxVelocity;
    private Camera Camera;
    private LineRenderer LineRenderer;

    private void Awake() {
        ghostFieldLayer = LayerMask.NameToLayer("GhostField");
        terrainLayer = LayerMask.NameToLayer("Terrain");
        minionLayer = LayerMask.NameToLayer("Minion");
    }

    private void Start() {
        Camera = Camera.main;
        LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        const float VELOCITY_SCALE = 2f;
        if (Input.GetMouseButtonDown(0)) {
            spawnPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (allowVelocity) {
            LineRenderer.enabled = Input.GetMouseButton(0);
            Vector2 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButton(0)) {
                spawnVelocity = (mousePos - spawnPosition) * VELOCITY_SCALE;
                if (spawnVelocity.magnitude > maxVelocity) {
                    spawnVelocity = spawnVelocity.normalized * maxVelocity;
                }
            } else {
                spawnPosition = mousePos;
            }
        } else {
            LineRenderer.enabled = false;
            spawnVelocity = Vector2.zero;
            spawnPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            //LineRenderer.SetPositions(new Vector3[] { transform.position, transform.position });
        }
        LineRenderer.SetPositions(new Vector3[] { spawnPosition, spawnPosition + spawnVelocity / VELOCITY_SCALE });
        transform.position = spawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        int layer = collision.gameObject.layer;

        // Check is inside of GhostField
        if (layer == ghostFieldLayer) {
            isInsideGhostField = true;
        }

        // Check is inside of Terrain
        if (layer == terrainLayer) {
            isInsideTerrain = true;
        }

        // Check is overlapping a Minion
        if (layer == minionLayer) {
            targetMinion = collision.gameObject;
            Debug.Log($"Enter {collision.gameObject}");
        }

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
        if (layer == minionLayer) {
            targetMinion = null;
            Debug.Log($"Exit {collision.gameObject}");
        }
    }
}
