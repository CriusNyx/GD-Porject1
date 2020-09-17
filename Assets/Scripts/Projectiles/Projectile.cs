using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    new Collider2D collider;
    new Rigidbody2D rigidbody;

    public Vector2 velocity;

    public static int count = 0;

    private void Awake()
    {
        count++;
    }

    private void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (collider == null)
        {
            throw new System.Exception("The player does not have a 2D collider attached.");
        }
        if (rigidbody == null)
        {
            throw new System.Exception("The player does not have a 2D rigidbody attached.");
        }
    }

    private void FixedUpdate()
    {
        // Calculate the delta momentum to acheive the target velocity
        Vector2 oldVelocity = rigidbody.velocity;
        Vector2 deltaP = (velocity - oldVelocity) * rigidbody.mass;

        // Apply the Delta P to the rigidbody.
        rigidbody.AddForce(deltaP, ForceMode2D.Impulse);

        if(transform.position.magnitude > 40f)
        {
            Destroy(gameObject);
            count--;
        }
    }
}
