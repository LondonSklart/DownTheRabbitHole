using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{

    public GameObject enemy;
    Quaternion rotation;
    TurnManager turnManager;
    EnemyController[] enemiesList;

    [SerializeField]
    Encounter testEncounter;

    public List<GameObject> allCharactersList = new List<GameObject>();

    public GameObject[] spawnPositions=new GameObject[3];


    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        OnRoomLoad(testEncounter);


    }


    public void OnRoomLoad(Encounter encounter)
    {
        for(int i = 0; i < encounter.enemiesInRoom.Count(); i++)
        {
            Instantiate(encounter.enemiesInRoom[i], spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
        }
        //switch (enemies)
        //{
        //    case 1:
        //        Instantiate(enemy, spawnPositions[0].transform.position, spawnPositions[0].transform.rotation);
        //        break;
        //    case 2:
        //        Instantiate(enemy, spawnPositions[0].transform.position, spawnPositions[0].transform.rotation);
        //        Instantiate(enemy, spawnPositions[1].transform.position, spawnPositions[1].transform.rotation);
        //        break;
        //    case 3:
        //        Instantiate(enemy, spawnPositions[0].transform.position, spawnPositions[0].transform.rotation).name = "E1";
        //        Instantiate(enemy, spawnPositions[1].transform.position, spawnPositions[1].transform.rotation).name = "E2";
        //        Instantiate(enemy, spawnPositions[2].transform.position, spawnPositions[2].transform.rotation).name = "E3";
        //        break;
        //    case 4:
        //        Instantiate(enemy, spawnPositions[0].transform.position, spawnPositions[0].transform.rotation);
        //        Instantiate(enemy, spawnPositions[1].transform.position, spawnPositions[1].transform.rotation);
        //        Instantiate(enemy, spawnPositions[2].transform.position, spawnPositions[2].transform.rotation);
        //        Instantiate(enemy, spawnPositions[3].transform.position, spawnPositions[3].transform.rotation);
        //        break;
        //}
        enemiesList = FindObjectsOfType<EnemyController>();
        foreach (EnemyController e in enemiesList)
        {
            foreach (EnemyController ee in enemiesList)
            {
                if (e.initiative == ee.initiative)
                {
                    e.initiative -= 0.1f;
                    ee.initiative += 0.1f;
                }
            }
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
        Debug.Log("dab");
       // turnManager.EnterFight();
    }

}
