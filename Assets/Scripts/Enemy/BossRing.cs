using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRing : MonoBehaviour
{
    public GameObject pointPrefab;
    GameObject[] ringObjects;

    public float ecc = 2.2f;
    public float simSpeed = 0.5f;
    public float spinSpeed = 30f;
    public float baseSpeed = 1f;

    public float localTime1;
    public float localTime2;

    public void Start()
    {
        int l = 12;
        ringObjects = new GameObject[l];
        for(int i = 0; i < l; i++)
        {
            ringObjects[i] = Instantiate(pointPrefab);
            ringObjects[i].transform.parent = transform;
        }
    }

    public void Update()
    {
        simSpeed = Mathf.Sin(Time.time * baseSpeed) * 0.5f;

        int l = ringObjects.Length;
        for(int i = 0; i < l; i++)
        {
            float t1 = (localTime1 + ((float)i / l));
            float pi2_2 = ecc * Mathf.PI;
            float t2 = (Mathf.Sin(2f * Mathf.PI * t1) + pi2_2 * t1) / (pi2_2);
            float t3 = t2 % 1f;
            float angle = t3 * 360f + localTime2;
            Vector3 localPos = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            Quaternion localRot = Quaternion.LookRotation(-localPos, Vector3.up);
            ringObjects[i].transform.localPosition = localPos;
            ringObjects[i].transform.localRotation = localRot;
        }

        localTime1 += Time.deltaTime * simSpeed * baseSpeed;
        localTime2 += Time.deltaTime * spinSpeed * baseSpeed;
    }
}