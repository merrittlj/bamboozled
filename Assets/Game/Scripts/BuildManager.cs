using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    public bool created = false;

    void Awake()
    {

        instance = this;

    }

    public GameObject standardTurretPrefab;
    public GameObject superTurretPrefab;
    public GameObject robotPrefab;

    public TurretBlueprint TurretToBuild;

    public bool CanBuild { get { return TurretToBuild != null; } }

    public void BuildTurretOn(Node node)
    {

        GameObject turretOb = (GameObject)Instantiate(TurretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        created = true;

    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {

        TurretToBuild.prefab = turret.prefab;

    }

}
