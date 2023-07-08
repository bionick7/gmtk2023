using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public GameObject buttonPrefab;

    public List<MinionData> minionList = new List<MinionData>();

    public List<KeyCode> hotkeys = new List<KeyCode>();

    private PlayerController playerController;

    private void Start()
    {
        // Can better set this.
        GameObject player_controller_game_object = GameObject.Find("PlayerController");
        if(player_controller_game_object)
            playerController = player_controller_game_object.GetComponent<PlayerController>();

        UpdateToolbarDisplay();
    }

    private void Update()
    {
        // Hotkeys
        for (int i = 0; i < hotkeys.Count; i++)
        {
            if (Input.GetKey(hotkeys[i]) && minionList.Count > i)
            {
                playerController.SetHoldObject(minionList[i]);
            }
        }
    }

    private void UpdateToolbarDisplay()
    {
        // TODO: Come back and make this more performant.
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Generate New Set of Buttons and hook them up.
        //for (int i = 0; i < minionList.Count; i++)
        //{
        //    GameObject button_game_object = Instantiate(buttonPrefab, transform);
        //    Button button = button_game_object.GetComponent<Button>();
        //    button.onClick.AddListener(delegate { playerController.SetHoldObject(minionList[i]); });

        //}

        foreach(MinionData minionData in minionList)
        {
            GameObject button_game_object = Instantiate(buttonPrefab, transform);
            Button button = button_game_object.GetComponent<Button>();
            button.onClick.AddListener(delegate { playerController.SetHoldObject(minionData); });
        }
    }
}
