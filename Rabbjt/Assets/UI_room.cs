using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_room : MonoBehaviour {

    //Instantiation data
    const int TILESIZE = 50;
    int tileSizeY;

    PlayerController player;
    WorldBuilder worldBuilder;

    [SerializeField]
    GameObject uiRoom;
    private GameObject[] floorMap;
    bool initialized = false;

    private void Init()
    {
        worldBuilder = FindObjectOfType<WorldBuilder>();
        player = FindObjectOfType<PlayerController>();
        Debug.Log(uiRoom.name);
        initialized = true;
        UpdateUI();
    }

    public void UpdateUI()
    {//Constructs map overview from new rect transforms

        if (!initialized)
        {
            Init();
            Debug.Assert(worldBuilder != null);
            return;
        }
        if (floorMap != null)
        {
            for(int i = 0; i< floorMap.Length; i++)
            {
                GameObject.Destroy(floorMap[i]);
            }
        }
        floorMap = new GameObject[WorldBuilder.floorSizeX * WorldBuilder.floorSizeY];
        Debug.Log("Floor map lenght:"+ floorMap.Length);
        for (int i = 0, y = 0; y < WorldBuilder.floorSizeY; y++)
        {
            for (int x = 0; x < WorldBuilder.floorSizeX; x++, i++)
            {
                //worldBuilder.rooms[];
                GameObject currentRoom;

                currentRoom = GameObject.Instantiate(uiRoom, this.transform);
                currentRoom.transform.localPosition = new Vector3(x*TILESIZE-(WorldBuilder.floorSizeX*TILESIZE)/2+TILESIZE/2, y*TILESIZE-(WorldBuilder.floorSizeY * TILESIZE)/2 + TILESIZE / 2, 0f);
                currentRoom.name = "x:" + x + " y:" + y;

                floorMap[i] = currentRoom;

                GameObject[] monsterIcons = currentRoom.GetComponent<uiRoomData>().monsterIcons;
                for(int j = 0; j < monsterIcons.Length; j++)
                {
                    if(worldBuilder.rooms[i].Encounter != null)
                    {
                    monsterIcons[j].GetComponent<Image>().sprite = worldBuilder.rooms[i].Encounter.enemiesInRoom[j].GetComponent<EnemyController>().uiIcon;

                    }
                    else if(worldBuilder.rooms[i].enemiesInRoom != null)
                    {
                        monsterIcons[j].GetComponent<Image>().sprite = worldBuilder.rooms[i].enemiesInRoom[j].GetComponent<EnemyController>().uiIcon;
                    }
                
                    else { Debug.Log("Encounter is null for room: "+ i);
                        break;
                    }
                }
                //floorMap[i]
                
            }
        }
        floorMap[player.CurrentRoom].GetComponent<Image>().color = Color.yellow;

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
