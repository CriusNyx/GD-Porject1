using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemDebugger : MonoBehaviour
{
    Spawner spawner;

    int frameCount;

    public void Start()
    {
        spawner = gameObject.GetComponent<Spawner>();

        var baseSpawn = new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, 1f, 0f));

        var pattern = new FanSpawn(baseSpawn, 12, 30f);

        spawner.Spawn(pattern, new Vector2(1, 1) * 3f, 0f);
        spawner.Spawn(pattern, new Vector2(1, -1) * 3f, 0f);
        spawner.Spawn(pattern, new Vector2(-1, 1) * 3f, 0f);
        spawner.Spawn(pattern, new Vector2(-1, -1) * 3f, 0f);
    }
}
