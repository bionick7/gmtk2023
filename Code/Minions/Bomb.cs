using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Minion {

	protected override void Start() {
		base.Start();
		RB.angularVelocity = Random.Range(-1f, 1f);
	}
}
