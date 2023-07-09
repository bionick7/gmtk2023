using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private int MinionLayer;

	[SerializeField] private int DMG = 1;
	[SerializeField] private LayerMask ImpactLayermask;

	public Vector2 Velocity;

	private void Awake() {
		MinionLayer = LayerMask.NameToLayer("Minion");
	}

	private void Update() {
		transform.position += (Vector3) Velocity * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == MinionLayer) {
			Health health = collision.gameObject.GetComponent<Health>();
			health.TakeDamage(DMG);
			gameObject.SetActive(false);
		} else if ((1 << collision.gameObject.layer & ImpactLayermask) != 0) {
			gameObject.SetActive(false);
		}
	}
}
