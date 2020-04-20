using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public static Robot instance;

    Waypoints wayp;
    Turret turret;

    [Header("Attributes")]
    public int timeLeft = 30;

    public bool activated = false;
    private bool waited = false;

    private GameObject waypoint;
    private GameObject waypointParent;
    private GameObject endWaypoint;
    private GameObject thisOb;

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

        endWaypoint.transform.SetParent(transform);

        Vector3 desiredPos = thisOb.gameObject.transform.position + new Vector3(0, 3, 0);

        GameObject wayPoint = (GameObject)Instantiate(waypoint, desiredPos, thisOb.gameObject.transform.rotation);
        Instantiate(endWaypoint);
        wayPoint.transform.SetParent(waypointParent.transform);
        endWaypoint.transform.SetParent(waypointParent.transform);
        wayp.CheckChild();

    }

    private void Update()
    {
        
        if (timeLeft <= 0)
        {

            Debug.Log("BEEP BEEP BEEP BOOM");
            activated = false;

        }

        if (waited == true)
        {

            RobotShoot();
            waited = false;

        }

        if (activated == false)
        {

            StopAllCoroutines();

        }

        if (activated == true)
        {

            turret.Shoot();

        }

    }

    void RobotShoot()
    {

        timeLeft -= 1;
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
