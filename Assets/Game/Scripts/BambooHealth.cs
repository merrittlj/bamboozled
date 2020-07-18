using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooHealth : MonoBehaviour
{

    public static Transform[] bamboo;

    Health health;

    public Transform endwaypoint;

    public GameObject bambootogeteaten;

    public bool newbamboo = true;

    // Start is called before the first frame update
    void Start()
    {

        health = Health.instance;

        bamboo = new Transform[transform.childCount];

        for (int i = 0; i < bamboo.Length; i++)
        {

            bamboo[i] = transform.GetChild(i);

        }

        foreach (Transform bambooo in bamboo)
        {

            float randomz = Random.Range(1, 85);
            float randomx = Random.Range(-38, -30);

            bambooo.transform.position = new Vector3(randomx, 3, randomz);
            bambooo.transform.rotation = Quaternion.Euler(36, 90, 0);

        }

    }

    private void Update()
    {

        if (newbamboo == true) {

            int randombamboo = Random.Range(0, bamboo.Length + 1);

            bambootogeteaten = bamboo[randombamboo].gameObject;

            endwaypoint.transform.position = bambootogeteaten.gameObject.transform.position;

            newbamboo = false;

        }

        if (bamboo.Length == 0)
        {

            health.health = 0;

        }

    }

}
