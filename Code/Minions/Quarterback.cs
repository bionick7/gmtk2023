using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarterback : Minion {

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

	public override void FuseTo(Minion minionB) {
		base.FuseTo(minionB);
		minionB.IsPassive = true;
		if (minionB is Quarterback qbB) {
			FixedVelocity += qbB.FixedVelocity;
		}
		Debug.Log($"{name} - {minionB.name} ; FixedVelocity = {FixedVelocity}");
		heldObjectRB = minionB.RB;
		minionB.OnLand();
		holdingOffset = heldObjectRB.position - RB.position;
	}
}
