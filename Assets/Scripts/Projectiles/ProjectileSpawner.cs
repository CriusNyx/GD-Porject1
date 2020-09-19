using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    int bulletCount = 0;
    public GameObject bullet;

    private void FixedUpdate()
    {
        float expectedBulletCount = 5f * Mathf.Pow(1.05f, Time.timeSinceLevelLoad);
        while(bulletCount < expectedBulletCount)
        {
            bulletCount++;
            if (Projectile.count <= 1000) 
            {
                float angle1 = Random.Range(0f, 360f);
                float angle2 = Random.Range(0f, 360f);
                Vector3 pos = Quaternion.Euler(0f, 0f, angle1) * Vector3.right * 20f;

                var newBullet = GameObject.Instantiate(bullet) as GameObject;
                newBullet.transform.position = pos;
                var projectile = newBullet.GetComponent<Projectile>();
                projectile.velocity = Quaternion.Euler(0f, 0f, angle2) * Vector3.right * 5f;
            }
        }
    }
}
