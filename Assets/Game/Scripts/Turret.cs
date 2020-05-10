using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    private Transform target;

    public static Turret instance;
    Waypoints wayp;
    Shop shop;
    Upgrade upg;

    [Header("Health Bar Stuff")]
    public Image healthBar;
    public Image thisHealthBar;
    private int calculateHealth;
    private GameObject healthBarHolder;
    private GameObject desiredRotOb;
    private Image thisHealthBarFill;
    private Vector3 desiredPosHealthBar;

    [Header("Waypoint stuff")]
    public GameObject endWaypoint;
    public GameObject waypoint;
    public GameObject waypointParent;
    public GameObject thisOb;
    private Vector3 endWaypointPos;

    private int isTier1 = 1;
    private GameObject td1;
    private GameObject td2;
    private GameObject to1;
    private GameObject to2;
    Ray ray;
    RaycastHit hit;


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

    
    public GameObject upgradeBar;

    [HideInInspector]
    public GameObject thisUpGBAR;

    public int isclick;

    public Vector3 offset;

    public float health;
    public float startHealth;

    public float damage;

    private GameObject thiswayp;

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
        healthBarHolder = GameObject.Find("Canvases/HealthBarCanvas");
        desiredRotOb = GameObject.Find("DesiredRotHealthBarOb");

        endWaypointPos = endWaypoint.transform.position;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        endWaypoint.transform.SetParent(transform);

        Vector3 desiredPos = thisOb.gameObject.transform.position + new Vector3(0, 3, 0);

        GameObject wayPoint1 = (GameObject)Instantiate(waypoint, desiredPos, thisOb.gameObject.transform.rotation);
        wayPoint1.transform.SetParent(waypointParent.transform);
        endWaypoint.transform.SetParent(waypointParent.transform);
        thiswayp = wayPoint1;
        wayp.CheckChild();

        Vector3 desiredPosTurUp = gameObject.transform.position + offset;
        thisUpGBAR = (GameObject)Instantiate(upgradeBar, desiredPosTurUp, upgradeBar.transform.rotation);
        thisUpGBAR.SetActive(false);

        to1 = thisUpGBAR.transform.Find("UpgradeTurretTierOffense1").gameObject;
        to2 = thisUpGBAR.transform.Find("UpgradeTurretTierOffense2").gameObject;
        td1 = thisUpGBAR.transform.Find("UpgradeTurretTierDefense1").gameObject;
        td2 = thisUpGBAR.transform.Find("UpgradeTurretTierDefense2").gameObject;

        if (gameObject.tag == "Panda")
        {

            Vector3 desiredPosHealthBar = gameObject.transform.position + new Vector3(3, 5, 0);

            thisHealthBar = (Image)Instantiate(healthBar, desiredPosHealthBar, desiredRotOb.transform.rotation);
            thisHealthBar.transform.SetParent(healthBarHolder.transform);

            startHealth = health;

        }
        if (gameObject.tag == "Medic")
        {

            Vector3 desiredPosHealthBar = gameObject.transform.position + new Vector3(3, 10, 0);

            thisHealthBar = (Image)Instantiate(healthBar, desiredPosHealthBar, desiredRotOb.transform.rotation);
            thisHealthBar.transform.SetParent(healthBarHolder.transform);

            startHealth = health;

        }

    }
    void Update()
    {

        if (isTier1 == 1)
        {

            to2.SetActive(false);
            td2.SetActive(false);

        }
        if (isTier1 == 2)
        {

            to1.SetActive(false);
            td1.SetActive(false);

        }

        thisHealthBar.transform.Find("HealthBar").GetComponent<Image>().fillAmount = health / startHealth;

        if (isclick == 1)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                if (Input.GetMouseButtonDown(0))
                {

                    if (hit.transform.gameObject == to1 || hit.transform.gameObject == to2)
                    {

                            if (gameObject.tag == "Panda")
                            {
                                if (isTier1 == 1)
                                {

                                    if (shop.balance > 500)
                                    {

                                        range -= 3;
                                        fireRate += 1;
                                        damage += 10;
                                        Vector3 lTemp = transform.localScale;
                                        lTemp.x *= 1.5f;
                                        lTemp.z *= 1.5f;
                                        transform.localScale = lTemp;
                                        shop.balance -= 500;
                                        to1.SetActive(false);
                                        to2.SetActive(true);
                                        isTier1 = 2;
                                        return;

                                    }

                            }
                                if (isTier1 == 2)
                                {

                                    if (shop.balance > 2000)
                                    {
                                        range -= 6.5f;
                                        fireRate += 2;
                                        damage += 7;
                                        to2.SetActive(false);
                                        shop.balance -= 2000;
                                        return;
                                    }

                                }

                        }

                        if (gameObject.tag == "Medic")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance > 1000)
                                {

                                    shop.balance -= 1000;
                                    to1.SetActive(false);
                                    to2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance > 5000)
                                {
                                    to2.SetActive(false);
                                    shop.balance -= 5000;
                                    return;
                                }

                            }

                        }

                    }
                    if (hit.transform.gameObject == td1 || hit.transform.gameObject == td2)
                    {

                        if (gameObject.tag == "Panda")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance > 500)
                                {

                                    health += 25;
                                    damage -= 10;
                                    range += 7.5f;
                                    shop.balance -= 500;
                                    td1.SetActive(false);
                                    td2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance > 2000)
                                {

                                    health += 75;
                                    damage -= 15;
                                    range += 20;
                                    td2.SetActive(false);
                                    shop.balance -= 2000;
                                    return;
                                }

                            }

                        }

                        if (gameObject.tag == "Medic")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance > 1000)
                                {

                                    shop.balance -= 1000;
                                    td1.SetActive(false);
                                    td2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance > 5000)
                                {
                                    td2.SetActive(false);
                                    shop.balance -= 5000;
                                    return;
                                }

                            }

                        }

                    }

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

            GameObject bulletGO = (GameObject)Instantiate(shop.panda.projectile, firePoint.position, firePoint.rotation);
            BulletTest bullet = bulletGO.GetComponent<BulletTest>();

            if (bullet != null)
            {

                bullet.Seek(target);
                bullet.thisDamage = damage;

            }

        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    private void OnMouseDown()
    {


        thisUpGBAR.SetActive(true);
        isclick = 1;

    }

    public void TakeDamage(float amount)
    {

        health -= amount;

        thisHealthBar.transform.Find("HealthBar").GetComponent<Image>().fillAmount = health / startHealth;

        if (health <= 0)
        {

            Destroy(thisUpGBAR);
            Destroy(thiswayp);
            Destroy(thisHealthBar);
            Destroy(gameObject);


        }

    }

}
