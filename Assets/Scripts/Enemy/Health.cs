using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    Boss boss;

    public void Start()
    {
        boss = gameObject.GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>() != null)
        {
            health--;
            Destroy(collision.gameObject);

            if(health == 0)
            {
                Destroy(gameObject);
            }
            foreach(var system in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                system.Emit(1);
            }
            boss.Shake(1f);
        }
    }
}
