using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{

    Turret turret;

    public GameObject upgradeBar;

    private void Start()
    {

        turret = Turret.instance;

    }

    private void OnMouseDown()
    {

        upgradeBar.SetActive(false);
        turret.isclick = 0;

    }

}
