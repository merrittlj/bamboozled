using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    public List<GameObject> slides;

    public int curslide;

    private void Start()
    {

        hideAll();

        slides.First().SetActive(true);

    }

    private void Update()
    {

        if (curslide > slides.Count - 1)
        {

            SceneManager.LoadScene("Menu");

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            curslide += 1;

            if (curslide > slides.Count)
            {

                SceneManager.LoadScene("Menu");

            }

            hideAll();

            GameObject slidetoshow = slides[curslide];

            slidetoshow.SetActive(true);

        }

    }

    private void hideAll()
    {

        foreach (GameObject slide in slides)
        {

            slide.SetActive(false);

        }

    }
}
