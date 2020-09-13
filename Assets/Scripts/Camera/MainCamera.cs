using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to the root of the main camera object
/// This class controls camera position, and properties such as screen shake
/// </summary>
public class MainCamera : MonoBehaviour
{
    private static MainCamera mainCamera;

    new private Camera camera;
    private GameObject cameraObject;

    private const float PERLIN_NOISE_X_OFFSET = 1000f;
    private const float PERLIN_NOISE_Y_OFFSET = 10000000f;

    private float currentScreenShake = 0f;

    /// <summary>
    /// How many units of screen shake intensity 1 will produce
    /// </summary>
    [Tooltip("How many unity units of screen shake intensity 1 will produce.")]
    public float screenShakeMagnitude = 1f;

    /// <summary>
    /// How many units per second screen shake will decay by
    /// </summary>
    [Tooltip("How many units per second screen shake will decay by.")]
    public float screenShakeDecay = 2f;

    /// <summary>
    /// Determines how violent the screen shake will appear.
    /// </summary>
    [Tooltip("Determines how violent the screen shake will appear.")]
    public float screenShakeSpeed = 20f;

    private void Awake()
    {
        mainCamera = this;
    }

    private void Start()
    {
        camera = gameObject.GetComponentInChildren<Camera>();
        cameraObject = camera.gameObject;
    }

    private void OnDestroy()
    {
        if(mainCamera == this)
        {
            mainCamera = null;
        }
    }

    private void Update()
    {
        UpdateScreenShake();
    }

    private void UpdateScreenShake()
    {
        // Determine how far the camera should be shaking currently
        // Squaring the currentScreenShake value will cause smoother transitions into and out of screen shake
        float currentMagnitude = currentScreenShake * currentScreenShake * screenShakeMagnitude;

        // Make sure magnitude doesn't go below 0
        currentMagnitude = Mathf.Max(currentMagnitude, 0f);

        // Get the current x and y offset using perlin noise
        // the * 2 - 1 function at the end will remap the output from [0, 1] -> [-1, 1]
        float xOffset = Mathf.PerlinNoise(screenShakeSpeed * Time.time, PERLIN_NOISE_X_OFFSET) * 2f - 1f;
        float yOffset = Mathf.PerlinNoise(screenShakeSpeed * Time.time, PERLIN_NOISE_Y_OFFSET) * 2f - 1f;

        // Apply screenshake to camera
        cameraObject.transform.localPosition = new Vector2(xOffset, yOffset) * currentMagnitude;

        // Decay current screen shake value
        currentScreenShake -= Time.deltaTime * screenShakeDecay;
        currentScreenShake = Mathf.Max(0f, currentScreenShake);
    }

    public static void Shake(float value)
    {
        mainCamera?._Shake(value);
    }

    private void _Shake(float value)
    {
        currentScreenShake = Mathf.Max(currentScreenShake, value, 0f);
    }
}
