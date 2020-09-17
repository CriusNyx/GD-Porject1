using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnPattern
{
    /// <summary>
    /// Creates a fan blade with two bursts, with bullets interleaved.
    /// </summary>
    /// <param name="fanAngle"></param>
    /// <param name="bulletsPerVolley"></param>
    /// <param name="delayPerVolley"></param>
    /// <param name="bulletSpeed"></param>
    /// <returns></returns>
    public static BulletSpawnDefinition InterleavedFan(float fanAngle, int bulletsPerVolley, float delayPerVolley, float bulletSpeed) 
        => InterleavedFan(fanAngle, bulletsPerVolley, delayPerVolley, new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, bulletSpeed, 0f)));

    /// <summary>
    /// Creates a fan blade with two bursts, with bullets interleaved.
    /// </summary>
    /// <param name="fanAngle"></param>
    /// <param name="bulletsPerVolley"></param>
    /// <param name="delay"></param>
    /// <param name="baseDefinition"></param>
    /// <returns></returns>
    public static BulletSpawnDefinition InterleavedFan(float fanAngle, int bulletsPerVolley, float delay, BulletSpawnDefinition baseDefinition)
    {
        return new PatternSpawn(delay, new FanSpawn(baseDefinition, bulletsPerVolley, fanAngle), new FanSpawn(baseDefinition, bulletsPerVolley - 1, fanAngle));
    }

    /// <summary>
    /// Creates a fan with line shaped blades that expand over time
    /// </summary>
    /// <param name="anglePerBullet"></param>
    /// <param name="bulletsPerLine"></param>
    /// <param name="linesPerVolley"></param>
    /// <param name="speedGrowthPerBullet"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public static BulletSpawnDefinition AcceleratingFan(float anglePerBullet, int bulletsPerLine, int linesPerVolley, float speedGrowthPerBullet, float speed) 
        => AcceleratingFan(anglePerBullet, bulletsPerLine, linesPerVolley, speedGrowthPerBullet, new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, speed, 0f)));

    public static BulletSpawnDefinition AcceleratingFan(float anglePerBullet, int bulletsPerLine, int linesPerVolley, float speedGrowthPerBullet, BulletSpawnDefinition baseDefinition)
    {
        return new FanSpawn(new LineSpawn(baseDefinition, linesPerVolley, Vector2.zero, 0f, speedGrowthPerBullet), linesPerVolley, anglePerBullet);
    }
}
