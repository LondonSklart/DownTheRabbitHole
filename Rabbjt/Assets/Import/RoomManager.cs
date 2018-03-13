using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{

    PlayerController player;
    public GameObject enemy;

    Quaternion rotation;
    TurnManager turnManager;
    EnemyController[] enemiesList;
    WorldBuilder worldBuilder;

    [SerializeField]
    Encounter testEncounter;

    public List<GameObject> allCharactersList = new List<GameObject>();

    public GameObject[] spawnPositions=new GameObject[3];


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        worldBuilder = FindObjectOfType<WorldBuilder>();
        turnManager = FindObjectOfType<TurnManager>();

        OnRoomLoad(player.CurrentRoom);


    }


    public void OnRoomLoad(int currentRoom)
    {
        allCharactersList.Clear();
        Debug.Log("Loading new room: "+currentRoom);
        Room activeRoom = worldBuilder.LoadRoom(currentRoom);

        if(activeRoom.Encounter != null) //Only runs if the room contains enemies
        {
            for(int i = 0; i < activeRoom.enemiesInRoom.Count(); i++)
            {
                Instantiate(activeRoom.enemiesInRoom[i], spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            }
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

        turnManager.NewTurn();
       // turnManager.SeedSecondTurn();
       // turnManager.EnterFight();
    }

}
