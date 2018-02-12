﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{

    public string name;

    public enum RoomType
    {
        Empty,
        Monster,
        Start,
        End,
        Shop,
        HealingFountain,
    };

    bool[] avialableDoors = new bool[4];
    /*
     * 0 = N
     * 1 = E
     * 2 = S
     * 3 = W
     */

    public RoomType roomType;
    public int treasures;

    private Enemy[] enemiesInRoom;

    public Room(RoomType rt)
    {
        roomType = rt;
    }

}
