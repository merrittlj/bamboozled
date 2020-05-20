using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    BuildManager build;

    [Header("Enemies")]
    public GameObject normalEnemy;
    public GameObject explosiveEnemy;
    public GameObject bigEnemy;

    [Header("Prefabs")]
    public Transform SpawnPoint;
    public TextMeshProUGUI waveSpawnerCountdown;
    public TextMeshProUGUI waveNumber;

    [Header("Attributes")]
    public float TimeBetweenWaves = 5f;
    public float waitTime = 0.5f;

    private int waveIndex = 0;
    private float countDown = 2f;
    private int wave = 0;

    private bool isstarted = false;
    public GameObject startButton;

    private void Start()
    {

        build = BuildManager.instance;

        startButton.SetActive(false);

    }

    void Update()
    {

        if (build.created == true)
        {

            startButton.SetActive(true);

        }

        waveNumber.text = wave.ToString();

        if (countDown <= 0f)
        {

            wave++;
            StartCoroutine(spawnWave());
            countDown = TimeBetweenWaves;

        }

        if (isstarted == true)
        {

            countDown -= Time.deltaTime;

        }

    }

    IEnumerator spawnWave()
    {

        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {

            spawnEnemy();
            yield return new WaitForSeconds(waitTime);

        }


    }

    void spawnEnemy()
    {

        int whichEnemy = Random.Range(1, 11);

        if (whichEnemy == 1)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 2)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 3)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 4)
        {

            Instantiate(explosiveEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }

        if (whichEnemy == 5)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 6)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 7)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 8)
        {

            Instantiate(explosiveEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 9)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
        if (whichEnemy == 10)
        {

            Instantiate(bigEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }

    }

    public void startTimer()
    {

        isstarted = true;
        startButton.SetActive(false);

    }

}
