using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinionSO", menuName = "ScriptableObjects/MinionSO", order = 1)]
public class MinionData : ScriptableObject {
	public string Name = "Undefined";
	public float RespawnPeriod = 0f;
	public int MaxVisibleUnits = 5;

	public Minion Instance;
}
