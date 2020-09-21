using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Spawner spawner;

    public GameObject ringPrefab;
    private GameObject[] rings;
    public GameObject player;
    public GameObject body;

    public bool alive = true;

    new private Rigidbody2D rigidbody;

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

    public void Start()
    {
        spawner = gameObject.GetComponent<Spawner>();


        var ring1 = new GameObject("Ring1");
        ring1.transform.parent = transform;
        var ring1Ring = ring1.AddComponent<BossRing>();
        ring1Ring.baseSpeed = 1f;
        ring1Ring.pointPrefab = ringPrefab;

        var ring2 = new GameObject("Ring2");
        ring2.transform.parent = transform;
        ring2.transform.localRotation = Quaternion.Euler(0f, 90f, 60f);
        ring2.transform.localScale = Vector3.one * 1.2f;
        var ring2Ring = ring2.AddComponent<BossRing>();
        ring2Ring.baseSpeed = Mathf.PI / 2f;
        ring2Ring.pointPrefab = ringPrefab;

        var ring3 = new GameObject("Ring3");
        ring3.transform.parent = transform;
        ring3.transform.localRotation = Quaternion.Euler(30f, 0f, -60f);
        ring3.transform.localScale = Vector3.one * 0.8f;
        var ring3Ring = ring3.AddComponent<BossRing>();
        ring3Ring.baseSpeed = Mathf.PI * Mathf.PI / 6f;
        ring3Ring.pointPrefab = ringPrefab;

        rings = new GameObject[] { ring1, ring2, ring3 };

        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        Difficulty.SetBaseDifficultyLevel(Difficulty.DifficultyLevel.VeryHard);

        body = transform.Find("Body").gameObject;

        StartCoroutine(Logic());
    }

    float seed1 = 1, seed2 = 100000, seed3 = 1000000000;

    public void Update()
    {

        for (int i = 0; i < 3; i++)
        {
            float x = Mathf.PerlinNoise(seed1 + Time.time + i * 1000, 0f);
            float y = Mathf.PerlinNoise(seed2 + Time.time + i * 1000, 0f);
            float z = Mathf.PerlinNoise(seed3 + Time.time + i * 1000, 0f);

            rings[i].transform.localRotation *= Quaternion.Euler(x * 30 * Time.deltaTime, y * 30 * Time.deltaTime, z * 30 * Time.deltaTime);
        }
        UpdateScreenShake();
    }

    private IEnumerator Logic()
    {
        while (alive)
        {
            yield return Move(Difficulty.GetValue(50, 50, 100, 100, 200));

            float v = Random.Range(0f, 1f);
            if (v < 0.1f)
            {
                yield return PlayInterlievedRing();
                yield return new WaitForSeconds(Difficulty.GetValue(5f, 4f, 4f, 3f, 2f));
            }
            else if (v < 0.4f)
            {
                yield return PlayInterlievedFan();
                yield return new WaitForSeconds(Difficulty.GetValue(5f, 4f, 4f, 3f, 2f));
            }
            else
            {

                for (int i = 0; i < Random.Range(Difficulty.GetValue(3, 3, 6, 6, 15), Difficulty.GetValue(5, 5, 10, 10, 20)); i++)
                {
                    yield return Move(Difficulty.GetValue(50, 50, 100, 100, 200));
                    yield return PlayFastFan();
                    yield return new WaitForSeconds(Difficulty.GetValue(0.8f, 0.6f, 0.4f, 0.2f, 0f));
                }
                yield return new WaitForSeconds(Difficulty.GetValue(5f, 4f, 4f, 3f, 3f));
            }
        }
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
        body.transform.localPosition = new Vector2(xOffset, yOffset) * currentMagnitude;

        // Decay current screen shake value
        currentScreenShake -= Time.deltaTime * screenShakeDecay;
        currentScreenShake = Mathf.Max(0f, currentScreenShake);
    }

    public void Shake(float value)
    {
        currentScreenShake = Mathf.Max(currentScreenShake, value, 0f);
    }

    private IEnumerator Move(float speed = 50f)
    {
        float x = Random.Range(-10f, 10f);
        float y = 8f;
        Vector2 target = new Vector2(x, y);
        while (Vector2.Distance(transform.position, target) > 0.1f)
        {
            rigidbody.MoveTowards(target, speed);
            yield return null;
        }
        rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator PlayInterlievedRing()
    {
        Vector2 playerVec = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerVec.y, playerVec.x) * Mathf.Rad2Deg;

        float time
                = spawner.Spawn(
                    InterlievedRing(
                        Difficulty.GetValue(1f, 1.2f, 1.4f, 1.6f, 1.8f),
                        Difficulty.GetValue(1, 2, 3, 5, 10),
                        Difficulty.GetValue(2f, 1.8f, 1.6f, 1.4f, 1.2f)),
                    transform.position,
                    angle);
        yield return new WaitForSeconds(time);
    }

    private IEnumerator PlayInterlievedFan()
    {
        Vector2 playerVec = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerVec.y, playerVec.x) * Mathf.Rad2Deg;

        float time
                = spawner.Spawn(
                    InterlievedFan(
                        Difficulty.GetValue(1f, 1.2f, 1.4f, 1.6f, 1.8f),
                        Difficulty.GetValue(1, 2, 3, 5, 10),
                        Difficulty.GetValue(2f, 1.8f, 1.6f, 1.4f, 1.2f),
                        5),
                    transform.position,
                    angle);

        yield return new WaitForSeconds(time);
    }

    private IEnumerator PlayFastFan()
    {
        Vector2 playerVec = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(playerVec.y, playerVec.x) * Mathf.Rad2Deg;

        float time
            = spawner.Spawn(
                FastFan(
                    Difficulty.GetValue(1f, 1.2f, 1.4f, 1.6f, 1.8f),
                    15,
                    Difficulty.GetValue(2, 3, 4, 5, 6),
                    Difficulty.GetValue(0.5f, 0.4f, 0.3f, 0.2f, 0.1f)),
                transform.position,
                angle);

        yield return new WaitForSeconds(time);
    }

    public static BulletSpawnDefinition InterlievedRing(float speed, int volleyCount, float spawnPeriod)
    {
        return new FanSpawn(
            SpawnPattern.InterleavedFan(
                15,
                25,
                spawnPeriod / 2f,
                SpawnPattern.Single(speed)),
            volleyCount,
            1f,
            spawnPeriod,
            center: false);
    }

    public static BulletSpawnDefinition InterlievedFan(float speed, int volleyCount, float spawnPeriod, int bulletsPerVolley)
    {
        return new FanSpawn(
            SpawnPattern.InterleavedFan(
                15,
                bulletsPerVolley,
                spawnPeriod / 2f,
                SpawnPattern.Single(speed)),
            volleyCount,
            1f,
            spawnPeriod,
            center: false);
    }

    public static BulletSpawnDefinition FastFan(float speed, float totalAngle, int bulletCount, float spawnTime)
    {
        if (bulletCount < 2)
        {
            throw new System.ArgumentException($"Cannot spawn a fan with {bulletCount} bullets. At least 2 are needed.");
        }
        float anglePerBullet = totalAngle / (bulletCount - 1);
        float delay = spawnTime / (bulletCount - 1);

        return new FanSpawn(new SingleSpawn(speed, totalAngle / 2f), bulletCount, -anglePerBullet, delay, center: false);
    }
}