using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classical villain, controlled by the human
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    public enum ControlMode { Mouse, Keyboard };

    public float Acceleration = 10;
    public float MaxSpeed = 10;
    public Vector2 NaturalVelocity = new Vector2(15, 0);
    public ControlMode Mode;

    [SerializeField] private MinionData[] MinionOptions;
    private float[] MinionCoolDowns;

    private Vector2 Velocity = Vector2.zero;
    private Camera Camera;

    private Rigidbody2D RB;

    private void Start() {
        Camera = Camera.main;
        RB = GetComponent<Rigidbody2D>();

        MinionCoolDowns = new float[MinionOptions.Length];
        for (int i=0; i < MinionOptions.Length; i++) {
            MinionCoolDowns[i] = 0;
        }
    }

    private void Update() {
        for (int i=0; i < MinionOptions.Length; i++) {
            MinionCoolDowns[i] = Mathf.Max(MinionCoolDowns[i] - Time.deltaTime, 0f);
        }

        // Dropping stuff
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Alpha1)) {  // Only one option rn
            RequestMinionDrop(0);
        }
    }

    private void FixedUpdate() {

        // Movement
        Vector3 accVector = Vector3.zero;
        // TODO: would be cool if you could play it with a gamepad
        switch (Mode) {
            case ControlMode.Mouse:
                Vector3 mouseposInWorld = Camera.ScreenToWorldPoint(Input.mousePosition);
                accVector = mouseposInWorld - transform.position;
                accVector *= .1f;
                break;
            case ControlMode.Keyboard:
                float inputX = Input.GetAxis("Horizontal");
                float inputY = Input.GetAxis("Vertical");
                accVector = new Vector3(inputX, inputY, 0);
                break;
            default:
                break;
        }
        if (accVector.magnitude > 1) {
            accVector = accVector.normalized;
        }


        Vector2 acc = accVector * Acceleration;
        Vector2 Drag = Acceleration / MaxSpeed * (Velocity - NaturalVelocity);
        Velocity += (acc - Drag) * Time.fixedDeltaTime;
        RB.MovePosition(RB.position + Velocity * Time.fixedDeltaTime);
    }

    private void RequestMinionDrop(int index) {
        // TODO: replace with visible amount counter
        if (MinionCoolDowns[index] < 0.005) {
            MinionData minionData = MinionOptions[index];
            Minion minion = Instantiate<Minion>(minionData.Instance, transform.position, Quaternion.identity);
            MinionCoolDowns[index] = minionData.RespawnPeriod;

            Rigidbody2D minionRB = minion.GetComponent<Rigidbody2D>();
            minionRB.velocity = Velocity;
        }
    }
}
