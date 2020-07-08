using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static Enemy instance;

    public DestroyTurret destur;

    Shop shop;
    GameOverText gameovertext;

    EnemyMovement enmove;

    public float health;

    Animator anim;

    public bool iseat;

    public bool waited;

    private void Awake()
    {

        instance = this;

    }

    private void Start()
    {

        enmove = EnemyMovement.instance;
        destur = DestroyTurret.instance;
        shop = Shop.instance;
        gameovertext = GameOverText.instance;

        GameObject animatorHolder = gameObject.transform.Find("zombie 1").gameObject;

        anim = animatorHolder.GetComponent<Animator>();

        waited = true;

    }

    private void Update()
    {
        if (anim != null && iseat == false && waited == true)
        {

            anim.Play("Walk");
            waited = false;
            StartCoroutine(wait1(17.458f / 10));

        }

        if (iseat == true && waited == true)
        {

            anim.Play("Eat");
            waited = false;
            StartCoroutine(wait1(13.5f / 10));

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

    IEnumerator wait1(float time)
    {

        yield return new WaitForSeconds(time);
        waited = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (gameObject.tag == "ExplosiveEnemy" && collision.gameObject.tag == "Bullet") 
        {

            destur.Explode();

        }

    }

}
