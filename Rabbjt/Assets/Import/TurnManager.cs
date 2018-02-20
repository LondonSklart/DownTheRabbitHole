using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private bool playerTurn = true;
    private float highestInitiative = 0;
    public float lowestInitiative = 100;
    private float highestHaste;

    RoomManager room;

    public List<TurnController> turnControllerList = new List<TurnController>();
    public List<GameObject> HasteEntryList = new List<GameObject>();

    private void Awake()
    {
        room = FindObjectOfType<RoomManager>();

    }


    public void NewTurn()
    {

         // EnterFight();
       // FindHighestHaste();
        SortMainListInt();
       // findcontrollerwithhighesthaste();
       // upkeep()
        if (room.allCharactersList[0] != null)
        {
            GetNextInQueue().GetComponent<TurnController>().SetTurn(true);
           // room.allCharactersList[0].GetComponent<TurnController>().SetTurn(true);

        }
        else
        {
            print("Cannot find entry 0 in queue");
           // room.allCharactersList.RemoveAt(0);
            NewTurn();
        }


        // Debug.Log(room.allCharactersList[0].GetComponent<TurnController>().initiative);

    }

    public void EndTurn()
    {

        room.allCharactersList[0].GetComponent<TurnController>().SetTurn(false);
    
       // room.allCharactersList.Add(room.allCharactersList[0]);
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        FindObjectOfType<PlayerController>().haste--;

        foreach (EnemyController e in enemies)
        {
            e.haste--;
           // Debug.Log(e.haste);
        }
        Debug.Log(FindObjectOfType<PlayerController>().haste);
        room.allCharactersList.RemoveAt(0);
        NewTurn();
    }



    public void Death (GameObject character)
    {
        room.allCharactersList.Remove(character);

    }
    public void EnterQueue(GameObject character)
    {
        HasteEntryList.Add(character);
        if (HasteEntryList.Count > 1)
        {
            HasteEntryList.Sort(delegate (GameObject a, GameObject b) {

                return (a.GetComponent<TurnController>().initiative).CompareTo(b.GetComponent<TurnController>().initiative);
            });
        }
    }
    public void EnterFight()
    {
        Debug.Log(HasteEntryList.Count);
        for (int i = 0; i < HasteEntryList.Count; i++)
        {
            if (HasteEntryList.Count > 0)
            {
                Debug.Log("Här");

                room.allCharactersList.Add(HasteEntryList[0]);
                HasteEntryList.RemoveAt(0);

            }

        }


    }
    public void SeedSecondTurn()
    {
        List<GameObject> currentBattlers = new List<GameObject>();
        foreach(GameObject go in room.allCharactersList)
        {
            currentBattlers.Add(go);
        }
        room.allCharactersList.AddRange(currentBattlers);






















        //EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        //for (int i = 0; i < room.allCharactersList.Count; i++)
        //{
        //   // Debug.Log("Dab");
        //FindObjectOfType<PlayerController>().haste--;

        //foreach (EnemyController e in enemies)
        //{
        //    e.haste--;
        //    // Debug.Log(e.haste);
        //}
        //Debug.Log(FindObjectOfType<PlayerController>().haste);
        //}
    }

    public void FindHighestHaste()
    {
        highestHaste = 0;
        foreach (GameObject t in room.allCharactersList)
        {
            if (t.GetComponent<TurnController>().GetHaste() > highestHaste)
            {
                highestHaste = t.GetComponent<TurnController>().GetHaste();
            }
        }
    }

    public GameObject GetNextInQueue()
    {
        float lowestHaste = 100;
        List<GameObject> objectsWithLowestHaste = new List<GameObject>();
        foreach (GameObject t in room.allCharactersList)
        {
            if (lowestHaste > t.GetComponent<TurnController>().GetHaste())
            {
                lowestHaste = t.GetComponent<TurnController>().GetHaste();
            }
        }
        foreach (GameObject t in room.allCharactersList)
        {
            if (lowestHaste == t.GetComponent<TurnController>().GetHaste())
            {
                objectsWithLowestHaste.Add(t);
            }
        }
        float highestInitiative = 1000;
        foreach (GameObject t in objectsWithLowestHaste)
        {
            if (highestInitiative > t.GetComponent<TurnController>().initiative)
            {
                highestInitiative = t.GetComponent<TurnController>().initiative;
            }
        }
        foreach (GameObject t in objectsWithLowestHaste)
        {
            if (t.GetComponent<TurnController>().initiative == highestInitiative)
            {
               // Debug.Log(string.Format("Returning {0} with {1} haste", t.name, t.GetComponent<TurnController>().haste));
                return t;
            }
        }
        return null;
    }


    public void FindControllerWithHighestHaste()
    {
        foreach (GameObject t in room.allCharactersList)
        {
            if (highestHaste == t.GetComponent<TurnController>().GetHaste())
            {
                t.GetComponent<TurnController>().SetTurn(true);
            }
        }
    }
    public void DecreaseHaste()
    {
        foreach (GameObject t in room.allCharactersList)
        {
     t.GetComponent<TurnController>().IncreaseHaste();
            
        }
    }
    public void SortMainListInt()
    {

            room.allCharactersList.Sort(delegate (GameObject a, GameObject b) {
                int xdiff = a.GetComponent<TurnController>().GetHaste().CompareTo(b.GetComponent<TurnController>().GetHaste());
                if (xdiff != 0) return xdiff;
               else  return (a.GetComponent<TurnController>().initiative).CompareTo(b.GetComponent<TurnController>().initiative);
            });
        
    }
}
