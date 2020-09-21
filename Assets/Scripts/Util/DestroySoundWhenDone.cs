using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundWhenDone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWhenDone());
    }

    IEnumerator DestroyWhenDone()
    {
        var sound = gameObject.GetComponent<AudioSource>();
        yield return new WaitForSeconds(1f);
        while(sound.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}