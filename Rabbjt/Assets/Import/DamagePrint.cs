using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePrint : MonoBehaviour
{
    public TextController text;

    public void PrintDamage(float damage)
    {
        TextController texten = Instantiate(text,gameObject.transform);
        texten.SetText(damage);
    }
}
