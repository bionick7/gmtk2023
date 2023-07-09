using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public MetaData MetaData;

    private Health Health;

    private void Start() {
        Health = GetComponent<Health>();

        transform.position += new Vector3(MetaData.ArenaWidth, 0, 0);
    }

    private void Update() {
        if (MetaData.HeroProgress >= MetaData.ArenaWidth) {
            // Active
        }
        if (Health.isDead) {
            MetaData.PlayerIsLoose = true;
        }
    }
}
