using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{

    Turret turret;

    private bool waited;

    private GameObject touchob;

    private void Start()
    {

        turret = Turret.instance;

        EnemyMovement enmove = GetComponent<EnemyMovement>();

        enmove.enabled = true;

    }

    private void Update()
    {

        EnemyMovement enmove = GetComponent<EnemyMovement>();

        if (touchob != null && touchob.transform.tag == "Panda")
        {

            enmove.enabled = false;
            if (waited == true)
            {

                touchob.transform.gameObject.GetComponent<Turret>().health -= 10;
                waited = false;
                StartCoroutine(wait1());

            }


        }
        if (touchob == null)
        {

            enmove.enabled = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        StartCoroutine(wait1());

        touchob = other.gameObject;

    }

    IEnumerator wait1()
    {


        yield return new WaitForSeconds(5);
        waited = true;

    }
}
    
