using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public static Robot instance;

    Waypoints wayp;
    Turret turret;

    [Header("Attributes")]
    public int timeLeft = 10;

    public bool activated = false;
    private bool waited = false;

    private GameObject waypoint;
    private GameObject waypointParent;
    private GameObject endWaypoint;
    private GameObject thisOb;

    public int howmanytime;

    [Header("Health Bar Stuff")]
    public Image batteryBar;
    private Image thisBatteryBar;
    private int calculateHealth;
    private GameObject healthBarHolder;
    private GameObject desiredRotOb;
    private Image thisBatteryBarFill;
    private Vector3 desiredPosHealthBar;
    private float startbattery;
    public float battery;

    private void Awake()
    {

        instance = this;

    }

    private void Start()
    {

        turret = Turret.instance;
        wayp = Waypoints.instance;

        thisOb = this.gameObject;
        waypoint = GameObject.Find("Waypoints/Waypoint");
        waypointParent = GameObject.Find("Waypoints");
        endWaypoint = GameObject.Find("EndWaypoint");
        healthBarHolder = GameObject.Find("Canvases/HealthBarCanvas");
        desiredRotOb = GameObject.Find("DesiredRotHealthBarOb");

        endWaypoint.transform.SetParent(transform);

        Vector3 desiredPos = thisOb.gameObject.transform.position + new Vector3(0, 3, 0);

        GameObject wayPoint = (GameObject)Instantiate(waypoint, desiredPos, thisOb.gameObject.transform.rotation);
        wayPoint.transform.SetParent(waypointParent.transform);
        endWaypoint.transform.SetParent(waypointParent.transform);
        wayp.CheckChild();

        Vector3 desiredPosHealthBar = gameObject.transform.position + new Vector3(3, 7, 0);

        thisBatteryBar = (Image)Instantiate(batteryBar, desiredPosHealthBar, Quaternion.Euler(50, 90, 0));
        thisBatteryBar.transform.SetParent(healthBarHolder.transform);

        startbattery = battery;

    }

    private void Update()
    {
        
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

            turret.Shoot();

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

}
