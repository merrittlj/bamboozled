using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    Robot robot;

    private void Start()
    {

        robot = Robot.instance;

    }

    void Update()
    {
        if (robot.activated == true)
        {

            if (Input.GetMouseButton(0))
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Vector3 hitpos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    transform.LookAt(hitpos);
                }

            }

        }
    }
}
