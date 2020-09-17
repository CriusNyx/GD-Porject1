using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletSpawnDefinition
{
    protected BulletSpawnDefinition child;

    protected BulletSpawnDefinition(BulletSpawnDefinition child)
    {
        this.child = child;
    }

    public abstract IEnumerable<(Vector2 offset, float unitAngle, float speed, float delay)> GetSpawnPattern();
}
