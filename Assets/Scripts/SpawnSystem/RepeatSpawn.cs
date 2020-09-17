using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSpawn : BulletSpawnDefinition
{
    public readonly int repeatCount;
    public readonly float timeDelayPerSpawn;

    public RepeatSpawn(BulletSpawnDefinition child, int repeatCount, float timeDelayPerSpawn) : base(child)
    {
        this.repeatCount = repeatCount;
        this.timeDelayPerSpawn = timeDelayPerSpawn;
    }

    public override IEnumerable<(Vector2 offset, float unitAngle, float speed, float delay)> GetSpawnPattern()
    {
        for (int i = 0; i < repeatCount; i++)
        {
            foreach(var (offset, angle, speed, delay) in child.GetSpawnPattern())
            {
                yield return (offset, angle, speed, delay + i * timeDelayPerSpawn);
            }
        }
    }
}