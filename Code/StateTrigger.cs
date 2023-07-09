using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTrigger : MonoBehaviour
{
    public void Transition(string name)
    {
        ManageScene.Instance.LoadScene(name);
    }

    public void AddScene(string name)
    {
        ManageScene.Instance.AddScene(name);
    }
}
