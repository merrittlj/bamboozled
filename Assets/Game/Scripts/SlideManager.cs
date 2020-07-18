using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    public List<GameObject> slides;

    public int curslide;

    public Animator anim;

    private void Start()
    {

        hideAll();

        slides.First().SetActive(true);

    }

    private void Update()
    {

        if (curslide > slides.Count - 1)
        {

            StartCoroutine(scenetransition());

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            curslide += 1;

            if (curslide > slides.Count)
            {

                StartCoroutine(scenetransition());

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

    IEnumerator scenetransition()
    {

        anim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Menu");

    }

}
