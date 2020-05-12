using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour
{

    public string whatValue;

    public static NumberManager instance;

    Shop shop;

    public int ones;
    public int tens;
    public int hundreds;

    private GameObject zeromodel;
    private GameObject onemodel;
    private GameObject twomodel;
    private GameObject threemodel;
    private GameObject fourmodel;
    private GameObject fivemodel;
    private GameObject sixmodel;
    private GameObject sevenmodel;
    private GameObject eightmodel;
    private GameObject ninemodel;

    private int curBal;

    [HideInInspector]
    public bool isdone;

    private GameObject tensHolder;

    private int doonce;

    private int amount;

    private void Awake()
    {

        instance = this;

    }

    private void Start()
    {

        shop = Shop.instance;

        zeromodel = transform.Find("0").gameObject;
        onemodel = transform.Find("1").gameObject;
        twomodel = transform.Find("2").gameObject;
        threemodel = transform.Find("3").gameObject;
        fourmodel = transform.Find("4").gameObject;
        fivemodel = transform.Find("5").gameObject;
        sixmodel = transform.Find("6").gameObject;
        sevenmodel = transform.Find("7").gameObject;
        eightmodel = transform.Find("8").gameObject;
        ninemodel = transform.Find("9").gameObject;

        tensHolder = GameObject.Find("TensNumberManager");

        ones = shop.balance;
        curBal = shop.balance;

    }

    private void Update()
    {

        tensHolder.GetComponent<NumberManager>().ones = ones;

        if (ones >= 10)
        {

            ones -= 10;
            tens += 1;
            isdone = false;

        }
        if (tens >= 10)
        {

            tens -= 10;
            hundreds += 1;

        }

        if (ones < 10)
        {

            isdone = true;

        }

        if (isdone == true)
        {

            if (curBal != shop.balance && doonce == 0)
            {

                amount = curBal - shop.balance;

                doonce = 1;

            }

        }

        if (doonce == 1)
        {

            if (amount >= 100)
            {

                hundreds -= 1;
                amount -= 100;
                Debug.Log(amount);

            }
            if (amount >= 10 && amount < 100)
            {

                tens -= 1;
                amount -= 10;

            }
            if (amount < 10 && amount != 0)
            {

                ones -= amount;
                amount = 0;

            }

            if (amount == 0)
            {

                doonce = 0;
                curBal = shop.balance;

            }

        }

        if (whatValue == "tens")
        {

            shownum(tens);

        }

        if (whatValue == "ones")
        {

            shownum(ones);

        }
        if (whatValue == "hundreds")
        {

            shownum(hundreds);

        }

    }

    private void showonly(GameObject obtoshow)
    {

        zeromodel.SetActive(false);
        onemodel.SetActive(false);
        twomodel.SetActive(false);
        threemodel.SetActive(false);
        fourmodel.SetActive(false);
        fivemodel.SetActive(false);
        sixmodel.SetActive(false);
        sevenmodel.SetActive(false);
        eightmodel.SetActive(false);
        ninemodel.SetActive(false);

        obtoshow.SetActive(true);

    }

    private void shownum(int numstoshow)
    {

        if (numstoshow == 0)
        {

            showonly(zeromodel);

        }

        if (numstoshow == 1)
        {

            showonly(onemodel);

        }
        if (numstoshow == 2)
        {

            showonly(twomodel);

        }
        if (numstoshow == 3)
        {

            showonly(threemodel);

        }

        if (numstoshow == 4)
        {

            showonly(fourmodel);

        }

        if (numstoshow == 5)
        {

            showonly(fivemodel);

        }
        if (numstoshow == 6)
        {

            showonly(sixmodel);

        }

        if (numstoshow == 7)
        {

            showonly(sevenmodel);

        }
        if (numstoshow == 8)
        {

            showonly(eightmodel);

        }

        if (numstoshow == 9)
        {

            showonly(ninemodel);

        }

    }

}
