using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldBuilder : MonoBehaviour {

    //enum for floor types (Like in binding of isaac) e.g. Basement I, Basement II, Womb, Sheol.
    public enum FloorType
    {
        RabbitHole,
        Pool,
        HookahLounge,
        DuchessHouse,
        TeaParty,
        QueensLair,
        WonderLand
    };

    int levelSeed;

    [SerializeField]
    private int currentFloor;
    
    private int startRoom; //To be generated within the first 
    private int endRoom;

    //Floor Size
    public static int floorSizeX = 5;
    public static int floorSizeY = 5;

    [SerializeField]
    private Room[] rooms; //Holds rooms on current floor

    // Use this for initialization
    void Start () {
        levelSeed = Random.Range(int.MinValue, int.MaxValue); //Level Seed Stuff
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>().text = "Seed: " + levelSeed;

        GenerateFloor();
	}

    public void TestGen()
    {
    }

    public void GenerateFloor()
    {
        rooms = new Room[floorSizeX * floorSizeY];

        //Creates empty floor (Probably exists a better solution)
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i] = ScriptableObject.CreateInstance<Room>();
        }

        System.Random sr = new System.Random(levelSeed);
        int startIndex = sr.Next(0, floorSizeX * floorSizeY - 1);
        rooms[startIndex].roomType = Room.RoomType.Start;
        rooms[startIndex].name = "Start Room"; //Debug purpose
        
        //Seed start room
        int RoomTypeCount = System.Enum.GetNames(typeof(Room.RoomType)).Length;


        //Seed end room

    }

    static int Calc_W(int index)
    {
        int newIndex = 0;

        if(index % floorSizeX == 0)
        {
            newIndex = index + floorSizeX - 1;
        } else
        {
            newIndex = index - 1;
        }

        return newIndex;
    }

    static int Calc_N(int index)
    {
        int newIndex = 0;
        if (index + floorSizeX > (floorSizeX * floorSizeY - 1))
        {
            newIndex = index + floorSizeX - (floorSizeX * floorSizeY);
        }
        else newIndex = index + floorSizeX;

        return newIndex;
    }

    static int Calc_E(int index)
    {
        int newIndex = 0;

        if ((index - (floorSizeX - 1)) % floorSizeX == 0)
        {
            newIndex = index - (floorSizeX - 1);
        } else
        {
            newIndex = index + 1;
        }

        return newIndex;
    }

    static int Calc_S(int index)
    {
        int newIndex = 0;

        if(index - floorSizeX < 0)
        {
            newIndex = (floorSizeX * floorSizeY - 1) - Mathf.Abs(index - floorSizeX);
        } else
        {
            newIndex = index - floorSizeX;

        }
        return newIndex;
    }
}
