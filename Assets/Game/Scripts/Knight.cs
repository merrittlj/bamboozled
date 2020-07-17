using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    Turret turret;

    public GameObject sword;

    // Start is called before the first frame update
    void Start()
    {

        turret = Turret.instance;

    }

    // Update is called once per frame
    void Update()
    {

        sword.transform.localScale = new Vector3(turret.range / 1.4f, turret.range / 1.4f, turret.range / 1.4f);

    }
}
