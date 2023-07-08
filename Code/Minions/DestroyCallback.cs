using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyCallback : MonoBehaviour
{
    UnityEvent OnDestroyed;

    private void Awake()
    {
        OnDestroyed ??= new UnityEvent();
    }

    private void OnDestroy()
    {
        OnDestroyed.Invoke();
    }

    public void Subscribe(UnityAction action)
    {
        OnDestroyed?.AddListener(action);
    }
}
