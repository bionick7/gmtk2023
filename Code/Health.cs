using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Set Properties")]
    [SerializeField] int healthMax;
    [SerializeField] LayerMask destroyContacts;

    [Header("Internal")]
    [SerializeField] int healthCurrent;

    private void Awake()
    {
        healthCurrent = healthMax;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage();

        // A way for the Hero to Destroy Minions it touches.
        int layer_match = destroyContacts & (1 << collision.gameObject.layer);
        if (layer_match != 0)
            Destroy(collision.gameObject);
    }

    private void CheckDeath()
    {
        if(healthCurrent <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(int amount = 1)
    {
        healthCurrent -= amount;
        CheckDeath();
    }
}
