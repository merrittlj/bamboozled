using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health = 100;

    private void Update()
    {
        
        if (health <= 0)
        {

            Destroy(gameObject);
            Debug.Log("GAME OVER");

        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {

            health -= 1;
            Debug.Log("Health Left: " + health);

        }

    }

}
