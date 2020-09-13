using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraDebugger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MainCamera.Shake(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MainCamera.Shake(0.75f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MainCamera.Shake(1f);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            MainCamera.Shake(2f);
        }
    }
}
