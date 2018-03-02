using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Encounter/New Encounter", order = 1)]
public class Encounter : ScriptableObject {

	public List<GameObject> enemiesInRoom;
}
