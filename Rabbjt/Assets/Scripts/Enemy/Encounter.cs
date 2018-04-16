using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Encounter/New Encounter", order = 1)]
public class Encounter : ScriptableObject {
    public int difficulty = 1; //Intended to help seed rooms defaults to 1(Future feature)

    public bool bossRoom = false;

    public int coinReward = 10;

	public List<GameObject> enemiesInRoom;

    public List<WorldBuilder.FloorType> validFloors;

    //Maybe add some logic for encounter specific buffs and other stuff
}
