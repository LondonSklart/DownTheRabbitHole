using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_room : MonoBehaviour {

    //Instantiation data
    const int TILESIZE = 50;
    int tileSizeY;

    PlayerController player;
    WorldBuilder worldBuilder;

    [SerializeField]
    GameObject uiRoom;
    private GameObject[] floorMap;

    private void Start()
    {
        worldBuilder = FindObjectOfType<WorldBuilder>();
        player = FindObjectOfType<PlayerController>();
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
                //worldBuilder.rooms[];
                GameObject currentRoom = floorMap[i];

                currentRoom = GameObject.Instantiate(uiRoom, this.transform);
                currentRoom.transform.localPosition = new Vector3(x*TILESIZE-(WorldBuilder.floorSizeX*TILESIZE)/2+TILESIZE/2, y*TILESIZE-(WorldBuilder.floorSizeY * TILESIZE)/2 + TILESIZE / 2, 0f);
                currentRoom.name = "x:" + x + " y:" + y;

                GameObject[] monsterIcons = currentRoom.GetComponent<uiRoomData>().monsterIcons;
                for(int j = 0; j < monsterIcons.Length; j++)
                {
                    //Set logic for monser icons monsterIcons[j] = worldBuilder.rooms[i].enemiesInRoom[j];
                }
                //floorMap[i]
                
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
