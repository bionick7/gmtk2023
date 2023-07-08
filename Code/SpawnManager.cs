using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private int minionCountCurrent = 0;
    private int minionCountMax = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Spawn(GameObject prefab)
    {
        // Prevent Spawning over Max
        if (minionCountCurrent >= minionCountMax)
            return;

        // Do Stuff
        Instantiate(prefab);
        minionCountCurrent++;
    }

    public int GetMinionCountCurrent()
    {
        return minionCountCurrent;
    }

    public int GetMinionCountMax()
    {
        return minionCountMax;
    }


}
