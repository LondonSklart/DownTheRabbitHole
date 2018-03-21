using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiRoomData : MonoBehaviour {

    
    public GameObject[] monsterIcons;
    
    public GameObject[] treasureIcons;

    public Sprite GetIcon(int index)
    {
        return monsterIcons[index].GetComponent<Image>().sprite;
    }

    public void SetIcon(int index, Sprite sprite)
    {
        monsterIcons[index].GetComponent<Image>().sprite = sprite;
    }

}
