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
    GameOverText gameovertext;
    DestroyTurret destur;

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
    public int isclick;

    public Vector3 offset;

    public float damage;

    [Header("Unity Setup Fields")]
    string[] enemyTag;
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    [Header("Upgrade Stuff")]
    public GameObject upgradeBar;
    public GameObject testupgradebar;
    public GameObject upgradepos;

    public GameObject rangecircle;

    private bool waitsworb;

    private float swingtime = 1;

    [HideInInspector]
    public GameObject thisUpGBAR;

    [Header("Health Bar Stuff")]
    public float health;
    public float startHealth;

    private GameObject thiswayp;

    [Header("Enemy Stuff")]

    GameObject[] Enemies;

    private float shortestDistance;
    private GameObject nnearestEnemy;
    private float distanceToEnemy;

    private void Awake()
    {

        instance = this;

        wayp = Waypoints.instance;
        shop = Shop.instance;
        upg = Upgrade.instance;
        gameovertext = GameOverText.instance;

        destur = DestroyTurret.instance;

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

        upgradepos = GameObject.Find("Scene/UpgradeBarpos");

        //Vector3 desiredPosTurUp = gameObject.transform.position + offset;
        Vector3 desiredPosTurUp = upgradepos.transform.position;
        thisUpGBAR = (GameObject)Instantiate(testupgradebar, desiredPosTurUp, testupgradebar.transform.rotation);
        thisUpGBAR.SetActive(false);

        to1 = thisUpGBAR.transform.Find("UpgradeTurretTierOffense1").gameObject;
        to2 = thisUpGBAR.transform.Find("UpgradeTurretTierOffense2").gameObject;
        td1 = thisUpGBAR.transform.Find("UpgradeTurretTierDefense1").gameObject;
        td2 = thisUpGBAR.transform.Find("UpgradeTurretTierDefense2").gameObject;

        Vector3 desiredPosHealthBar = gameObject.transform.position + new Vector3(3, 5, 0);

        thisHealthBar = (Image)Instantiate(healthBar, desiredPosHealthBar, desiredRotOb.transform.rotation);
        thisHealthBar.transform.SetParent(healthBarHolder.transform);

        startHealth = health;

    }
    void Update()
    {

        if (gameObject.tag == "Farm" || gameObject.tag == "Robot" || gameObject.tag == "Medic")
        {

            rangecircle = null;

        }

        if (rangecircle != null)
        {

            rangecircle.transform.localScale = new Vector3(range / 1.5f, range / 1.5f, range / 1.5f);

        }

        if (startHealth != health)
        {


            if (health > startHealth)
            {

                startHealth = health;

            }

        }

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
                    if (hit.transform.gameObject != gameObject && hit.transform.tag != "UpgradeBar" && hit.transform.tag != "TierOffense" && hit.transform.tag != "TierDefense")
                    {

                        thisUpGBAR.SetActive(false);
                        if (rangecircle != null)
                        {

                            rangecircle.SetActive(false);

                        }

                    }
                    if (hit.transform.gameObject == to1 || hit.transform.gameObject == to2)
                    {

                        if (gameObject.tag == "Panda")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance >= 500)
                                {

                                    range -= 3;
                                    fireRate += 1;
                                    damage += 10;
                                    Vector3 lTemp = PartToRotate.localScale;
                                    lTemp.x *= 1.5f;
                                    lTemp.z *= 1.5f;
                                    PartToRotate.localScale = lTemp;
                                    shop.balance -= 500;
                                    to1.SetActive(false);
                                    to2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 2000)
                                {
                                    range -= 5f;
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

                                if (shop.balance >= 700)
                                {

                                    gameObject.GetComponent<Medic>().power += 30;
                                    health -= 15;
                                    startHealth = health;
                                    shop.balance -= 700;
                                    to1.SetActive(false);
                                    to2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 4000)
                                {
                                    gameObject.GetComponent<Medic>().power += 80;
                                    health -= 25;
                                    startHealth = health;
                                    to2.SetActive(false);
                                    shop.balance -= 2500;
                                    return;
                                }

                            }

                        }

                        if (gameObject.tag == "Farm")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance >= 500)
                                {

                                    gameObject.GetComponent<Farm>().amount += 7;
                                    health -= 75;
                                    startHealth = health;
                                    shop.balance -= 500;
                                    to1.SetActive(false);
                                    to2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 2000)
                                {

                                    gameObject.GetComponent<Farm>().amount += 20;
                                    health -= 100;
                                    shop.balance -= 2000;
                                    startHealth = health;
                                    to2.SetActive(false);
                                    return;

                                }

                            }

                        }
                        if (gameObject.tag == "Knight")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance >= 750)
                                {

                                    swingtime = 0.5f;
                                    health -= 100;
                                    startHealth = health;
                                    shop.balance -= 750;
                                    to1.SetActive(false);
                                    to2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 2000)
                                {

                                    swingtime = 0.25f;
                                    health -= 200;
                                    startHealth = health;
                                    to2.SetActive(false);
                                    shop.balance -= 2000;
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

                                if (shop.balance >= 500)
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

                                if (shop.balance >= 2000)
                                {

                                    health += 75;
                                    damage -= 15;
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

                                if (shop.balance >= 800)
                                {

                                    health += 50;
                                    gameObject.GetComponent<Medic>().power -= 3;
                                    shop.balance -= 800;
                                    td1.SetActive(false);
                                    td2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 2000)
                                {

                                    health += 100;
                                    gameObject.GetComponent<Medic>().power -= 4;
                                    td2.SetActive(false);
                                    shop.balance -= 2000;
                                    return;
                                }

                            }

                        }

                        if (gameObject.tag == "Farm")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance >= 750)
                                {

                                    health += 100;
                                    gameObject.GetComponent<Farm>().amount -= 5;
                                    shop.balance -= 750;
                                    td1.SetActive(false);
                                    td2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 2000)
                                {

                                    health += 250;
                                    gameObject.GetComponent<Farm>().amount -= 7;
                                    td2.SetActive(false);
                                    shop.balance -= 2000;
                                    return;
                                }

                            }

                        }

                        if (gameObject.tag == "Knight")
                        {
                            if (isTier1 == 1)
                            {

                                if (shop.balance >= 750)
                                {

                                    swingtime = 1.5f;
                                    gameObject.GetComponent<Knight>().sword.transform.Find("pixil-frame-0 (2)").gameObject.GetComponent<Swordd>().amount += 20;
                                    shop.balance -= 750;
                                    td1.SetActive(false);
                                    td2.SetActive(true);
                                    isTier1 = 2;
                                    return;

                                }

                            }
                            if (isTier1 == 2)
                            {

                                if (shop.balance >= 2500)
                                {

                                    swingtime = 2.5f;
                                    gameObject.GetComponent<Knight>().sword.transform.Find("pixil-frame-0 (2)").gameObject.GetComponent<Swordd>().amount += 50;
                                    td2.SetActive(false);
                                    shop.balance -= 2500;
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
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f && target != null)
        {

            Shoot();
            fireCountdown = 1f / fireRate;

        }

        fireCountdown -= Time.deltaTime;

        if (waitsworb == true)
        {

            StartCoroutine(waitsword());

        }

    }

    public void UpdateTarget()
    {

        shortestDistance = Mathf.Infinity;
        nnearestEnemy = null;

        string[] EnemyTags = { "Enemy", "ExplosiveEnemy", "BigEnemy" };
        foreach (string tag in EnemyTags)
        {

            Enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in Enemies)
            {

                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {

                    shortestDistance = distanceToEnemy;
                    nnearestEnemy = enemy;

                }

            }

        }

        if (nnearestEnemy != null && shortestDistance <= range)
        {

            target = nnearestEnemy.transform;

        }

    }

    public void Shoot()
    {

        if (this.gameObject.tag == "Panda")
        {

            GameObject bulletGO = (GameObject)Instantiate(shop.panda.projectile, firePoint.position, firePoint.rotation);
            Projectile bullet = bulletGO.GetComponent<Projectile>();

            if (bullet != null)
            {

                bullet.Seek(target);
                bullet.thisDamage = damage;

            }

        }

        if (gameObject.tag == "Knight")
        {

            gameObject.GetComponent<Knight>().sword.gameObject.GetComponent<Animator>().Play("swordswing1");

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
        if (rangecircle != null)
        {

            rangecircle.SetActive(true);

        }

        isclick = 1;

    }

    public void TakeDamage(float amount)
    {

        health -= amount;

        if (health <= 0)
        {

            gameovertext.pandasdied += 1;

            Destroy(thisUpGBAR);
            Destroy(thiswayp);
            Destroy(thisHealthBar.gameObject);
            Destroy(gameObject);


        }

    }

    IEnumerator waitsword()
    {

        waitsworb = false;
        yield return new WaitForSeconds(swingtime);
        waitsworb = true;

    }

}
