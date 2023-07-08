using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState {
    Moving,
    Jumping,
}

// "Hero" is the entity that the player has to fight. It is controlled by the game
public class Hero : MonoBehaviour {
    private int RaycastCollisionLayerMask;
    private int TerrainLayerMask;

    private float ExpectedJumpZenith;

    public float HoriziontalSpeed = 10;
    public float JumpDistance = 10;
    public float MaxDodgableMinionSpeed = 20;
    public float Gravity = 0;
    public float FloorHeight = 0;

    public bool CanDoubleJump = false;
    public bool UsedDoubleJump = false;

    public MovementState MovState = MovementState.Moving;

    public Transform RaycastOrigin;

    private Rigidbody2D RB;
    [HideInInspector] public Health Health;
    private Vector2 Velocity = Vector2.zero;

    private void Awake() {
        RaycastCollisionLayerMask = LayerMask.GetMask("Minion");
        TerrainLayerMask = LayerMask.GetMask("Terrain");
    }

    private void Start() {
        RB = GetComponent<Rigidbody2D>();
        Health = GetComponent<Health>();
    }

    /*private void Update() {
        Vector2 tgtVelocity = new Vector2(HoriziontalSpeed, 0);
        transform.position += (Vector3) tgtVelocity * Time.deltaTime;  // Camera jitters if we only update rb position on fixedUpdate, so we do both
    }*/

    private void FixedUpdate() {
        switch (MovState) {
            case MovementState.Moving: { 
                Velocity = new Vector2(HoriziontalSpeed, 0);

                float castDistance = JumpDistance * (1 + MaxDodgableMinionSpeed / HoriziontalSpeed);
                RaycastHit2D hit = Physics2D.BoxCast(RB.position, Vector2.one, 0, Vector2.right, castDistance, RaycastCollisionLayerMask);
                if (hit) {
                    Debug.DrawLine(transform.position, hit.point);
                    float relative_vel_x = HoriziontalSpeed - hit.rigidbody.velocity.x;
                    float airTimeRequired = hit.distance / relative_vel_x;
                    float airTime = JumpDistance / HoriziontalSpeed;
                    if (airTimeRequired <= airTime) {
                        Jump();
                    }
                }
                break; }
            case MovementState.Jumping: { 
                if (CanDoubleJump && !UsedDoubleJump && Time.time > ExpectedJumpZenith) {
                    RaycastHit2D hit = Physics2D.BoxCast(RB.position, Vector2.one * 1.5f, 0, Velocity.normalized, 100, RaycastCollisionLayerMask);
                    Debug.DrawRay(RB.position, Velocity.normalized * 100);
                    if (hit) {
                        UsedDoubleJump = true;
                        Jump();
                    }
                }
                if (RB.position.y <= FloorHeight && Time.time > ExpectedJumpZenith) {
                    RB.position = new Vector2(RB.position.x, FloorHeight);
                    Velocity = new Vector2(HoriziontalSpeed, 0);
                    MovState = MovementState.Moving;
                    UsedDoubleJump = false;
                    Debug.Log("Landing");
                } else {
                    Velocity.y -= Gravity * Time.fixedDeltaTime;
                }
                break; }
        }
        Vector2 p = RB.position + Velocity * Time.fixedDeltaTime;
        RB.MovePosition(p);
    }

    private void Jump() {
        MovState = MovementState.Jumping;
        float airTime = JumpDistance / HoriziontalSpeed;
        float v_y = airTime * Gravity / 2f;
        Velocity = new Vector2(HoriziontalSpeed, v_y);
        ExpectedJumpZenith = Time.time + airTime / 2;
        //Debug.Log($"v = {Velocity}");
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log($"Hit {collision.gameObject}");
        if (MovState != MovementState.Jumping || Time.time < ExpectedJumpZenith) {
            return;
        }
        if ((1 << collision.gameObject.layer & TerrainLayerMask) != 0) {
            MovState = MovementState.Moving;
            RB.bodyType = RigidbodyType2D.Kinematic;
        }
    }*/
}

