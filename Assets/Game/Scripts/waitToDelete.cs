using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitToDelete : MonoBehaviour
{

    private void Start()
    {

        StartCoroutine(WaitDelete());

    }

    IEnumerator WaitDelete()
    {

        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }

}
