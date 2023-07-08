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
	public bool CanFuseAtRuntime = true;  // Is true for e.g. the quarterback
	[SerializeField] private int ContactDmg = 0;
	[SerializeField] protected Collider2D TriggerCollider;

	public bool IsLanded = false;
	[HideInInspector] public Rigidbody2D RB;
	[HideInInspector] public bool IsPassive = false;  // If e.g. carried by quarterback

	protected Collider2D PhysicsCollider;

	public float TimePlaced = 0;
	public Minion FuseParent { get; private set; }

	private void Awake() {
		PhysicsCollider = GetComponent<Collider2D>();
		terrainLayerMask = LayerMask.GetMask("Terrain");
		minionLayerMask = LayerMask.GetMask("Minion");
		heroLayerMask = LayerMask.GetMask("Hero");
	}

	protected virtual void Start() {
		RB = GetComponent<Rigidbody2D>();
		Hero = FindObjectOfType<Hero>();

		TimePlaced = Time.realtimeSinceStartup;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		int collisionLayer = 1 << collision.gameObject.layer;
		if ((collisionLayer & terrainLayerMask) != 0 && CanLand) {
			IsLanded = true;
			OnLand();
		}

		if ((collisionLayer & minionLayerMask) != 0 && CanFuseAtRuntime && FuseParent == null) {
			Minion compagnion = collision.gameObject.GetComponent<Minion>();
			if (compagnion != this && compagnion != null) {
				if (TimePlaced > compagnion.TimePlaced) {
					FuseTo(compagnion);
				}
			}
		}

		if ((collisionLayer & heroLayerMask) != 0) {
			Hero hero = collision.gameObject.GetComponent<Hero>();
			OnHitHero(hero.Health);
		}
	}

	public virtual void FuseTo(Minion minionB) {  // this is minionA
		// Fuse A to B
		FuseParent = minionB;
		Physics2D.IgnoreCollision(PhysicsCollider, minionB.PhysicsCollider);
		minionB.GetFusedTo(this);
	}

	protected virtual void GetFusedTo(Minion minionA) {  // this is minionB
		// Conjugate of FuseTo(_)

	}

	public virtual void OnLand() {
		RB.bodyType = RigidbodyType2D.Kinematic;
		RB.useFullKinematicContacts = true;
	}

	protected virtual void OnHitHero(Health heroHealth) {
		Debug.Log($"Hit by {name}");
		heroHealth.TakeDamage(ContactDmg);
	}

}
