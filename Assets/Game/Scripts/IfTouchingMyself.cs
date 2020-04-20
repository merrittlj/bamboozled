using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTouchingMyself : MonoBehaviour
{

    public bool touch = false;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Panda")
        {

            touch = true;

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        touch = false;

    }

}
