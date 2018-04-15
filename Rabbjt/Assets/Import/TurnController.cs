using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    private bool myTurn = false;
    EnemyController enemy;
    PlayerController player;
    CharacterStats Chstats;
    GameObject uiParent;
    public float initiative;
    public float Startinghaste;
    public float haste = 0;
    private bool fighting = false;

    private void Awake()
    {
        uiParent = GameObject.FindGameObjectWithTag("UI");
        if (initiative <= 0)
        {
            initiative = Random.Range(16,420);
        }
        if (Startinghaste <= 0)
        {
            Startinghaste = 4;//Random.Range(1,100);
        }
       // haste = Startinghaste;
    }


    public void SetTurn(bool turn)
    {
        myTurn = turn;
        if (turn == true)
        {
            if (gameObject.GetComponent<PlayerController>() != null)
            {
;                GameObject.FindGameObjectWithTag("TurnText").GetComponent<Text>().color = Color.green;
            }
            else
            {
                GameObject.FindGameObjectWithTag("TurnText").GetComponent<Text>().color = Color.red;
            }
            GameObject.FindGameObjectWithTag("TurnText").GetComponent<Text>().text = gameObject.name + "'s Turn";

        }

        if (FindObjectsOfType<TurnController>().Length <= 1)
        {
            GameObject.FindGameObjectWithTag("TurnText").GetComponent<Text>().text = "";

        }
    }
    public void CheckEndOfFight()
    {
        if (FindObjectsOfType<TurnController>().Length <= 1)
        {
            GameObject.FindGameObjectWithTag("TurnText").GetComponent<Text>().text = "";

        }
    }

    public bool GetTurn()
    {
        return myTurn;
    }
    public float GetHaste()
    {
        return haste;
    }
    public void IncreaseHaste()
    {
        haste--;
    }
    public void ResetHaste()
    {
        haste = Startinghaste;
    }
}
