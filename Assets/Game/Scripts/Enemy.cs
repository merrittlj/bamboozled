using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    EnemyMovement enmove;

    public float health;

    private void Start()
    {

        enmove = EnemyMovement.instance;

    }

    private void Update()
    {
        
        if (health <= 1)
        {

            Destroy(gameObject);

        }

    }

    public void TakeDamage(float amount)
    {

        health -= amount;

        enmove.speed = health / 10;

    }
}
