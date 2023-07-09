using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int minionCountCurrent = 0;
    [SerializeField] private TextMeshProUGUI spawnCounterText;
    [SerializeField] private GameObject holdObject;

    public MetaData MetaData;

    // Referencing this so we can still access the minion data.
    // This way we can access things like placement rules or what unit to spawn.
    private MinionData selectedMinion;

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

        // Check Valid Placement

        // Check Combination or Special Placement (ghost rules)

        // Check for Player Input to Place/Confirm
        if (Input.GetMouseButtonUp(0))
        {
            PlaceHoldObject();
        }
    }

    private void CancelHoldObject()
    {
        // Right Click --> Remove Hold Object
        if (Input.GetMouseButtonUp(1))
        {
            if (holdObject) {
                Destroy(holdObject);
            }

            // Reset back to Default Timescale
            SetTimeScale(1f);
        }
    }


    private bool CheckMinionCount()
    {
        return minionCountCurrent < MetaData.MaxMinionAmount;
    }

    private void PlaceHoldObject()
    {
        // Check Minion Count --> Need to highlight red
        if (!holdObject || !CheckMinionCount())
        {
            return;
        }

        if (!ghost.isInsideGhostField)
        {
            return;
        }

        // ~~Do~interactions~here~~ Actually, deviate to Minion.FuseTo(_)
        GameObject compagnion = ghost.targetMinion;
        
        //Destroy(holdObject);

        GameObject spawn = Instantiate(selectedMinion.minionPrefab, mousePosition, Quaternion.identity);
        Minion spawnMinon = spawn.GetComponent<Minion>();
        spawnMinon.Setup();
        spawnMinon.RB.velocity = ghost.spawnVelocity;
        spawnMinon.RB.position = ghost.spawnPosition;
        Debug.Log($"Launch {spawn} with v = {ghost.spawnVelocity}");
        DestroyCallback destroy_callback = spawn.GetComponent<DestroyCallback>();
        destroy_callback.Subscribe(() =>
        {
            minionCountCurrent--;
            UpdateCounterText();
        });

        if (compagnion != null)
        {
            spawn.GetComponent<Minion>().InteractTo(compagnion.GetComponent<Minion>());
        }


        // Update Count and Text
        minionCountCurrent++;
        UpdateCounterText();

        // Reset back to Default Timescale
        if (minionCountCurrent == MetaData.MaxMinionAmount) {
            SetTimeScale(1f);
        }
    }

    public void UpdateCounterText()
    {
        if (!spawnCounterText)
        {   
            // Check for the Spawn Counter
            GameObject spawnCounter = GameObject.Find("Spawn Counter - Text");

            if (!spawnCounter)
                return;

            spawnCounterText = spawnCounter.GetComponent<TextMeshProUGUI>();
        }

        string text = $"{minionCountCurrent} /{MetaData.MaxMinionAmount}";
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
