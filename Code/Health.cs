using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    private SoundTrigger SoundTrigger;

    [Header("Set Properties")]
    public int healthMax;
    //[SerializeField] LayerMask dammageContacts;
    public LayerMask destroyContacts;

    [Header("Internal")]
    public int healthCurrent;

    public bool AutoKill = true;
    public bool isDead = false;

    public AudioClip DieSFX;

    private void Awake() {
        healthCurrent = healthMax;
    }

    private void Start() {
        SoundTrigger = GetComponent<SoundTrigger>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        int layerMask = 1 << collision.gameObject.layer;
        //if ((layerMask & dammageContacts) != 0) {
        //    TakeDamage();
        //}

        if ((layerMask & destroyContacts) != 0) {
            Die();
        }
    }

    private void CheckDeath() {
        if(healthCurrent <= 0) {
            Die();
        }
    }

    public void TakeDamage(int amount = 1) {
        healthCurrent -= amount;
        CheckDeath();
    }

    private void Die() {
        isDead = true;
        if (AutoKill) Destroy(gameObject);
        if (SoundTrigger != null && DieSFX != null) {
            SoundTrigger.PlayClip(DieSFX);
        }
    }
}
