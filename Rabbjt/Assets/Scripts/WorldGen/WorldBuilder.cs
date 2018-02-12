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

    int runSeed; //Used to seed entire run.
    int[] floorSeed = new int[7]; //Used for predictable seeding of floor sets. (Maybe overkill)

    public FloorGen[] floors = new FloorGen[7];

    private System.Random sr;
    private System.Random sfr;

    [SerializeField]
    private int currentFloor;
    
    private int startRoom; //To be generated within the first 
    private int endRoom;
    List<int> invalidIndex = new List<int>();

    //Floor Size
    public static int floorSizeX = 5;
    public static int floorSizeY = 5;

    [SerializeField]
    public Room[] rooms; //Holds rooms on current floor

    // Use this for initialization
    void Start () {
        runSeed = Random.Range(int.MinValue, int.MaxValue); //Level Seed Stuff
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>().text = "Seed: " + runSeed;
        sr = new System.Random(runSeed);

        for(int i = 0; i < floorSeed.Length; i++)
        {
            floorSeed[i] = sr.Next();
            //Debug.Log("Floor "+i+": "+floorSeed[i]);
        }

        GenerateFloor();
	}

    public void TestGen()
    {
        for (int x = 0; x < floorSizeX; x++)
        {
            for(int y = 0; y< floorSizeY; y++)
            {

            }
        }
    }

    public void GenerateFloor()
    {
        
        sfr = new System.Random(floorSeed[currentFloor]); //Gets random seed based on current floor
        rooms = new Room[floorSizeX * floorSizeY];
        Debug.Log("Amount of rooms: "+rooms.Length);
        //Creates empty floor (Probably exists a better solution)
        for(int i = 0; i < rooms.Length; i++)
        {
            //Debug.Log("Room made");
            rooms[i] = new Room(Room.RoomType.Empty);

        }

        //Seed start room
        startRoom = sfr.Next(0, floorSizeX * floorSizeY - 1);
        rooms[startRoom].roomType = Room.RoomType.Start;
        rooms[startRoom].name = "Start Room";

        InvalidIndex(startRoom);


        int RoomTypeCount = System.Enum.GetNames(typeof(Room.RoomType)).Length;

        //Seed Shop


        //Seed Healing Fountain

        //Seed end room

        for(int i = 0; i < rooms.Length; i++)
        {
            Debug.Log(i +": "+rooms[i].name);
        }
    }

    public void InvalidIndex(int index)
    { //Sets a cross of invalid indexes around the startindex(This should be updated for looping floors later on but requires a rewrite of the Calc_[dir] functions)
        invalidIndex.Add(index);
        invalidIndex.Add(Calc_N(index));
        invalidIndex.Add(Calc_E(index));
        invalidIndex.Add(Calc_S(index));
        invalidIndex.Add(Calc_W(index));

    }

    public static int randomDir(int index, bool looping = false)
    {
        int indexDir = 0;
        


        return indexDir;
    }

    public static int Calc_W(int index)
    {
        int newIndex = 0;

        if(index % floorSizeX == 0) //Checks if index is along left wall of Grid
        {
            newIndex = index + floorSizeX - 1;
            

        } else
        {
            newIndex = index - 1;
        }

        return newIndex;
    }

    public static int Calc_N(int index)
    {
        int newIndex = 0;
        if (index + floorSizeX > (floorSizeX * floorSizeY - 1))
        {
            newIndex = index + floorSizeX - (floorSizeX * floorSizeY);
        }
        else newIndex = index + floorSizeX;

        return newIndex;
    }

    public static int Calc_E(int index)
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

    public static int Calc_S(int index)
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
