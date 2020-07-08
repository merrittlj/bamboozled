using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public Animator anim;

    public void LoadSinglePlayer()
    {

        StartCoroutine(LoadLevel("SinglePlayer"));

    }

    public void LoadTutorial()
    {

        StartCoroutine(LoadLevel("Tutorial"));

    }
    public void loadMenu()
    {

        StartCoroutine(LoadLevel("Menu"));

    }

    IEnumerator LoadLevel(string scene)
    {

        anim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);

    }

}
