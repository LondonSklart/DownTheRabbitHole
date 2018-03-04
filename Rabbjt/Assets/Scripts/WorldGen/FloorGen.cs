using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Floor/NewFloor", order = 1)]
public class FloorGen : ScriptableObject {

    public string floorName;

    public bool loopingFloor = false;

    public int floorSizeX = 5;
    public int floorSizeY = 5;

    public int maxDifficulty = 5;

    public int[] itemList;

    public WorldBuilder.FloorType floorType;

    public GameObject[] wallsVis;
    public GameObject[] cornerWallsVis;
    public GameObject[] doorVis;
    public GameObject[] floorVis;
}
