using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{

    Shop shop;

    WaveSpawner wave;

    private bool waited;

    public int amount;

    // Start is called before the first frame update
    void Start()
    {

        shop = Shop.instance;

        wave = WaveSpawner.instance;

        waited = true;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (waited == true && wave.isstarted == true)
        {

            shop.balance += amount;
            StartCoroutine(wait1(5));

        }

    }
    
    IEnumerator wait1(float time)
    {

        waited = false;
        yield return new WaitForSeconds(time);
        waited = true;

    }

}
