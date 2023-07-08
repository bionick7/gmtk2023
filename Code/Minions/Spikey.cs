using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : Minion {

    public Vector2 FixedVelocity;

    private void FixedUpdate() {
        if (!IsPassive && IsLanded) {
            RB.MovePosition(RB.position + FixedVelocity * Time.fixedDeltaTime);
        }
    }
}
