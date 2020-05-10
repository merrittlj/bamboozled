using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public static Robot instance;

    private Transform target;

    public GameObject robotfirePoint1;
    public GameObject robotfirePoint2;

    Waypoints wayp;
    Turret turret;
    Shop shop;
    BulletTest bullet;

    [Header("Attributes")]
    public int timeLeft = 10;

    public bool activated = false;
    private bool waited = false;

    private GameObject waypoint;
    private GameObject waypointParent;
    private GameObject endWaypoint;
    private GameObject thisOb;
    public GameObject thiswaypoint;

    public int howmanytime;

    [Header("Health Bar Stuff")]
    public Image batteryBar;
    public Image thisBatteryBar;
    private int calculateHealth;
    private GameObject healthBarHolder;
    private GameObject desiredRotOb;
    private Vector3 desiredPosHealthBar;
    private float startbattery;
    public float battery;

    public float health;

    private void Awake()
    {

        instance = this;

    }

    private void Start()
    {

        turret = Turret.instance;
        wayp = Waypoints.instance;
        shop = Shop.instance;
        bullet = BulletTest.instance;

        thisOb = this.gameObject;
        waypoint = GameObject.Find("Waypoints/Waypoint");
        waypointParent = GameObject.Find("Waypoints");
        endWaypoint = GameObject.Find("EndWaypoint");
        healthBarHolder = GameObject.Find("Canvases/HealthBarCanvas");
        desiredRotOb = GameObject.Find("DesiredRotHealthBarOb");

        endWaypoint.transform.SetParent(transform);

        Vector3 desiredPos = thisOb.gameObject.transform.position + new Vector3(0, 3, 0);

        thiswaypoint = (GameObject)Instantiate(waypoint, desiredPos, thisOb.gameObject.transform.rotation);
        thiswaypoint.transform.SetParent(waypointParent.transform);
        endWaypoint.transform.SetParent(waypointParent.transform);
        wayp.CheckChild();

        Vector3 desiredPosHealthBar = gameObject.transform.position + new Vector3(3, 7, 0);

        thisBatteryBar = (Image)Instantiate(batteryBar, desiredPosHealthBar, Quaternion.Euler(50, 90, 0));
        thisBatteryBar.transform.SetParent(healthBarHolder.transform);

        startbattery = battery;

    }

    private void Update()
    {
        
        if (health <= 0)
        {

            Destroy(thiswaypoint);
            Destroy(thisBatteryBar.gameObject);
            Destroy(gameObject);

        }

        if (timeLeft <= 0)
        {

            activated = false;

        }

        if (waited == true)
        {

            Countdown();
            waited = false;

        }

        if (activated == false)
        {

            StopAllCoroutines();

        }

        if (activated == true)
        {

            Shoot();

            float calcBat = battery / startbattery;

            thisBatteryBar.transform.Find("HealthBar").GetComponent<Image>().fillAmount = calcBat;

        }

    }

    void Countdown()
    {

        timeLeft -= 1;
        battery -= 1;
        StartCoroutine(ifActivated());
        return;

    }

    private void OnMouseDown()
    {

        activated = !activated;
        StartCoroutine(ifActivated());

    }

    IEnumerator ifActivated()
    {

        yield return new WaitForSeconds(1f);
        waited = true;

    }

    void Shoot()
    {

        GameObject bulletGO1 = (GameObject)Instantiate(shop.robot.projectile, robotfirePoint1.transform.position, robotfirePoint1.transform.rotation);
        GameObject bulletGO2 = (GameObject)Instantiate(shop.robot.projectile, robotfirePoint2.transform.position, robotfirePoint2.transform.rotation);
        BulletTest bullet1 = bulletGO1.GetComponent<BulletTest>();
        BulletTest bullet2 = bulletGO2.GetComponent<BulletTest>();

        if (bullet1 != null)
        {

            bullet1.Seek(target);
            bullet1.thisDamage = 99999999999;

        }
        if (bullet2 != null)
        {

            bullet2.Seek(target);
            bullet1.thisDamage = 99999999999;

        }

    }

}
