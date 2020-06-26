using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public DestroyTurret destur;

    Shop shop;
    GameOverText gameovertext;

    EnemyMovement enmove;

    public float health;

    Animator anim;

    private void Start()
    {

        enmove = EnemyMovement.instance;
        destur = DestroyTurret.instance;
        shop = Shop.instance;
        gameovertext = GameOverText.instance;

        if (gameObject.tag == "Enemy")
        {

            GameObject animatorHolder = gameObject.transform.Find("zombie").gameObject;

            anim = animatorHolder.GetComponent<Animator>();

        }

    }

    private void Update()
    {
        if (anim != null)
        {

            anim.SetTrigger("Walk");

        }

        if (health <= 1)
        {
            if (gameObject.tag == "ExplosiveEnemy")
            {

                destur.Explode();

            }

            gameovertext.zombiesdied += 1;

            Destroy(gameObject);
            
            if (gameObject.tag == "Enemy")
            {

                shop.balance += 5;

            }
            if (gameObject.tag == "ExplosiveEnemy")
            {

                shop.balance += 10;

            }
            if (gameObject.tag == "BigEnemy")
            {

                shop.balance += 20;

            }

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
