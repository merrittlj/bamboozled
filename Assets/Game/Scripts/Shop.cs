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
    public TextMeshProUGUI MoneyText;
    public TurretBlueprint turretPlaceHolder;

    [Header("TurretBlueprints")]
    public TurretBlueprint panda;
    public TurretBlueprint medic;
    public TurretBlueprint robot;

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

        MoneyText.text = balance.ToString();


        if (balance <= 0)
        {

            Debug.Log("No money left!");

        }

    }

    public void SelectPanda()
    {

            if (balance >= panda.cost)
            {

                balance -= panda.cost;
                Debug.Log("Panda Selected");
                buildManager.SelectTowerToBuild(panda);
                return;

            }

            else
            {

                Debug.Log("Not enouth money!");
                return;

            }

    }
    public void SelectMedic()
    {

        if (balance >= medic.cost)
        {

                balance -= medic.cost;
                Debug.Log("Medic Selected");
                buildManager.SelectTowerToBuild(medic);
                return;

        }

        else
        {

            Debug.Log("Not enouth money!");
            return;

        }

    }
    public void SelectRobot()
    {

        if (balance >= robot.cost)
        {

            balance -= robot.cost;
            Debug.Log("Robot Selected");
            buildManager.SelectTowerToBuild(robot);
            turretSelected = "robot";
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

                SelectPanda();

            }

            if (WhatTower == "Medic")
            {

                SelectMedic();

            }

            if (WhatTower == "Robot")
            {

                SelectRobot();

            }

        }

    }

}
