using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawn : BulletSpawnDefinition
{
    BulletSpawnPos spawnPos;

    public SingleSpawn(BulletSpawnPos spawnPos) : base(null)
    {
        this.spawnPos = spawnPos;
    }

    public SingleSpawn(float speed = 1f, float angle = 0f, float delay = 0f, Vector2 offset = default) : base(null)
    {
        this.spawnPos = new BulletSpawnPos(offset, angle, speed, delay);
    }

    public override IEnumerable<(Vector2 offset, float unitAngle, float speed, float delay)> GetSpawnPattern()
    {
        yield return spawnPos;
    }
}