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

    public List<Effect> afflictedList = new List<Effect>();

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
                foreach (Effect e in afflictedList)
                {
                    if (e.Length > 0)
                    {
                        e.OnEndTurn(gameObject);
                        e.Length--;
                    }

                }

                for (int i = 0; i < afflictedList.Count; i++)
                {
                    if (afflictedList[i].Length <= 0)
                    {
                        Debug.Log(afflictedList[i].Name + " has fallen off from " + gameObject.name);
                        afflictedList.Remove(afflictedList[i]);
                        
                    }
                }

                // turnManager.EndTurn();
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
        bool check = false;
        foreach (Effect e in afflictedList)
        {
            if (e.Name == effect.Name)
            {
                e.Length = effect.Length;
                Debug.Log("Refreshing dot");
                check = true;
            }
        }
        if (check == true)
        {
            
        }
        else
        {
            Debug.Log(gameObject.name + "Recieved dot: " + effect.Name);
            afflictedList.Add(new Effect(effect.Name, effect.Damage, effect.HealthRecover, effect.Length));

        }
    }
}
