using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroProgress : MonoBehaviour {

    public MetaData SceneComm;

    private Image Fill;

    private void Start() {
        Fill = GetComponent<Image>();
    }

    private void Update() {
        Fill.fillAmount = SceneComm.HeroProgress;
    }
}
