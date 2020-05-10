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

    public GameObject pandaPrefab;
    public GameObject medicPrefab;
    public GameObject robotPrefab;

    public TurretBlueprint TurretToBuild;

    public bool CanBuild { get { return TurretToBuild != null; } }

    public void BuildTurretOn(Node node)
    {

        GameObject turretOb = (GameObject)Instantiate(TurretToBuild.prefab, node.GetBuildPosition(), TurretToBuild.prefab.transform.rotation);
        created = true;

    }

    public void SelectTowerToBuild(TurretBlueprint tower)
    {

        TurretToBuild.prefab = tower.prefab;

    }

}
