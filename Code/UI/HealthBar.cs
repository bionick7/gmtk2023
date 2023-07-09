using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {

    public Image Slider;
    public TextMeshProUGUI Number;

    public MetaData SceneComm;

    private void Update() {
        Number.text = $"{SceneComm.ActiveHero.Health.healthCurrent}";
        Slider.fillAmount = SceneComm.ActiveHero.Health.healthCurrent / SceneComm.HeroHealth;
    }
}
