using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{

    Turret turret;

    Shop shop;

    public static BulletTest instance;

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
        if (other.gameObject.tag == "Enemy")
        {
            hitOb = other.gameObject;
            HitTarget();
            return;

        }

    }

    void HitTarget()
    {

        GameObject effectIns = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);

        shop.balance++;

        Destroy(effectIns, 2f);
        Destroy(this.gameObject);
        Destroy(hitOb);

    }

    IEnumerator WaitToDelete()
    {

        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }

}
