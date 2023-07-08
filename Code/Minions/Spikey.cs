using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : Minion {

	public Vector2 FixedVelocity;

	private Rigidbody2D heldObjectRB = null;
	private Vector2 holdingOffset = Vector2.zero;

	private void FixedUpdate() {
		if (!IsPassive && IsLanded) {
			RB.MovePosition(RB.position + FixedVelocity * Time.fixedDeltaTime);
		}
		if (heldObjectRB != null) {
			heldObjectRB.MovePosition(RB.position + holdingOffset);
		}
	}

	public void Carry(Minion minionB) {
		minionB.IsPassive = true;
		heldObjectRB = minionB.RB;
		minionB.Land();
		holdingOffset = heldObjectRB.position - RB.position;
	}

}
