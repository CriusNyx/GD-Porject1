using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if(pos.x > 10.5)
        {
            pos.x = 10.5F;
        }
        if (pos.x < -10.5F)
        {
            pos.x = -10.5F;
        }
        if (pos.y > 5)
        {
            pos.y = 5;
        }
        if (pos.y < -5)
        {
            pos.y = -5;
        }
        transform.position = pos;
    }

}
