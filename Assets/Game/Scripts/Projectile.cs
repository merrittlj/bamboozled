using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Turret turret;

    Shop shop;
    NumberManager numman;

    public float thisDamage;

    public static Projectile instance;

    private GameObject hitOb;

    private Transform target;

    [Header("Attributes")]
    public float speed;

    [Header("Prefabs")]
    public GameObject ImpactEffect;

    private void Awake()
    {

        instance = this;
        shop = Shop.instance;

    }

    public void Seek(Transform _target)
    {

        target = _target;

    }

    void Start()
    {

        turret = Turret.instance;

        numman = NumberManager.instance;

        if (target != null)
        {

            Rigidbody rb = this.GetComponent<Rigidbody>();

            Vector3 DesiredVelocity = (target.transform.position - transform.position).normalized * speed;

            DesiredVelocity.y = 0;

            rb.velocity = DesiredVelocity;

        }

        if (target == null)
        {

            Rigidbody rb = this.GetComponent<Rigidbody>();

            Vector3 DesiredVelocity = (Input.mousePosition - transform.position).normalized * speed;

            DesiredVelocity.y = 0;

            rb.AddRelativeForce(Vector3.forward * 10000);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "BigEnemy")
        {
            hitOb = other.gameObject;
            HitTarget();
            return;

        }

        if (other.gameObject.tag == "ExplosiveEnemy")
        {

            other.gameObject.GetComponent<Enemy>().destur.Explode();

        }

    }

    void HitTarget()
    {

        GameObject effectIns = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);

        shop.balance += 1;

        Destroy(effectIns, 2f);
        Destroy(gameObject);
        hitOb.GetComponent<Enemy>().TakeDamage(thisDamage);

    }

    IEnumerator WaitToDelete()
    {

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

    }

}
