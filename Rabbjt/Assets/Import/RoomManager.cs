using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{

    PlayerController player;
    public GameObject enemy;

    int counter =0;

    Quaternion rotation;
    TurnManager turnManager;
    EnemyController[] enemiesList;
    WorldBuilder worldBuilder;


    public List<GameObject> allCharactersList = new List<GameObject>();

    public GameObject[] spawnPositions=new GameObject[3];
    UI_room uiRoom;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        worldBuilder = FindObjectOfType<WorldBuilder>();
        turnManager = FindObjectOfType<TurnManager>();
        uiRoom = FindObjectOfType<UI_room>();
        OnRoomLoad(player.CurrentRoom);

    }

    public void OnRoomLoad(int currentRoom)
    {
        allCharactersList.Clear();
        Debug.Log("Loading new room: "+currentRoom);
        Room activeRoom = worldBuilder.LoadRoom(currentRoom);
        if (activeRoom.roomType == Room.RoomType.Shop)
        {

        }

        if(activeRoom.Encounter != null) //Only runs if the room contains enemies
        {
            for(int i = 0; i < activeRoom.enemiesInRoom.Count(); i++)
            {
                if (activeRoom.enemiesInRoom[i] != null) Instantiate(activeRoom.enemiesInRoom[i], spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            }
            turnManager.SetCoinReward(activeRoom.Encounter.coinReward);

            activeRoom.Encounter = null;

        }

        enemiesList = FindObjectsOfType<EnemyController>();
        if (enemiesList.Length > 0)
        {
            turnManager.EnterFight();
        }
        
        GameObject[] allCharacters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject c in allCharacters)
        {
            allCharactersList.Add(c);
        }
        if (allCharactersList.Count > 0)
        {
            allCharactersList.Sort(delegate (GameObject a, GameObject b) {

                return (a.GetComponent<TurnController>().initiative).CompareTo(b.GetComponent<TurnController>().initiative);
            });
        }

        if (worldBuilder.rooms[player.CurrentRoom].Encounter == null)
        {
            counter++;
            Debug.Log("Setting coin reward " + counter);

        }
        uiRoom.UpdateUI();
        turnManager.NewTurn();
       // turnManager.SeedSecondTurn();
       // turnManager.EnterFight();
    }

}
