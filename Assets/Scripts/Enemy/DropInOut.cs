using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInOut : MonoBehaviour
{

    public Vector2 startPos { get; private set; }
    public Vector2 enterPos { get; private set; }
    public Vector2 exitPos { get; private set; }

    BulletSpawnDefinition pattern;
    Vector2 target;

    public float speed { get; private set; }

    new Rigidbody2D rigidbody;

    IEnumerator fixedUpdateRoutine;

    public static DropInOut Create(GameObject prefab, Vector2 startPos, Vector2 enterPos, Vector2 exitPos, float speed, BulletSpawnDefinition pattern, Vector2 target)
    {
        return Instantiate(prefab).GetComponent<DropInOut>().Init(startPos, enterPos, exitPos, speed, pattern, target);
    }

    private DropInOut Init(Vector2 startPos, Vector2 enterPos, Vector2 exitPos, float speed, BulletSpawnDefinition pattern, Vector2 target)
    {
        this.startPos = startPos;
        this.enterPos = enterPos;
        this.exitPos = exitPos;
        this.speed = speed;
        this.pattern = pattern;
        this.target = target;

        return this;
    }

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        fixedUpdateRoutine = FixedUpdateRoutine();

        transform.position = startPos;
    }

    private void FixedUpdate()
    {
        if (fixedUpdateRoutine != null && !fixedUpdateRoutine.MoveNext())
        {
            fixedUpdateRoutine = null;
        }
    }

    private IEnumerator FixedUpdateRoutine()
    {
        // Enter State
        while (Vector2.Distance(transform.position, enterPos) > 0.01f)
        {
            rigidbody.MoveTowards(enterPos, 10f);

            yield return null;
        }

        rigidbody.SetVelocity(Vector2.zero);

        // Spawn bullets
        var spawnTime = gameObject.GetComponent<Spawner>().Spawn(pattern, transform.position, Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg);

        float targetTime = Time.time + spawnTime + 0.1f;

        while(Time.time < targetTime)
        {
            yield return null;
        }

        while(Vector2.Distance(transform.position, exitPos) > 0.1f)
        {
            rigidbody.MoveTowards(exitPos, 10f);

            yield return null;
        }

        Destroy(gameObject);
    }
}