using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletSpawnPos
{
    public Vector2 offset;

    public float
        unitAngle,
        speed,
        delay;

    public BulletSpawnPos(Vector2 offset = default, float unitAngle = 0f, float speed = 1f, float delay = 0f)
    {
        this.offset = offset;
        this.unitAngle = unitAngle;
        this.speed = speed;
        this.delay = delay;
    }

    public static implicit operator (Vector2, float, float, float)(BulletSpawnPos spawnPos)
    {
        return (spawnPos.offset, spawnPos.unitAngle, spawnPos.speed, spawnPos.delay);
    }
}