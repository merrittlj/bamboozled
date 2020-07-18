using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    BuildManager buildmanager;
    public static Node instance;

    private Renderer rend;
    private GameObject turret;

    [Header("Attributes")]
    public Vector3 posOffset;

    [Header("Materials")]
    public Material mat;
    public Material startMat;

    private bool isBuiltOn;


    private void Awake()
    {

        instance = this;

    }
    void Start()
    {

        rend = GetComponent<Renderer>();

        buildmanager = BuildManager.instance;

    }

    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {

            return;

        }

        if (!buildmanager.CanBuild)
        {

            return;

        }

        if (isBuiltOn == false)
        {

            buildmanager.BuildTurretOn(this);
            isBuiltOn = true;

        }

    }

    public Vector3 GetBuildPosition()
    {

        return transform.position + posOffset;

    }

    void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {

            return;

        }

        if (!buildmanager.CanBuild)
        {

            return;

        }
        rend.material = mat;

    }

    void OnMouseExit()
    {

        rend.material = startMat;

    }

}
