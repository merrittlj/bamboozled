using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    [Header("TierBlueprints")]
    public TierBlueprint turretTiers;

    private GameObject td1;
    private GameObject td2;
    private GameObject to1;
    private GameObject to2;

    private int isTier1 = 1;

    public static Upgrade instance;

    Shop shop;
    Turret turret;

    public string upgradeValue;

    private GameObject hitObTur;

    GameObject cur;

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

                }

            }

            if (Input.GetKey(KeyCode.L))
            {

                hit.transform.gameObject.GetComponent<Turret>().thisUpGBAR.name = "curTurUp";
                cur = GameObject.Find("curTurUp");
                to1 = cur.transform.Find("UpgradeTurretTierOffense1").gameObject;
                to2 = cur.transform.Find("UpgradeTurretTierOffense2").gameObject;
                td1 = cur.transform.Find("UpgradeTurretTierDefense1").gameObject;
                td2 = cur.transform.Find("UpgradeTurretTierDefense2").gameObject;

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

                    td2.SetActive(false);
                    td1.SetActive(false);

                    if (isTier1 == 1)
                    {

                        if (shop.balance > 100)
                        {

                            shop.balance -= 100;
                            to1.SetActive(false);
                            to2.SetActive(true);
                            isTier1 = 2;
                            upgradeValue = "TO1";
                            return;

                        }

                    }

                    if (isTier1 == 2)
                    {

                        if (shop.balance > 500)
                        {
                            to2.SetActive(false);
                            shop.balance -= 500;
                            return;
                        }

                    }

                }
                if (hit.transform.tag == "TierDefense")
                {

                    to2.SetActive(false);
                    to1.SetActive(false);

                }

            }

        }
    }

}
