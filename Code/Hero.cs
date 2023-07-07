using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState {
    Moving,
    Jumping,
}

// "Hero" is the entity that the player has to fight. It is controlled by the game
public class Hero : MonoBehaviour {

    public MovementState MovState;

    private Rigidbody2D RB;

    public float HoriziontalSpeed = 10;
        
    private void Start() {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 tgtVelocity = new Vector2(HoriziontalSpeed, 0);
        transform.position += (Vector3) tgtVelocity * Time.deltaTime;
    }

    /*private void FixedUpdate() {
        Vector2 tgtVelocity = new Vector2(HoriziontalSpeed, 0);
        RB.MovePosition(RB.position + tgtVelocity * Time.fixedDeltaTime);
    }*/
}
