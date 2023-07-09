using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Set Properties")]
    [SerializeField] private int healthMax;
    //[SerializeField] LayerMask dammageContacts;
    [SerializeField] private LayerMask destroyContacts;

    [Header("Internal")]
    [SerializeField] int healthCurrent;

    private void Awake() {
        healthCurrent = healthMax;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        int layerMask = 1 << collision.gameObject.layer;
        //if ((layerMask & dammageContacts) != 0) {
        //    TakeDamage();
        //}

        if ((layerMask & destroyContacts) != 0) {
            Destroy(gameObject);
        }
    }

    private void CheckDeath() {
        if(healthCurrent <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(int amount = 1) {
        healthCurrent -= amount;
        CheckDeath();
    }
}
