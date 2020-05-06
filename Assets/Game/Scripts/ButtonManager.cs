using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadSinglePlayer()
    {

        SceneManager.LoadScene("SinglePlayer");

    }

    public void LoadTutorial()
    {

        SceneManager.LoadScene("Tutorial");

    }
}
