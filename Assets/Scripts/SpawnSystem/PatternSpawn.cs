using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatternSpawn : BulletSpawnDefinition
{
    private BulletSpawnDefinition[] elements;
    float delayPerSpawn = 0f;

    public PatternSpawn(float delayPerSpawn = 0f, params BulletSpawnDefinition[] elements) : base(null)
    {
        this.elements = elements;
        this.delayPerSpawn = delayPerSpawn;
    }

    public PatternSpawn(IEnumerable<BulletSpawnDefinition> elements, float delayPerSpawn) : base(null)
    {
        this.elements = elements.ToArray();
        this.delayPerSpawn = delayPerSpawn;
    }

    public override IEnumerable<(Vector2 offset, float unitAngle, float speed, float delay)> GetSpawnPattern()
    {
        for(int i = 0; i < elements.Length; i++)
        {
            float deltaT = i * delayPerSpawn;
            foreach(var (offset, angle, speed, delay) in elements[i].GetSpawnPattern())
            {
                yield return (offset, angle, speed, delay + i * deltaT);
            }
        }
    }
}