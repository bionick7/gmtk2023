using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Class for minions

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Minion : MonoBehaviour {

	protected int terrainLayerMask;
	protected int minionLayerMask;
	protected int heroLayerMask;

	protected Hero Hero;

	public bool CanLand = true;  // If true, this minion will disable physics as soon as it hits the ground
	public bool CanInteractAtRuntime = true;  // Is true for e.g. the quarterback
	[SerializeField] private int ContactDmg = 0;
	[SerializeField] protected Collider2D TriggerCollider;

	public bool IsLanded = false;
	[HideInInspector] public Rigidbody2D RB;
	[HideInInspector] public bool IsPassive = false;  // If e.g. carried by quarterback

	protected Collider2D PhysicsCollider;

	public float TimePlaced = 0;
	public bool HasInteracted { get; private set; }

	private void Awake() {
		PhysicsCollider = GetComponent<Collider2D>();
		terrainLayerMask = LayerMask.GetMask("Terrain");
		minionLayerMask = LayerMask.GetMask("Minion");
		heroLayerMask = LayerMask.GetMask("Hero");
	}

	public virtual void Setup() {  // Called before start
		RB = GetComponent<Rigidbody2D>();
		Hero = FindObjectOfType<Hero>();

		TimePlaced = Time.realtimeSinceStartup;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		int collisionLayer = 1 << collision.gameObject.layer;
		if ((collisionLayer & terrainLayerMask) != 0 && CanLand) {
			Land();
		}

		if ((collisionLayer & minionLayerMask) != 0 && CanInteractAtRuntime && HasInteracted) {
			Minion compagnion = collision.gameObject.GetComponent<Minion>();
			if (compagnion != this && compagnion != null) {
				if (TimePlaced > compagnion.TimePlaced) {
					InteractTo(compagnion);
				}
			}
		}

		if ((collisionLayer & heroLayerMask) != 0) {
			Hero hero = collision.gameObject.GetComponent<Hero>();
			OnHitHero(hero.Health);
		}
	}

	public void InteractTo(Minion fuseParent) {  // this is minionA
		// Fuse A to B
		HasInteracted = true;
		if (!Physics2D.GetIgnoreCollision(PhysicsCollider, fuseParent.PhysicsCollider)) {
			Physics2D.IgnoreCollision(PhysicsCollider, fuseParent.PhysicsCollider);
		}
		fuseParent.IsInteractedFrom(this);

		// Rules in order of importance
		if (fuseParent is Spikey spikey) {
			spikey.Carry(this);
			return;
		}

		if (this is Quarterback qb) {
			qb.Kick(fuseParent);
			return;
		}

	}

	protected void IsInteractedFrom(Minion minionA) {  // this is minionB
		// Conjugate of InteractTo(_)

	}

	public virtual void Fly() {
		RB.bodyType = RigidbodyType2D.Dynamic;
		IsLanded = false;
	}

	public virtual void Land() {
		RB.bodyType = RigidbodyType2D.Dynamic;
		RB.useFullKinematicContacts = true;
		IsLanded = true;
	}

	protected virtual void OnHitHero(Health heroHealth) {
		Debug.Log($"Hit by {name}");
		heroHealth.TakeDamage(ContactDmg);
	}

}
