using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    new Collider2D collider;
    new Rigidbody2D rigidbody;

    /// <summary>
    /// The speed value of the character.
    /// Higher values will create faster characters that are more difficult to control.
    /// </summary>
    [Tooltip(
        "The speed value of the character."
        + "Higher values will create faster characters that are more difficult to control.")]
    public float speed = 2f;

    /// <summary>
    /// The acceleration value of the character. 
    /// High acceleration values (above 50) will make erratic feeling movement, like Fox in smash bros. 
    /// Low acceleration values (below 30) will make a character feel slippery, like Luigi in smash bros.
    /// </summary>
    [Tooltip(
        "The acceleration value of the character."
        + "High acceleration values (above 50) will make erratic feeling movement, like Fox in smash bros."
        + "Low acceleration values (below 30) will make a character feel slippery, like Luigi in smash bros.")]
    public float acceleration = 100f;

    public int health = 3;

    GameObject ship;

    private void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        if(collider == null)
        {
            throw new System.Exception("The player does not have a 2D collider attached.");
        }
        if (rigidbody == null)
        {
            throw new System.Exception("The player does not have a 2D rigidbody attached.");
        }

        ship = transform.Find("Ship").gameObject;
    }

    private void FixedUpdate()
    {
        Vector2 targetInput = GetInput();

        // Scale the input by the speed value to get the target velocity.
        Vector2 targetVelocity = targetInput * speed;

        ApplyAcceleration(targetVelocity);

        CheckHealth();

        if(targetInput.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, Quaternion.Euler(0f, 0f, 90f) * targetInput) * Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("Game Over");
        Destroy(gameObject);
    }

    private void ApplyAcceleration(Vector2 targetVelocity)
    {
        // Get the current velocity of the object
        Vector2 oldVelocity = rigidbody.velocity;

        // Calculate the new velocity
        Vector2 newVelocity = Vector2.MoveTowards(oldVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        // Calculate the change in momentum
        Vector2 deltaP = (newVelocity - oldVelocity) * rigidbody.mass;

        // Apply the change in momentum to the rigidbody
        // The velocity property can be set directly, but I've learned that applying a change in momentum instead avoids some bugs, but I don't remember why.
        // If it's important ask me (RJ). I have notes on it somewhere.
        rigidbody.AddForce(deltaP, ForceMode2D.Impulse);
    }

    private static Vector2 GetInput()
    {
        Vector2 targetInput = Vector2.zero;

        // Get the input from the players keyboard, and add each input to the input vector
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            targetInput.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            targetInput.x += 1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            targetInput.y -= 1f;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            targetInput.y += 1f;
        }

        // Normalize the input if it's magnitude in larget then 1.
        if (targetInput.sqrMagnitude > 1f)
        {
            targetInput = targetInput.normalized;
        }

        return targetInput;
    }

    /// <summary>
    /// This method gets called when the player collides with anything.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            OnProjectileHit(projectile);
        }
    }

    /// <summary>
    /// This method gets called when a projectile is hit.
    /// </summary>
    /// <param name="projectile"></param>
    private void OnProjectileHit(Projectile projectile)
    {
        health--;
        // Put projectile collision code here
        Debug.Log($"A projectile was hit {projectile.name}");
        MainCamera.Shake(1f);
    }
}
