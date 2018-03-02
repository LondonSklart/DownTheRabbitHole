using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Enemy Encounter/new Encounter", order = 1)]
public class Encounter : ScriptableObject {

    new public string name;

    public List<GameObject> enemiesInRoom;
    
    [System.Flags]
    public enum ValidFloorType
    {
        RabbitHole = 1,
        Pool = 2,
        HookahLounge = 4,
        DuchessHouse = 8,
        TeaParty = 16,
        QueensLair = 32,
        WonderLand = 64
    }

    
    public ValidFloorType validFloors;
}
