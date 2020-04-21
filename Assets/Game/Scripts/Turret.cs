using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{

    private Transform target;

    public static Turret instance;
    Waypoints wayp;
    Shop shop;
    Upgrade upg;

    Ray ray;
    RaycastHit hit;
    private int isTier1 = 1;

    [Header("Upgrade Panel stuff")]
    private GameObject UpgradePanelHolder;
    public GameObject upgradePanel;
    public Vector3 offset;
    private GameObject thisPanel;

    private GameObject thisPanelTD1;
    private GameObject thisPanelTD2;
    private GameObject thisPanelTO1;
    private GameObject thisPanelTO2;

    List<GameObject> uplist = new List<GameObject>();
    private GameObject curOb;

    [Header("Waypoint stuff")]
    public GameObject endWaypoint;
    public GameObject waypoint;
    public GameObject waypointParent;
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

        waypoint = GameObject.Find("Waypoints/Waypoint");
        waypointParent = GameObject.Find("Waypoints");
        endWaypoint = GameObject.Find("EndWaypoint");
        UpgradePanelHolder = GameObject.Find("UpgradePanelHolder");

        endWaypointPos = endWaypoint.transform.position;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        endWaypoint.transform.SetParent(transform);

        Vector3 desiredWaypointPos = gameObject.transform.position + new Vector3(0, 3, 0);

        GameObject wayPoint = (GameObject)Instantiate(waypoint, desiredWaypointPos, Quaternion.identity);
        wayPoint.transform.SetParent(waypointParent.transform);
        endWaypoint.transform.SetParent(waypointParent.transform);
        wayp.CheckChild();

        Vector3 desiredUpgradePanelPos = gameObject.transform.position + offset;
        GameObject ThisupgradePanel = (GameObject)Instantiate(upgradePanel, desiredUpgradePanelPos, Quaternion.identity);

        thisPanel = ThisupgradePanel;

        thisPanelTD1 = thisPanel.transform.Find("UpgradeTurretTierOffense1").gameObject;
        thisPanelTD2 = thisPanel.transform.Find("UpgradeTurretTierOffense2").gameObject;
        thisPanelTO1 = thisPanel.transform.Find("UpgradeTurretTierDefense1").gameObject;
        thisPanelTO2 = thisPanel.transform.Find("UpgradeTurretTierDefense2").gameObject;

    }
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.tag == "TierOffense")
                {

                    thisPanelTD2.SetActive(false);
                    thisPanelTD1.SetActive(false);

                    if (isTier1 == 1)
                    {

                        if (shop.balance > 100)
                        {

                            shop.balance -= 100;
                            thisPanelTO1.SetActive(false);
                            thisPanelTO2.SetActive(true);
                            isTier1 = 2;
                            return;

                        }

                        return;

                    }

                    if (isTier1 == 2)
                    {

                        if (shop.balance > 500)
                        {
                            thisPanelTO2.SetActive(false);
                            shop.balance -= 500;
                            return;
                        }

                        return;

                    }

                }

                if (hit.transform.tag == "TierDefense")
                {

                    thisPanelTO2.SetActive(false);
                    thisPanelTO1.SetActive(false);

                }
            }

        }

        if (target == null)
            {

                return;

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
