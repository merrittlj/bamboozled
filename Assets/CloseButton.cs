using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{

    public GameObject upgradeBar;

    private void Start()
    {

        upgradeBar.SetActive(true);

    }

    private void OnMouseDown()
    {

        upgradeBar.SetActive(false);

    }

}
