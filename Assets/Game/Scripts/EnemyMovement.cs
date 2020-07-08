using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Attributes")]
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;

    float amount;

    public Vector3 offset;

    public static EnemyMovement instance;

    public GameObject bamboo;

    private void Awake()
    {

        instance = this;

    }

    void Start()
        {

        target = Waypoints.points[0];
        Vector3 dirwayy = target.transform.position;

        transform.LookAt(dirwayy);

        bamboo = GameObject.Find("Scene/Bamboo");

    }

        void Update()
        {

        if (target == null)
        {

            GetNextWayPoint();

        }

        if (target != null)
        {

            Vector3 dir = target.position - gameObject.transform.position;

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        }

        if (target != null && Vector3.Distance(transform.position, target.position) <= 0.4f)
        {

            GetNextWayPoint();

        }

        }

    void GetNextWayPoint() {

        if (wavepointIndex >= Waypoints.points.Length - 1)
        {

            Destroy(bamboo.gameObject.GetComponent<BambooHealth>().bambootogeteaten);
            bamboo.gameObject.GetComponent<BambooHealth>().newbamboo = true;
            Destroy(gameObject);
            return;

        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

        if (target != null)
        {

            Vector3 dirwayy = target.transform.position;

            transform.LookAt(dirwayy);

        }

    }

}
