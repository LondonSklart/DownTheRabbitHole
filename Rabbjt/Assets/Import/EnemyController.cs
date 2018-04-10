using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public Sprite uiIcon;


    public float startingHealth;
    private float health;
    public int attackDamage;
    private float swingTimer = 1;
    public float initiative;
    public float startingHaste;
    public float haste;

    public Image healthbar;
    TurnManager turnManager;
    RoomManager roomManager;
    DamagePrint damagePrint;
    TurnController turnController;

    private List<Effect> afflictedList = new List<Effect>();

    private void Awake()
    {
        startingHealth = Random.Range(5, 10);
        attackDamage = Random.Range(1, 5);
        initiative = Random.Range(1, 3);
        if (startingHaste <= 0)
        {
            startingHaste = 2;//Random.Range(1, 10);
            haste = startingHaste;
        }

        health = startingHealth;
        damagePrint = GetComponentInChildren<DamagePrint>();
        turnController = gameObject.GetComponent<TurnController>();
        turnManager = FindObjectOfType<TurnManager>();
        roomManager = FindObjectOfType<RoomManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (afflictedList.Count > 0)
        {

        }

        if (turnController.GetTurn())
        {

            swingTimer -= Time.deltaTime;
            if (swingTimer <= 0)
            {

                Attack(attackDamage);
                swingTimer = 1;
                turnController.ResetHaste();
                turnController.SetTurn(false);
                turnManager.NewTurn();
                turnManager.DecreaseHaste();

                for (int i = 0; i < afflictedList.Count; i++)
                {
                    if (afflictedList[i].Length <= 0)
                        afflictedList.Remove(afflictedList[i]);
                    else
                    {
                        afflictedList[i].OnEndTurn(gameObject);

                    }

                }

            }

        }

	}
    public void TakeDamage(float damage)
    {
        damagePrint.PrintDamage(damage);
        health -= damage;
        healthbar.fillAmount = health / startingHealth;
        if (health <= 0)
        {
           turnManager.Death(gameObject);
            Destroy(gameObject);
        }
    }
    public void Attack(int damage)
    {
        PlayerController player;
        player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
        player.TakeDamage(damage);
        }


    }
    public void Afflicted(Effect effect)
    {
        afflictedList.Add(effect);
    }
}
