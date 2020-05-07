using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{

    Shop shop;

    public string whatToSelect;

    private void Start()
    {

        shop = Shop.instance;

    }

    private void OnMouseDown()
    {

        shop.SelectTower(whatToSelect);

    }
}
