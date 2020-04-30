using System.Collections;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{

    Turret turret;

    public bool waited;

    private GameObject touchob;

    private void Start()
    {

        turret = Turret.instance;

        EnemyMovement enmove = GetComponent<EnemyMovement>();

        enmove.enabled = true;

        waited = false;

    }

    private void Update()
    {

        EnemyMovement enmove = GetComponent<EnemyMovement>();
        if (waited == true)
        {

            if (touchob != null)
            {
                if (touchob.transform.tag == "Panda" || touchob.transform.tag == "Super Panda" || touchob.transform.tag == "Robot")
                {

                    enmove.enabled = false;
                    StartCoroutine(wait1());
                    touchob.transform.gameObject.GetComponent<Turret>().health -= 10;
                    waited = false;

                }


            }

        }
        if (touchob == null)
        {

            enmove.enabled = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Panda" || other.gameObject.tag == "Super Panda" || other.gameObject.tag == "Robot")
        {

            StartCoroutine(wait1());

            touchob = other.gameObject;

        }

    }

    IEnumerator wait1()
    {

        EnemyMovement enmove = GetComponent<EnemyMovement>();
        enmove.enabled = false;
        yield return new WaitForSeconds(1);
        waited = true;

    }
}
    
