using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{

    private Animator anim;

    public bool isdone;

    private void Start()
    {

        anim = GetComponent<Animator>();

        isdone = true;

    }

    private void OnMouseDown()
    {

        if (isdone == true)
        {

            StartCoroutine(wait1());
            anim.Play("ButtonPush");
            isdone = false;
            StartCoroutine(wait1());

        }

    }

    IEnumerator wait1()
    {

        yield return new WaitForSeconds(1);
        isdone = true;

    }

}
