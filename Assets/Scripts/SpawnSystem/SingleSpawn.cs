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

    public override IEnumerable<(Vector2 offset, float unitAngle, float speed, float delay)> GetSpawnPattern()
    {
        yield return spawnPos;
    }
}