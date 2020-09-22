using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Logic());
    }

    IEnumerator Logic()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("WinMenu");
    }
}
