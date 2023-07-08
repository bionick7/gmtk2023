using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int minionCountCurrent = 0;
    [SerializeField] private int minionCountMax = 3;

    [SerializeField] private TextMeshProUGUI spawnCounterText;

    // Referencing this so we can still access the minion data.
    // This way we can access things like placement rules or what unit to spawn.
    private MinionData selectedMinion;

    private GameObject holdObject;
    private GhostPlacement ghost;

    // Private References
    private Camera mainCamera;
    private Vector3 mousePosition;

    // Internal References
    private float fixedDeltaTime;

    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check for Cancel
        CancelHoldObject();

        // Move the Object
        MoveHoldObject();

        // Check Valid Placement

        // Check Combination or Special Placement (ghost rules)

        // Check Minion Count --> Need to highlight red
        bool can_place = CheckMinionCount();

        // Check for Player Input to Place/Confirm
        if (can_place)
        {
            PlaceHoldObject();
        }
    }

    private void CancelHoldObject()
    {
        // Right Click --> Remove Hold Object
        if (Input.GetMouseButtonDown(1) && holdObject)
        {
            Destroy(holdObject);

            // Reset back to Default Timescale
            SetTimeScale(1f);
        }
    }

    private void MoveHoldObject()
    {
        if (!holdObject)
            return;

        // Camera to World Position
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        holdObject.transform.position = mousePosition;
    }

    private bool CheckMinionCount()
    {
        return minionCountCurrent < minionCountMax;
    }

    private void PlaceHoldObject()
    {
        if (!holdObject || !Input.GetMouseButtonDown(0))
            return;

        Destroy(holdObject);

        GameObject spawn = Instantiate(selectedMinion.minionPrefab, mousePosition, Quaternion.identity);
        DestroyCallback destroy_callback = spawn.GetComponent<DestroyCallback>();
        destroy_callback.Subscribe(() =>
        {
            minionCountCurrent--;
            UpdateCounterText();
        });

        // Update Count and Text
        minionCountCurrent++;
        UpdateCounterText();

        // Reset back to Default Timescale
        SetTimeScale(1f);
    }

    public void UpdateCounterText()
    {
        string text = minionCountCurrent.ToString() + "/" + minionCountMax.ToString();
        spawnCounterText.text = text;
    }

    public void SetHoldObject(MinionData minionData)
    {
        selectedMinion = minionData;

        // Replace the holdObject with the new minion.
        if (holdObject)
            Destroy(holdObject);

        holdObject = Instantiate(minionData.ghostPrefab, transform);
        ghost = holdObject.GetComponent<GhostPlacement>();

        // Slow Time while Placing
        SetTimeScale(0.1f);
    }

    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = fixedDeltaTime * scale;
    }
}
