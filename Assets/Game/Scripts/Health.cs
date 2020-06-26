using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public static Health instance;

    GameOverText gameovertext;

    public float health = 100;

    public float startHealth;

    private void Awake()
    {

        instance = this;

        startHealth = health;

        gameovertext = GameOverText.instance;

    }

    private void Update()
    {
        
        if (health <= 0)
        {

            Destroy(gameObject);
            Debug.Log("GAME OVER");
            gameovertext.isdone = true;

        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {

            health -= 1;
            Debug.Log("Health Left: " + health);

        }
        if (collision.gameObject.tag == "ExplosiveEnemy")
        {

            health -= 2;
            Debug.Log("Health Left: " + health);

        }
        if (collision.gameObject.tag == "BigEnemy")
        {

            health -= 5;
            Debug.Log("Health Left: " + health);

        }

    }

}
