using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_room : MonoBehaviour {

    //Instantiation data
    const int TILESIZE = 50;
    int tileSizeY;
    private Dictionary<Room.RoomType, Sprite> roomColorMap;
    PlayerController player;
    WorldBuilder worldBuilder;
    public Sprite[] floorBG;
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
        roomColorMap = new Dictionary<Room.RoomType, Sprite>()
        {//Color Map
        { Room.RoomType.Start, floorBG[0]},//Start
        { Room.RoomType.End, floorBG[1]},//End
        { Room.RoomType.Monster, floorBG[2]},//Monster
        { Room.RoomType.Shop, floorBG[3]},//Shop
        { Room.RoomType.HealingFountain, floorBG[4]},//Healing Fountain
        { Room.RoomType.Empty, floorBG[5]},//Empty
    };
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
                currentRoom.GetComponent<uiRoomData>().roomBG.GetComponent<Image>().sprite = roomColorMap[worldBuilder.rooms[i].roomType];
                
                GameObject[] monsterIcons = currentRoom.GetComponent<uiRoomData>().monsterIcons;
                for(int j = 0; j < monsterIcons.Length; j++)
                {
                    if(worldBuilder.rooms[i].Encounter != null)
                    {
                        if (worldBuilder.rooms[i].Encounter.enemiesInRoom[j] == null) break;
                        else
                        {
                        monsterIcons[j].GetComponent<Image>().sprite = worldBuilder.rooms[i].Encounter.enemiesInRoom[j].GetComponentInChildren<EnemyController>().uiIcon;

                        }
                        
                    }
                    else if(worldBuilder.rooms[i].enemiesInRoom != null)
                    {
                        if (worldBuilder.rooms[i].enemiesInRoom[j] == null) break;
                        else
                        {
                        monsterIcons[j].GetComponent<Image>().sprite = worldBuilder.rooms[i].enemiesInRoom[j].GetComponentInChildren<EnemyController>().uiIcon;

                        }
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

    //private Dictionary<Room.RoomType, Sprite> roomColorMap = new Dictionary<Room.RoomType, Sprite>()
    //{//Color Map
    //    { Room.RoomType.Start, floorBG[0]},//Start
    //    { Room.RoomType.End, floorBG[1]},//End
    //    { Room.RoomType.Monster, floorBG[2]},//Monster
    //    { Room.RoomType.Shop, floorBG[3]},//Shop
    //    { Room.RoomType.HealingFountain, floorBG[4]},//Healing Fountain
    //    { Room.RoomType.Empty, floorBG[5]},//Empty
    //};


}
