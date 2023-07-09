using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetaData", menuName = "ScriptableObjects/MetaData", order = 1)]
public class MetaData : ScriptableObject {
	public bool HeroHasDoubleJump;
	public int HeroHealth = 2;
	public float ArenaWidth;
	public int MaxMinionAmount;

	public Hero ActiveHero;

	public bool PlayerIsWon;
	public bool PlayerIsLoose;

	public float HeroProgress;
	public int LevelCount = 1;

	public AudioClip SpawnSFX;

	public void Reset() {
		LevelCount = 1;
		HeroHealth = 2;
		HeroHasDoubleJump = false;
	}

	public void IncreasaeLevel() {
		LevelCount++;
		if (LevelCount >= 1) {
			HeroHealth = 3;
		} else if (LevelCount >= 2) {
			HeroHealth = 4;
		} else if (LevelCount >= 3) {
			HeroHasDoubleJump = true;
		}
	}
}
