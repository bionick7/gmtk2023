using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaManager : MonoBehaviour {

    public static MetaManager Active;    
    public MetaData MetaData;
    public int MainSceneIndex, MetaSceneIndex;

    private void Awake() {
        if (Active == null) Active = this;
        else Destroy(gameObject);
    }

    private void Start() {
        MetaData.Reset();
        DontDestroyOnLoad(gameObject);
    }

    public void Reload() {
        Debug.Log("Reload");
        SceneManager.LoadScene(MainSceneIndex, LoadSceneMode.Single);
        SceneManager.LoadScene(MetaSceneIndex, LoadSceneMode.Additive);
    }

    private void Update() {
        if (MetaData.PlayerIsLoose) {
            MetaData.Reset();
            MetaData.PlayerIsLoose = false;
            Reload();
        }
        else if (MetaData.PlayerIsWon) {
            MetaData.IncreasaeLevel();
            MetaData.PlayerIsWon = false;
            Reload();
        }
    }
}
