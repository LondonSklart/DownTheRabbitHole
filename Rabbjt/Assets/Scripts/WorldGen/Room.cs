using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject{

    public enum RoomType
    {
        Empty,
        Monster,
        Start,
        End,
        Shop,
        HealingFountain,
    };
    public RoomType roomType;
    public int treasures;

    public Room(RoomType rt)
    {
        roomType = rt;
    }


}
