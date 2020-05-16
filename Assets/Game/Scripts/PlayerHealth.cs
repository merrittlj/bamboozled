using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    Health health;

    Image healthbar;

    void Start()
    {

        health = Health.instance;

        healthbar = this.GetComponent<Image>();

    }

    void Update()
    {

        healthbar.fillAmount = health.health / health.startHealth;

    }
}
