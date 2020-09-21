using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensions
{
    public static void SetVelocity(this Rigidbody2D rigidbody, Vector2 newVelocity)
    {
        Vector2 oldVelocity = rigidbody.velocity;
        Vector2 delta = newVelocity - oldVelocity;

        rigidbody.AddForce(delta * rigidbody.mass, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Move the rigidbody to the target, making sure not to overshoot.
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <param name="targetPosition"></param>
    public static void MoveTowards(this Rigidbody2D rigidbody, Vector2 targetPosition, float speed)
    {
        Vector2 position = rigidbody.transform.position;
        Vector2 delta = targetPosition - position;

        float mag = delta.magnitude;
        if (mag < speed * Time.fixedDeltaTime)
        {
            rigidbody.SetVelocity(delta / Time.fixedDeltaTime);
        }
        else
        {
            rigidbody.SetVelocity(delta.normalized * speed);
        }
    }
}