using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_room : MonoBehaviour {

    //Instantiation data
    const int TILESIZE = 50;
    int tileSizeY;

    WorldBuilder worldBuilder;

    [SerializeField]
    GameObject uiRoom;
    private GameObject[] floorMap;

    private void Start()
    {
        worldBuilder = FindObjectOfType<WorldBuilder>();
        Debug.Log(uiRoom.name);
        UpdateUI();
    }

    private void UpdateUI()
    {//Constructs map overview from new rect transforms
        floorMap = new GameObject[WorldBuilder.floorSizeX * WorldBuilder.floorSizeY];
       
        for (int i = 0, y = 0; y < WorldBuilder.floorSizeY; y++)
        {
            for (int x = 0; x < WorldBuilder.floorSizeX; x++, i++)
            {


                floorMap[i] = GameObject.Instantiate(uiRoom, this.transform);
                floorMap[i].transform.position = new Vector3(x, y, 0f);
                floorMap[i].name = "x:" + x + " y:" + y;
            }
        }

    }

    private Dictionary<Room.RoomType, Color> roomColorMap = new Dictionary<Room.RoomType, Color>()
    {//Color Map
        { Room.RoomType.Start, Color.green},
        { Room.RoomType.End, Color.red},
        { Room.RoomType.Monster, Color.magenta},
        { Room.RoomType.Shop, Color.yellow},
        { Room.RoomType.HealingFountain, Color.blue},
        { Room.RoomType.Empty, Color.white},
    };


}
