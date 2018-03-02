﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldBuilder : MonoBehaviour {

    //enum for floor types (Like in binding of isaac) e.g. Basement I, Basement II, Womb, Sheol.  Probably solved through
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
        //VisualGenerator();
	}

    

    public void GenerateFloor()
    {
        
        sfr = new System.Random(floorSeed[currentFloor]); //Gets random seed based on current floor

        floorSizeX = floors[currentFloor].floorSizeX;
        floorSizeY = floors[currentFloor].floorSizeY;

        rooms = new Room[floorSizeX * floorSizeY];
        Debug.Log("Amount of rooms: "+rooms.Length);
        //Creates empty floor (Probably exists a better solution)
        for(int i = 0; i < rooms.Length; i++)
        {
            //Debug.Log("Room made");
            rooms[i] = new Room(Room.RoomType.Empty);

        }

        //Seed start room
        startRoom = SeedRooms();
        rooms[startRoom].roomType = Room.RoomType.Start;
        rooms[startRoom].name = "Start Room";

        InvalidIndex(startRoom, true);


        int RoomTypeCount = System.Enum.GetNames(typeof(Room.RoomType)).Length;

        //Seed end room
        
        endRoom = SeedRooms();
        rooms[endRoom].roomType = Room.RoomType.End;
        rooms[endRoom].name = "End Room";

        InvalidIndex(endRoom);

        //Seed Shop

        int shop = SeedRooms();
        rooms[shop].roomType = Room.RoomType.Shop;
        rooms[shop].name = "End Room";

        InvalidIndex(shop);


        //Seed Healing Fountain

        int healingFountain = SeedRooms();
        rooms[healingFountain].roomType = Room.RoomType.HealingFountain;
        rooms[healingFountain].name = "End Room";

        InvalidIndex(healingFountain);

        //Seed monster rooms
        for(int i = 0; i < floorSizeX*floorSizeY; i++)
        {
        if(rooms[i])
        rooms[i]
        }

    }

    private int SeedRooms()
    {
        int newIndex = sfr.Next(0, floorSizeX * floorSizeY);
        while(invalidIndex.Contains(newIndex))
        {
            newIndex = sfr.Next(0, floorSizeX * floorSizeY);
        }
        return newIndex;
    }

    public void InvalidIndex(int index, bool cross = false)
    { //Sets a cross of invalid indexes around the startindex Calc_[dir] func returns floorSizeX * floorSizeY as an invalid token
        int oob_index = floorSizeX * floorSizeY;


        invalidIndex.Add(index);

        if (cross)
        {
        int[] refCoords = new int[4];
        refCoords[0] = (Calc_N(index, floors[currentFloor].loopingFloor));
        refCoords[1] = (Calc_E(index, floors[currentFloor].loopingFloor));
        refCoords[2] = (Calc_S(index, floors[currentFloor].loopingFloor));
        refCoords[3] = (Calc_W(index, floors[currentFloor].loopingFloor));

        for(int i = 0; i < refCoords.Length; i++)
        {
            if (refCoords[i] != oob_index && !invalidIndex.Contains(refCoords[i])) //Avoids duplicate entries
            {
                invalidIndex.Add(refCoords[i]);
            }

        }


        }
    }

    public int randomDir(int index, bool looping = false)
    {
        int rndDir = sfr.Next(0, 4);
        int indexOut = floorSizeX * floorSizeY;
        switch (rndDir)
        {
            case 0:
                indexOut = Calc_W(index, looping);
                break;
            case 1:
                indexOut = Calc_W(index, looping);
                break;
            case 2:
                indexOut = Calc_W(index, looping);
                break;
            case 3:
                indexOut = Calc_W(index, looping);
                break;
        }
        if (indexOut == floorSizeX*floorSizeY && looping == false) indexOut = randomDir(index, looping);
        return indexOut;
    }
    
    //These functions return FloorSizeX * FloorSizeY if the tile is invalid
    public static int Calc_W(int index, bool isLooping)
    {
        int newIndex = 0;

        if(index % floorSizeX == 0) //Checks if index is along left wall of Grid
        {
            if (isLooping) newIndex = index + floorSizeX - 1;
            else return floorSizeX * floorSizeY; //Should be outside array range and will be used for invalid rooms

        } else
        {
            newIndex = index - 1;
        }

        return newIndex;
    }

    public static int Calc_N(int index, bool isLooping)
    {
        int newIndex = 0;
        if (index + floorSizeX > (floorSizeX * floorSizeY - 1))
        {
            if (isLooping) newIndex = index + floorSizeX - (floorSizeX * floorSizeY);
            else return floorSizeX * floorSizeY;//Used for invalid positions
        }
        else newIndex = index + floorSizeX;

        return newIndex;
    }

    public static int Calc_E(int index, bool isLooping)
    {
        int newIndex = 0;

        if ((index - (floorSizeX - 1)) % floorSizeX == 0)
        {
            if (isLooping) newIndex = index - (floorSizeX - 1);
            else return floorSizeX * floorSizeY;
        } else
        {
            newIndex = index + 1;
        }

        return newIndex;
    }

    public static int Calc_S(int index, bool isLooping)
    {
        int newIndex = 0;

        if(index - floorSizeX < 0)
        {
            if (isLooping) newIndex = (floorSizeX * floorSizeY - 1) - Mathf.Abs(index - floorSizeX);
            else return floorSizeX * floorSizeY;
        } else
        {
            newIndex = index - floorSizeX;

        }
        return newIndex;
    }


    //General debug Rendering functions
    private GameObject[] meshMap;
    private void VisualGenerator()
    {//Constructs world overview from cube primitives
        meshMap = new GameObject[floorSizeX * floorSizeY];
        Debug.Log("Mesh map: " + meshMap.Length);
        for(int i = 0, y = 0; y < floorSizeY; y++)
        {
            for(int x = 0; x< floorSizeX; x++, i++)
            {
            meshMap[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            meshMap[i].transform.position = new Vector3(x, y, 0f);
                meshMap[i].name = "x:" + x + " y:" + y;
            }
        }
        
        UpdateColorMap();
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


    private void UpdateColorMap()
    {//Recolors cube primitives based on roomtype
        for(int i = 0; i<rooms.Length; i++)
        {
            Color colorOut = Color.clear;
            meshMap[i].GetComponent<MeshRenderer>().material.color = roomColorMap[rooms[i].roomType];
        }


    }

}
