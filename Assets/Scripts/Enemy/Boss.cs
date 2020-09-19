using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static BulletSpawnDefinition InterlievedFan(float speed, int volleyCount)
    {
        return new FanSpawn(SpawnPattern.InterleavedFan(15, 25, 1f, SpawnPattern.Single(speed)), volleyCount, 1f, 2f, center: false);
    }

    public void Start()
    {
        gameObject.GetComponent<Spawner>().Spawn(InterlievedFan(1f, 1), Vector2.zero, 270f);
    }
}