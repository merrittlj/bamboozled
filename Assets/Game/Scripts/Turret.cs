using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    public static Turret instance;
    Waypoints wayp;
    Shop shop;
    Upgrade upg;

    [Header("Waypoint stuff")]
    public GameObject endWaypoint;
    public GameObject waypoint;
    public GameObject waypointParent;
    public GameObject thisOb;
    private Vector3 endWaypointPos;


    [Header("Special Firepoints")]
    public Transform robotFirepoint1;
    public Transform robotFirepoint2;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    private void Awake()
    {

        instance = this;

        wayp = Waypoints.instance;
        shop = Shop.instance;
        upg = Upgrade.instance;

    }

    void Start()
     {


        thisOb = this.gameObject;
        waypoint = GameObject.Find("Waypoints/Waypoint");
        waypointParent = GameObject.Find("Waypoints");
        endWaypoint = GameObject.Find("EndWaypoint");

        endWaypointPos = endWaypoint.transform.position;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        endWaypoint.transform.SetParent(transform);

        Vector3 desiredPos = thisOb.gameObject.transform.position + new Vector3(0, 3, 0);

        GameObject wayPoint = (GameObject)Instantiate(waypoint, desiredPos, thisOb.gameObject.transform.rotation);
        wayPoint.transform.SetParent(waypointParent.transform);
        endWaypoint.transform.SetParent(waypointParent.transform);
        wayp.CheckChild();

     }
    void Update()
    {

            if (target == null)
            {

                return;

            }

            if (upg.upgradeValue == "TO1")
            {

                range = 1000000000000000000;

            }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        if (fireCountdown <= 0f && target != null)
        {

            Shoot();
            fireCountdown = 1f / fireRate;

        }

        fireCountdown -= Time.deltaTime;

    }

    public void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {

            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {

                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {

            target = nearestEnemy.transform;

        }

        else
        {

            target = null;

        }

    }

    public void Shoot()
    {

        if (this.gameObject.tag == "Panda")
        {

            GameObject bulletGO = (GameObject)Instantiate(shop.turret.projectile, firePoint.position, firePoint.rotation);
            BulletTest bullet = bulletGO.GetComponent<BulletTest>();

            if (bullet != null)
            {

                bullet.Seek(target);

            }

        }

        if (this.gameObject.tag == "Super Panda")
        {

            GameObject bulletGO = (GameObject)Instantiate(shop.superturret.projectile, firePoint.position, firePoint.rotation);
            BulletTest bullet = bulletGO.GetComponent<BulletTest>();

            if (bullet != null)
            {

                bullet.Seek(target);

            }

        }

        if (this.gameObject.tag == "Robot")
        {

            GameObject bulletGO1 = (GameObject)Instantiate(shop.robot.projectile, robotFirepoint1.position, robotFirepoint1.rotation);
            GameObject bulletGO2 = (GameObject)Instantiate(shop.robot.projectile, robotFirepoint2.position, robotFirepoint2.rotation);
            BulletTest bullet1 = bulletGO1.GetComponent<BulletTest>();
            BulletTest bullet2 = bulletGO2.GetComponent<BulletTest>();

            if (bullet1 != null)
            {

                bullet1.Seek(target);

            }
            if (bullet2 != null)
            {

                bullet2.Seek(target);

            }

        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }

}
