using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Minion {

	[SerializeField] private float ExplostionRadius = 5f;
	[SerializeField] private int Dammage = 3;
	[SerializeField] private float TimeUntilBoom = 2f;
	[SerializeField] private Vector2 InitVelocity = new Vector2(-5, 0);
	
	[SerializeField] private GameObject ExplostionRadiusIndicator;
	[SerializeField] private SpriteMask CountdownDialMask;

	private float ExplostionCountdown = 0f;
	public bool IsTicking = true;
	private bool IsExploding = false;

	public AudioClip ExplodeSFX;

	public override void Setup() {
		base.Setup();

		ExplostionCountdown = TimeUntilBoom;
		CountdownDialMask = GetComponentInChildren<SpriteMask>();

		RB.angularVelocity = Random.Range(-1f, 1f);
		//RB.velocity = InitVelocity;
	}

	private void Update() {
		if (IsTicking) {
			ExplostionCountdown -= Time.deltaTime;

			CountdownDialMask.transform.rotation = Quaternion.identity;  // lock rotation of "UI" while letting the rigid body move freely
			CountdownDialMask.alphaCutoff = ExplostionCountdown / TimeUntilBoom;
			if (ExplostionCountdown <= 0f) {
				Explode();
			}
		}
	}

	protected override void OnHitHero(Health heroHealth) {
		//base.OnHitHero(heroHealth);
		if (!IsExploding) {
			Explode();
		}
	}

	private void Explode() {
		if (ExplodeSFX != null) {
			SoundTrigger.PlayClip(ExplodeSFX);
		}
		foreach (Health health in FindObjectsOfType<Health>()) {  // Explosion is indiscriminate
			if (health.gameObject != gameObject && (health.transform.position - transform.position).magnitude <= ExplostionRadius) {
				health.TakeDamage(Dammage);
			}
		}
		IsTicking = false;
		IsExploding = true;
		StartCoroutine(ExplostionAnimation());
	}

	private IEnumerator ExplostionAnimation() {
		ExplostionRadiusIndicator.SetActive(true);
		ExplostionRadiusIndicator.transform.localScale = Vector3.one * ExplostionRadius * 2f;
		yield return new WaitForSeconds(0.4f);
		Destroy(gameObject);
	}
}
