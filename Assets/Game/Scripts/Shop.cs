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
    public TurretBlueprint turret;
    public TurretBlueprint superturret;
    public TurretBlueprint robot;

    private string turretSelected;

    public static Shop instance;

    Node node;

    private bool waited;

    private void Awake()
    {

        instance = this;

    }

    void Start()
    {
        node = Node.instance;
        buildManager = BuildManager.instance;
    }

    private void Update()
    {

        if (buildManager.created == true)
        {

            buildManager.SelectTurretToBuild(turretPlaceHolder);
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

    public void SelectTurret()
    {

            if (balance >= turret.cost)
            {

                balance -= turret.cost;
                Debug.Log("Turret Selected");
                buildManager.SelectTurretToBuild(turret);
                turretSelected = "turret";
                return;

            }

            else
            {

                Debug.Log("Not enouth money!");
                return;

            }

    }
    public void SelectSuperturret()
    {

        if (balance >= superturret.cost)
        {

                balance -= superturret.cost;
                Debug.Log("Super turret Selected");
                buildManager.SelectTurretToBuild(superturret);
                turretSelected = "superturret";
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
            buildManager.SelectTurretToBuild(robot);
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

}
