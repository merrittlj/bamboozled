using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{

    public static GameOverText instance;

    public GameObject gameoverpanel;
    public TextMeshProUGUI timetext;
    public TextMeshProUGUI zombietext;
    public TextMeshProUGUI pandatext;

    public float zombiesdied;
    public float pandasdied;
    public float seconds;
    public float minutes;

    public bool isdone;

    private bool waited;

    private void Awake()
    {

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {

        waited = true;

        gameoverpanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (isdone == true)
        {

            gameoverpanel.SetActive(true);

            timetext.text = "Survived for " + minutes + " minutes and " + seconds + " seconds";

            zombietext.text = "Killed " + zombiesdied + " zombies";

            pandatext.text = pandasdied + " pandas died";

        }

        if (isdone == false)
        {

            if (waited == true)
            {

                waited = false;
                StartCoroutine(timecount());

            }

            if (seconds == 60)
            {

                seconds = 0;
                minutes++;

            }

        }

    }

    IEnumerator timecount()
    {

        yield return new WaitForSeconds(1);
        waited = true;
        seconds++;

    }

}
