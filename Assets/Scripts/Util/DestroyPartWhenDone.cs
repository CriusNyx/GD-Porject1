using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPartWhenDone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWhenDone());
    }

    IEnumerator DestroyWhenDone()
    {
        var system = gameObject.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(1f);
        while(system.particleCount > 0)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}