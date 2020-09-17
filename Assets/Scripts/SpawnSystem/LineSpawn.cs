using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns bullets in a line
/// </summary>
public class LineSpawn : BulletSpawnDefinition
{
    /// <summary>
    /// The number of projectiles to spawn
    /// </summary>
    public readonly int projectileCount;
    /// <summary>
    /// The positional offset of each bullet
    /// </summary>
    public readonly Vector2 offsetPerSpawn;
    /// <summary>
    /// Time delay per bullet spawn
    /// </summary>
    float delayPerSpawn;
    /// <summary>
    /// The increase in speed per spawn
    /// </summary>
    float deltaSpeedPerSpawn;
    /// <summary>
    /// If true, the position will represent the center of the line, instead of the end
    /// </summary>
    bool center;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="child">The pattern to spawn in a line.</param>
    /// <param name="projectileCount">The number of projectiles to spawn</param>
    /// <param name="offsetPerSpawn">The positional offset of each bullet</param>
    /// <param name="delayPerSpawn">Time delay per bullet spawn</param>
    /// <param name="deltaSpeedPerSpawn">The increase in speed per spawn</param>
    /// <param name="center">If true, the position will represent the center of the line, instead of the end</param>
    public LineSpawn(BulletSpawnDefinition child, int projectileCount, Vector2 offsetPerSpawn, float delayPerSpawn = 0f, float deltaSpeedPerSpawn = 0f, bool center = false) : base(child)
    {
        this.projectileCount = projectileCount;
        this.offsetPerSpawn = offsetPerSpawn;
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
            Vector2 deltaOffset = j * offsetPerSpawn;
            float deltaSpeed = j * deltaSpeedPerSpawn;
            float deltaDelay = j * delayPerSpawn;

            foreach (var (offset, unitAngle, speed, delay) in child.GetSpawnPattern())
            {
                yield return (offset + deltaOffset, unitAngle, speed + deltaSpeed, delay + deltaDelay);
            }
        }
    }
}
