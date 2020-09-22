using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;

    Boss boss;

    public static int phase2Count = 0;

    public void Start()
    {
        boss = gameObject.GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var projectile = collision.GetComponent<Projectile>();

        if (projectile != null)
        {
            health--;
            Destroy(collision.gameObject);

            if (health == 0)
            {
                Destroy(gameObject);
                if (boss != null)
                {
                    if (Difficulty.currentDifficultyLevel == Difficulty.DifficultyLevel.VeryHard)
                    {
                        if (!boss.isPhase2)
                        {
                            phase2Count = 2;

                            var bossPrefab = Resources.Load<GameObject>("Prefabs/Boss");
                            for (int i = 0; i < 2; i++)
                            {
                                var newBoss = GameObject.Instantiate(bossPrefab);
                                var newBossObject = newBoss.GetComponent<Boss>();
                                newBossObject.isPhase2 = true;
                                newBossObject.player = boss.player;
                            }

                            var phase2Dio = Resources.Load<GameObject>("Prefabs/Phase2");
                            GameObject.Instantiate(phase2Dio);
                        }
                        else
                        {
                            phase2Count--;
                            if (phase2Count == 0)
                            {
                                Win();
                            }
                        }
                    }
                    else
                    {
                        Win();
                    }
                }
            }
            foreach (var system in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                system.Emit(1);
            }
            boss.Shake(1f);

            projectile.Explode();
        }
    }

    private void Win()
    {
        Debug.Log("Win");
       
        SceneManager.LoadScene("WinMenu");
    }



}
