using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBelt : MonoBehaviour
{

    PlayerController player;
    public GameObject strIcon;
    public GameObject hstIcon;
    public Text amount1;
    public Text amount2;
    public Text amount3;

    public List<Effect> afflictedList = new List<Effect>();

    private int strengthPotions = 3;
    private int healthPotions = 3;
    private int hastePotions = 3;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        amount1.text = strengthPotions.ToString();
        amount2.text = healthPotions.ToString();
        amount3.text = hastePotions.ToString();
    }



    public void DrinkHealthPot()
    {
        if (healthPotions > 0)
        {
            float temp;

            temp = player.startingHealth - player.health;
            temp /= 2;

            if (temp % 2 != 0)
            {
                temp = Mathf.Round(temp);
            }
            player.GainHealth(temp);
            healthPotions--;
            amount2.text = healthPotions.ToString();
        }
    }
    public void DrinkStrengthPotion()
    {
        if (strengthPotions > 0)
        {
            player.Afflicted(new Effect("Strength", 0, 0, 2, strIcon, 0, 0));
            player.bonusDamage += 10;
            strengthPotions--;
            amount1.text = strengthPotions.ToString();
        }

    }
    public void DrinkHastePotion()
    {
        if (hastePotions > 0)
        {
            player.Afflicted(new Effect("Haste",0,0,2,hstIcon,0,0));
            player.GetComponent<TurnController>().Startinghaste -= 2;
            player.GetComponent<TurnController>().haste -= 2;
            hastePotions--;
            amount3.text = hastePotions.ToString();

        }
    }




}
