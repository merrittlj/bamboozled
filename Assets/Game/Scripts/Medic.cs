using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medic : MonoBehaviour
{

    private GameObject closestTower;
    public float closestDistance;
    private LineRenderer lr;

    GameObject[] towers;

    private bool iswaited;

    private void Start()
    {

        lr = GameObject.Find("lr").gameObject.GetComponent<LineRenderer>();

        lr.enabled = false;

        StartCoroutine(heal());

    }

    private void Update()
    {

        string[] tagsToDisable = { "Panda", "Robot"};
        foreach (string tag in tagsToDisable)
        {
            towers = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject tower in towers)
            {

                float distance = Vector3.Distance(gameObject.transform.position, tower.transform.position);

                if (distance < closestDistance)
                {

                    closestDistance = distance;

                    closestTower = tower;

                    lr.enabled = true;

                }

            }

        }

        if (closestTower != null)
        {

            lr.SetPosition(0, gameObject.transform.position + new Vector3(0, 2.5f, 0));
            lr.SetPosition(1, closestTower.transform.position + new Vector3(0, 2.5f, 0));

            lr.material.mainTextureScale = new Vector2(closestDistance * 4, 1);

            if (iswaited == true)
            {

                if (closestTower.GetComponent<Turret>().health != closestTower.GetComponent<Turret>().startHealth)
                {

                    closestTower.GetComponent<Turret>().health += 1;
                    closestTower.GetComponent<Turret>().thisHealthBar.transform.Find("HealthBar").GetComponent<Image>().fillAmount = closestTower.GetComponent<Turret>().health / closestTower.GetComponent<Turret>().startHealth;
                    iswaited = false;
                    StartCoroutine(heal());

                }

            }

        }

        if (closestTower == null)
        {

            lr.enabled = false;

        }

    }

    IEnumerator heal()
    {

        yield return new WaitForSeconds(1f);
        iswaited = true;

    }

}
