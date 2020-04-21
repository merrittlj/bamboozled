using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    [Header("TierBlueprints")]
    public TierBlueprint turretTiers;

    private int isTier1 = 1;

    public static Upgrade instance;

    Shop shop;
    Turret turret;

    public string upgradeValue;

    void Start()
    {

        instance = this;

        shop = Shop.instance;
        turret = Turret.instance;

        turretTiers.UpgradeBar.SetActive(false);

    }

    void Update()
    {

        if (isTier1 == 1)
        {

            turretTiers.tiero2.SetActive(false);
            turretTiers.tierd2.SetActive(false);

        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

            //rayOb = hit.transform.gameObject;

            if (hit.transform.tag == "Panda")
            {

                if (Input.GetMouseButton(0))
                {

                    turretTiers.UpgradeBar.SetActive(true);
                   /* if (rayOb = gameObject)
                    {

                        GameObject[] tiero = GameObject.FindGameObjectsWithTag("TierOffense");
                        GameObject[] tierd = GameObject.FindGameObjectsWithTag("TierDefense");

                        foreach(GameObject tieroo in tiero)
                        {

                            tieroo.SetActive(true);

                        }
                        foreach (GameObject tierdd in tierd)
                        {

                            tierdd.SetActive(true);

                        }

                    }*/

                }

            }

            if (hit.transform.tag != "Panda" && hit.transform.tag != "UpgradeBar" && hit.transform.tag != "TierOffense" && hit.transform.tag != "TierDefense")
            {

                if (Input.GetMouseButton(0))
                {

                    turretTiers.UpgradeBar.SetActive(false);
                    return;

                }

            }

            if (Input.GetMouseButtonDown(0))
            {

                if (hit.transform.tag == "TierOffense")
                {

                    turretTiers.tierd2.SetActive(false);
                    turretTiers.tierd1.SetActive(false);

                    if (isTier1 == 1)
                    {

                        if (shop.balance > 100)
                        {

                            shop.balance -= 100;
                            turretTiers.tiero1.SetActive(false);
                            turretTiers.tiero2.SetActive(true);
                            isTier1 = 2;
                            upgradeValue = "TO1";
                            Debug.Log("f");
                            return;

                        }
                        return;

                    }

                    if (isTier1 == 2)
                    {

                        if (shop.balance > 500)
                        {
                            turretTiers.tiero2.SetActive(false);
                            shop.balance -= 500;
                            return;
                        }
                        return;

                    }

                }
                if (hit.transform.tag == "TierDefense")
                {

                    turretTiers.tiero2.SetActive(false);
                    turretTiers.tiero1.SetActive(false);

                }

            }

        }
    }

}
