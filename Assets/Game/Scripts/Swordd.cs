using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordd : MonoBehaviour
{

    public float amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "ExplosiveEnemy" || other.gameObject.tag == "BigEnemy")
        {

            other.gameObject.GetComponent<Enemy>().TakeDamage(amount);

        }

    }

}
