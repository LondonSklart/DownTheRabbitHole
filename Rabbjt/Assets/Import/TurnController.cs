using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private bool myTurn = false;
    EnemyController enemy;
    PlayerController player;
    public float initiative;
    public float Startinghaste;
    public float haste = 0;


    private void Awake()
    {
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
