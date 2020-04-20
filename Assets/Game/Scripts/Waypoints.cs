using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Waypoints instance;

    public static Transform[] points;

    private void Awake()
    {

        instance = this;

        CheckChild();

    }

    public void CheckChild()
     {

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {

            points[i] = transform.GetChild(i);

        }

     }

}
