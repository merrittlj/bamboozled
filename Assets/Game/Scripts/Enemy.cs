using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public DestroyTurret destur;

    EnemyMovement enmove;

    public float health;

    private void Start()
    {

        enmove = EnemyMovement.instance;
        destur = DestroyTurret.instance;

    }

    private void Update()
    {
        
        if (health <= 1)
        {

           destur.Explode();

        }

    }

    public void TakeDamage(float amount)
    {

        if (gameObject.tag != "ExplosiveEnemy")
        {

            health -= amount;

            enmove.speed = health / 10;

        }

    }

}
