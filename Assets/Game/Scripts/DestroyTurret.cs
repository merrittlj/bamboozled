using System.Collections;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{

    public static DestroyTurret instance;

    Turret turret;

    Enemy enemy;

    GameOverText gameovertext;

    public bool waited;

    private GameObject touchob;

    public float explosionPower;

    public GameObject[] nearbyTowers;

    public GameObject explosionParticles;

    public float damage;

    private bool waitsworb;

    private void Awake()
    {

        instance = this;

    }

    private void Start()
    {

        turret = Turret.instance;
        gameovertext = GameOverText.instance;
        enemy = Enemy.instance;

        EnemyMovement enmove = GetComponent<EnemyMovement>();

        enmove.enabled = true;

        waited = false;

        waitsworb = true;

    }

    private void Update()
    {

        EnemyMovement enmove = GetComponent<EnemyMovement>();
        if (waited == true)
        {

            if (touchob != null)
            {
                if (touchob.transform.tag == "Panda" || touchob.transform.tag == "Medic" || touchob.transform.tag == "Robot" || touchob.transform.tag == "Farm" || touchob.transform.tag == "Knight")
                {

                    enmove.enabled = false;
                    StartCoroutine(wait1());
                    if (touchob.gameObject.tag == "Robot")
                    {

                        touchob.gameObject.GetComponent<Robot>().health = 0;

                    }

                    else
                    {

                        touchob.transform.gameObject.GetComponent<Turret>().TakeDamage(10);
                        waited = false;

                    }

                }


            }

        }

        if (touchob != null)
        {

            enemy.iseat = true;

            if (gameObject.tag == "ExplosiveEnemy")
            {

                int howmany = 0;

                if (touchob.gameObject != null)
                {

                    Explode();

                    if (touchob.gameObject == null)
                    {

                        return;

                    }

                }

            }

        }

        if (touchob == null)
        {

            enemy.iseat = false;

            enmove.enabled = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Panda" || other.gameObject.tag == "Medic" || other.gameObject.tag == "Robot" || other.gameObject.tag == "Farm" || other.gameObject.tag == "Knight")
        {

            StartCoroutine(wait1());

            touchob = other.gameObject;

        }

        if (gameObject.tag == "ExplosiveEnemy" && other.gameObject.tag == "Bullet")
        {

            Explode();

        } 

    }

    IEnumerator wait1()
    {

        EnemyMovement enmove = GetComponent<EnemyMovement>();
        enmove.enabled = false;
        yield return new WaitForSeconds(3);
        waited = true;

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, explosionPower);

    }

    public void Explode()
    {

        int howmany = 0;

        gameovertext.zombiesdied += 1;

        if (howmany == 0)
        {

            GameObject explosionEffect = (GameObject)Instantiate(explosionParticles, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(explosionEffect, 7f);
            howmany++;

        }
        if (touchob != null && touchob.gameObject.GetComponent<Turret>() != null)
        {

            touchob.gameObject.GetComponent<Turret>().TakeDamage(damage);

        }
        string[] tagsToDisable = { "Panda", "Robot", "Medic", "Farm", "Knight"};
        foreach (string tag in tagsToDisable)
        {
            nearbyTowers = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject i in nearbyTowers)
            {
                float dis = Vector3.Distance(gameObject.transform.position, i.gameObject.transform.position);
                if (dis <= explosionPower)
                {

                    i.gameObject.GetComponent<Turret>().TakeDamage(20);

                }

            }

        }
        Destroy(gameObject);

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "sword" && waitsworb == true)
        {

            print("hi");
            gameObject.GetComponent<Enemy>().TakeDamage(other.gameObject.GetComponent<Swordd>().amount);
            StartCoroutine(waitsword());

        }

    }

    IEnumerator waitsword()
    {

        waitsworb = false;
        yield return new WaitForSeconds(1);
        waitsworb = true;

    }

}
    
