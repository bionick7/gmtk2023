using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarterback : Minion {

	public Vector2 FixedVelocity;
	public AudioClip KickSFX;

	private void FixedUpdate() {
		if (!IsPassive && IsLanded) {
			RB.MovePosition(RB.position + FixedVelocity * Time.fixedDeltaTime);
		}
	}

	public void Kick(Minion minionB) {
		minionB.IsPassive = true;
		if (minionB is Quarterback qbB) {
			FixedVelocity += qbB.FixedVelocity;
		}
		if (KickSFX != null) SoundTrigger.PlayClip(KickSFX);
		minionB.Fly();
		minionB.CanLand = false;
		minionB.RB.velocity += new Vector2(-25, 5);
		FixedVelocity = new Vector2(-10, 0);
	}
}
