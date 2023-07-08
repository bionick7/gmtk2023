using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public GameObject Hero;
    private float Offset;

    private Camera Camera;

    private void Start() {
        Offset = transform.position.x - Hero.transform.position.x;
        Camera = GetComponent<Camera>();
    }

    private void Update() {
        if (!Hero)
            return;

        Vector3 pos = transform.position;
        pos.x = Hero.transform.position.x + Offset;
        transform.position = pos;
    }

}
