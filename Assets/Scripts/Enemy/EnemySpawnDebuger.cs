using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDebuger : MonoBehaviour
{
    public enum DebugType
    {
        DropInOut
    }

    public GameObject dropInOutPrefab;

    public DebugType debugType;

    public void Start()
    {
        switch (debugType)
        {
            case DebugType.DropInOut:
                DropInOutTest();
                break;
        }
    }

    private void DropInOutTest()
    {
        DropInSpawn2FansLeave(transform.position, transform.position + Vector3.right * 10f, Vector2.down * 10f, Vector2.left, Vector2.right);
        DropInSpawn2FansLeave(transform.position + Vector3.right * 10f + Vector3.down * 30f, transform.position + Vector3.down * 30f, Vector3.up * 10f, Vector2.right, Vector2.left);
    }

    private void DropInSpawn2FansLeave(Vector2 posA, Vector2 posB, Vector2 moveDelta, Vector2 targetDeltaA, Vector2 targetDeltaB)
    {
        BulletSpawnDefinition clockwiseFan = new FanSpawn(new FanSpawn(new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, 2f, 0f)), 3, 60, 0f, center: false), 3, 30, 0.2f, center: false);
        BulletSpawnDefinition counterClockwiseFan = new FanSpawn(new FanSpawn(new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, 2f, 0f)), 3, -60, 0f, center: false), 3, -30, 0.2f, center: false);

        Vector2 AMovePos = posA + moveDelta;
        Vector2 BMovePos = posB + moveDelta;

        Vector2 ATargetPos = AMovePos + targetDeltaA;
        Vector2 BTargetPos = BMovePos + targetDeltaB;

        DropInOut.Create(dropInOutPrefab, posA, AMovePos, posA, 10f, clockwiseFan, ATargetPos);
        DropInOut.Create(dropInOutPrefab, posB, BMovePos, posB, 10f, counterClockwiseFan, BTargetPos);
    }
}