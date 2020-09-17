using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = width / 2f;
        float y = height / 2f;
        Vector3 pos = transform.position;
        if(pos.x > x)
        {
            pos.x = x;
        }
        if (pos.x < -x)
        {
            pos.x = -x;
        }
        if (pos.y > y)
        {
            pos.y = y;
        }
        if (pos.y < -y)
        {
            pos.y = -y;
        }
        transform.position = pos;
    }

}
