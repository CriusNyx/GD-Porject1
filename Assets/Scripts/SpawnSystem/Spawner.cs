using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wintellect.PowerCollections;

public class Spawner : MonoBehaviour
{
    public GameObject prototype;
    OrderedBag<(Vector2 offset, float unitAngle, float speed, float delay)> priorityQueue;

    private void Awake()
    {
        priorityQueue = new OrderedBag<(Vector2 offset, float unitAngle, float speed, float delay)>((x, y) => x.delay.CompareTo(y.delay));
    }

    public void Spawn(BulletSpawnDefinition def, Vector2 position, float angle)
    {
        var data = def.GetSpawnPattern().Select(x => (x.offset + position, x.unitAngle + angle, x.speed, x.delay + Time.time));
        priorityQueue.AddMany(data);
    }

    private void FixedUpdate()
    {
        while(priorityQueue.Count > 0 && priorityQueue.GetFirst().delay <= Time.time)
        {
            var (offset, angle, speed, delay) = priorityQueue.RemoveFirst();

            var newBullet = Instantiate(prototype);
            newBullet.transform.position = offset;

            var projectile = newBullet.GetComponent<Projectile>();
            projectile.velocity = Quaternion.Euler(0f, 0f, angle) * Vector3.right * speed;
        }
    }
}
