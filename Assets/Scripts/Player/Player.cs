using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    new Collider2D collider;
    new Rigidbody2D rigidbody;

    int fixedFrameCount = 0;

    bool alive = true;

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

    public GameObject explosion;

    GameObject ship;

    GameObject Health1, Health2, Health3;

    Spawner spawner;
    BulletSpawnDefinition bullet;

    private float invincibleUntil = -1f;

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

        ship = transform.Find("Ship").gameObject;

        Health3 = GameObject.Find("Health3").gameObject;
        Health2 = GameObject.Find("Health2").gameObject;
        Health1 = GameObject.Find("Health1").gameObject;

        spawner = gameObject.GetComponent<Spawner>();
        bullet = new PatternSpawn(0f,
            new LineSpawn(SpawnPattern.Single(10f), 3, Vector2.left * 0.2f + Vector2.down * 0.1f, 0f),
            new LineSpawn(SpawnPattern.Single(10f), 3, Vector2.right * 0.2f + Vector2.down * 0.1f, 0f));
    }

    private void FixedUpdate()
    {
        if (!alive)
            return;

        Vector2 targetInput = GetInput();

        // Scale the input by the speed value to get the target velocity.
        Vector2 targetVelocity = targetInput * speed;

        ApplyAcceleration(targetVelocity);

        CheckHealth();

        if (targetInput.sqrMagnitude > 0.01f)
        {
            ship.transform.rotation = Quaternion.LookRotation(-Vector3.back, Quaternion.Euler(0f, 0f, 90f) * targetInput) * Quaternion.Euler(0f, 180f, 0f);
        }

        if (Input.GetKey(KeyCode.Space) && (fixedFrameCount % 3 == 1 || fixedFrameCount % 2 == 1))
        {
            spawner.Spawn(bullet, transform.position, 90f);
        }

        fixedFrameCount++;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("Game Over");

        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        // Creates explosion
        Instantiate(explosion, pos, transform.rotation);

        alive = false;
        foreach(var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            Destroy(renderer);
        }

        StartCoroutine(LoadGameOverScreen());
    }

    private IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("gameOverMenu");
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
        if (projectile != null && Time.time > invincibleUntil)
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

        invincibleUntil = Time.time + 1f;

        SetHealthIcons();

        // Put projectile collision code here
        MainCamera.Shake(1f);

        // Destroys projectile and creates explosion
        Vector3 pos = new Vector3(projectile.transform.position.x, projectile.transform.position.y, projectile.transform.position.z);

        Destroy(projectile.gameObject);
        //Instantiate(explosion, pos, transform.rotation);

        projectile.Explode();
    }

    private void SetHealthIcons()
    {
        if (health == 3)
        {
            Health3.SetActive(true);
            Health2.SetActive(true);
            Health1.SetActive(true);
        }
        if (health == 2)
        {
            Health3.SetActive(false);
            Health2.SetActive(true);
            Health1.SetActive(true);
        }
        if (health == 1)
        {
            Health3.SetActive(false);
            Health2.SetActive(false);
            Health1.SetActive(true);

        }
        if (health == 0)
        {
            Health3.SetActive(false);
            Health2.SetActive(false);
            Health1.SetActive(false);
        }
    }
}
