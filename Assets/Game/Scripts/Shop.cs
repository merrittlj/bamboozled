using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    [Header("Attributes")]
    public int balance;

    [Header("Prefabs")]
    public TurretBlueprint turretPlaceHolder;

    [Header("TurretBlueprints")]
    public TurretBlueprint panda;
    public TurretBlueprint medic;
    public TurretBlueprint robot;
    public TurretBlueprint farm;
    public TurretBlueprint knight;

    private string turretSelected;

    public static Shop instance;

    Node node;

    NumberManager numman;

    private bool waited;

    private void Awake()
    {

        instance = this;

    }

    void Start()
    {
        node = Node.instance;
        buildManager = BuildManager.instance;
        numman = NumberManager.instance;
    }

    private void Update()
    {

        if (buildManager.created == true)
        {

            buildManager.SelectTowerToBuild(turretPlaceHolder);
            StartCoroutine(waitabit());
            if (waited == true)
            {

                buildManager.created = false;

            }

        }

    }

    public void SelectTurb(TurretBlueprint turb)
    {

        if (balance >= turb.cost)
        {

            balance -= turb.cost;
            buildManager.SelectTowerToBuild(turb);
            return;

        }

        else
        {

            Debug.Log("Not enouth money!");
            return;

        }

    }

    IEnumerator waitabit()
    {

        yield return new WaitForSeconds(0.1f);
        waited = true;

    }

    public void SelectTower(string WhatTower)
    {

        if (numman.isdone == true)
        {

            if (WhatTower == "Panda")
            {

                SelectTurb(panda);

            }

            if (WhatTower == "Medic")
            {

                SelectTurb(medic);

            }

            if (WhatTower == "Robot")
            {

                SelectTurb(robot);

            }

            if (WhatTower == "Farm")
            {

                SelectTurb(farm);

            }

            if (WhatTower == "Knight")
            {

                SelectTurb(knight);

            }

        }

    }

}
