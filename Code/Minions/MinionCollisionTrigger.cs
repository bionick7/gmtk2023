using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCollisionTrigger : MonoBehaviour {
	private Minion Minion;

	private void Start() {
		Minion = GetComponentInParent<Minion>();
	}
	private void OnTriggerEnter2D(Collider2D collision) {
		//Minion.OnCollision(collision);
	}
}
