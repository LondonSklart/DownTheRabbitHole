﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public List<BaseStat> Stats { get; set; }
    public string ObjectSlug { get; set; }
    public string ItemType { get; set; }
    public bool[] AOE { get; set; }

    public Item(List <BaseStat> _Stats, string _ObjectSlug, string _ItemType, bool[] _AOE)
    {
        Stats = _Stats;
        ObjectSlug = _ObjectSlug;
        ItemType = _ItemType;
        AOE = _AOE;

    }


}

