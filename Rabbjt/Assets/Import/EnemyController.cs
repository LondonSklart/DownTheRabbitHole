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
    public float armor;
    public float fragile;

    public Image healthbar;

    public GameObject IconParent;

    TurnManager turnManager;
    RoomManager roomManager;
    DamagePrint damagePrint;
    TurnController turnController;

    List<GameObject> templist = new List<GameObject>();
    public List<Effect> afflictedList = new List<Effect>();

    private void Awake()
    {
        startingHealth = Random.Range(20, 50);
        attackDamage = Random.Range(1, 5);
        initiative = Random.Range(1, 3);
        armor = 2;
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

                float damageCache = 0;

                foreach (Effect e in afflictedList)
                {
                    if (e.Length > 0)
                    {

                        damageCache +=e.OnEndTurn();

                        e.Length--;
                    }

                }
                if (afflictedList.Count > 0 && damageCache >0)
                {
                    TakeDamage(damageCache,false);
                }

                for (int i = 0; i < afflictedList.Count; i++)
                {
                    if (afflictedList[i].Length <= 0)
                    {
                        Debug.Log(afflictedList[i].Name + " has fallen off from " + gameObject.name);

                        afflictedList[i].OnFallOff(gameObject);

                        for (int j = 0; j < templist.Count; j++)
                        {
                            if (afflictedList[i].Icon.GetComponent<Image>().sprite.name == templist[j].GetComponent<Image>().sprite.name)
                            {
                                Destroy(templist[j].gameObject);
                                templist.Remove(templist[j]);
                            }
                        }


                        afflictedList.Remove(afflictedList[i]);


                    }
                }

                // turnManager.EndTurn();
            }

        }

	}
    public void TakeDamage(float damage,bool affectedByArmor)
    {
        if (affectedByArmor == true)
        {
            damage /= armor;

            Debug.Log("Damage affected by armor " + damage);
            if (damage % 2 != 0)
            {
                damage = Mathf.Round(damage);
                Debug.Log("Is rounded to " + damage);
            }
        }
        damagePrint.PrintDamage(damage.ToString());
        health -= (damage + fragile);
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
            afflictedList.Add(new Effect(effect.Name, effect.Damage, effect.HealthRecover, effect.Length, effect.Icon,effect.ArmorShred,effect.FragileLevel));
            Debug.Log("Armor before shred" + armor);
            effect.OnApplied(gameObject);
            Debug.Log("Armor after shred" + armor);

            templist.Add(Instantiate(effect.Icon, IconParent.transform));
            StartCoroutine(TimedPopUp(0.5f, afflictedList));

        }








    }




        IEnumerator TimedPopUp(float time, List<Effect> dotList)
    {


        for (int i = 0; i < dotList.Count; i++)
        {
            yield return new WaitForSeconds(time);
            damagePrint.PrintDamage(dotList[i].Name);
        }



    }
}
