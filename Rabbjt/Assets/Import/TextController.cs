using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Animator animator;
    //private Text damageText;

    private void Awake()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //damageText = animator.GetComponent<Text>();
        Destroy(gameObject, clipInfo[0].clip.length);


    }
    public void SetText(string damage)
    {
        animator.GetComponent<Text>().text = damage.ToString();
    }

}
