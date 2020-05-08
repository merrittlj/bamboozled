using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Attributes")]
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;

    public static EnemyMovement instance;

    private void Awake()
    {

        instance = this;

    }

    void Start()
        {

        target = Waypoints.points[0];

        }

        void Update()
        {
        if (target == null)
        {

            GetNextWayPoint();

        }
        Vector3 dir = target.position - gameObject.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {

            GetNextWayPoint();

        }

        }

    void GetNextWayPoint() {

        if (wavepointIndex >= Waypoints.points.Length - 1)
        {

            Destroy(gameObject);
            return;

        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

    }

}
