using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public static ManageScene Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void AddScene(string scene_name)
    {
        SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
    }

    public void UnloadScene(string scene_name)
    {
        SceneManager.UnloadSceneAsync(scene_name);
    }
}
