using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Class for minions

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Minion : MonoBehaviour {

	protected Rigidbody2D RB;

	protected virtual void Start() {
		RB = GetComponent<Rigidbody2D>();
	}
}
