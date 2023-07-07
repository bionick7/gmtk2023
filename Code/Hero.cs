using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MovementState {
    Moving,
    Jumping,
}

// "Hero" is the entity that the player has to fight. It is controlled by the game
public class Hero : MonoBehaviour {

    public MovementState MovState;

    public float HoriziontalSpeed = 10;
        
    void Start() {

    }

    void Update() {
        switch(MovState) {

        }
        /*Vector3 v = Vector3.zero;
        if (Input.GetKey(KeyCode.D)) {
            v.x += Speed;
        }
        else if (Input.GetKey(KeyCode.A)) {
            v.x -= Speed;
        }
        transform.position = transform.position + v * Time.deltaTime;*/
    }
}
