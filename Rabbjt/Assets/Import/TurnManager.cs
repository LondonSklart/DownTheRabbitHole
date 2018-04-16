using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private bool playerTurn = true;
    private float highestInitiative = 0;
    public float lowestInitiative = 100;
    private float highestHaste;
    private bool fighting = false;

    RoomManager room;
    InventoryUI inventoryUI;
    List<GameObject> enemies = new List<GameObject>();
    UI_room ui_room;
    WorldBuilder worldBuilder;
    PlayerController player;

    public List<TurnController> turnControllerList = new List<TurnController>();
    public List<GameObject> HasteEntryList = new List<GameObject>();

    private void Awake()
    {
        room = FindObjectOfType<RoomManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        ui_room = FindObjectOfType<UI_room>();
        worldBuilder = FindObjectOfType<WorldBuilder>();
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CheckIfVictorious();

        }


    }

    public void NewTurn()
    {
        CheckIfVictorious();


        SortMainListInt();

        if (room.allCharactersList[0] != null)
        {
            GetNextInQueue().GetComponent<TurnController>().SetTurn(true);
           // room.allCharactersList[0].GetComponent<TurnController>().SetTurn(true);

        }
        else
        {
            print("Cannot find entry 0 in queue");
            NewTurn();
        }

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
        CheckIfVictorious();
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
        fighting = true;
    }
    public void SeedSecondTurn()
    {
        List<GameObject> currentBattlers = new List<GameObject>();
        foreach(GameObject go in room.allCharactersList)
        {
            currentBattlers.Add(go);
        }
        room.allCharactersList.AddRange(currentBattlers);


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
    public void CheckIfVictorious()
    {
        enemies.Clear();
        foreach (GameObject g in room.allCharactersList)
        {
            if (g.GetComponent<EnemyController>() != null)
            {
                enemies.Add(g);

            }
        }

            if (fighting == true && enemies.Count < 1)
            {
            foreach (TurnController t in FindObjectsOfType<TurnController>())
            {
                t.CheckEndOfFight();
            }
                inventoryUI.VictoryUI();
                fighting = false;
            Debug.Log("Player current room" + player.CurrentRoom);
            //worldBuilder.rooms[player.CurrentRoom].Encounter.coinReward;
            worldBuilder.rooms[player.CurrentRoom].EnemiesInRoom = null;
            worldBuilder.rooms[player.CurrentRoom].Encounter = null;
            ui_room.UpdateUI();
            }
    
    }
    public bool GetFighting()
    {
        return fighting;
    }
}
