using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpawn : BulletSpawnDefinition
{
    int projectileCount;
    float deltaAnglePerSpawn;
    float delayPerSpawn;
    float deltaSpeedPerSpawn;
    bool center;

    /// <summary>
    /// Spawns the child bullet pattern in a fan
    /// </summary>
    /// <param name="child">The base pattern to use for the fan</param>
    /// <param name="projectileCount">The number of projectiles to spawn in the fan</param>
    /// <param name="deltaAnglePerSpawn">The angle difference per spawn</param>
    /// <param name="delayPerSpawn">The delay per spawn</param>
    /// <param name="deltaSpeedPerSpawn">The change in speed per spawn</param>
    /// <param name="center">If true, the spawn will fan out from the center, instead of the edge</param>
    public FanSpawn(BulletSpawnDefinition child, int projectileCount, float deltaAnglePerSpawn, float delayPerSpawn = 0f, float deltaSpeedPerSpawn = 0f, bool center = true) : base(child)
    {
        this.projectileCount = projectileCount;
        this.deltaAnglePerSpawn = deltaAnglePerSpawn;
        this.delayPerSpawn = delayPerSpawn;
        this.deltaSpeedPerSpawn = deltaSpeedPerSpawn;
        this.center = center;
    }

    public override IEnumerable<(Vector2 offset, float unitAngle, float speed, float delay)> GetSpawnPattern()
    {
        // This function maps the index values to the offset index.
        Func<int, float> MapIndex;

        if (center)
        {
            if (projectileCount % 2 == 0)
            {
                MapIndex = Series.HalfGrowingZigZag;
            }
            else
            {
                MapIndex = (x) => Series.GrowingZigZag(x);
            }
        }
        else
        {
            MapIndex = (x) => x;
        }

        for (int i = 0; i < projectileCount; i++)
        {
            float j = MapIndex(i);
            float deltaAngle = j * deltaAnglePerSpawn;
            float deltaDelay = j * delayPerSpawn;
            float deltaSpeed = j * deltaSpeedPerSpawn;
            foreach (var spawn in child.GetSpawnPattern())
            {
                var (offset, angle, speed, delay) = spawn;
                yield return (Quaternion.Euler(0f, 0f, deltaAngle) * offset, angle + deltaAngle, speed + deltaSpeed, delay + deltaDelay);
            }
        }
    }
}