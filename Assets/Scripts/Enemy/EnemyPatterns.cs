using UnityEngine;

public class EnemyPatterns : MonoBehaviour
{
    public GameObject dropInOutPrefab;

    private static BulletSpawnDefinition clockwiseFan = new FanSpawn(new FanSpawn(new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, 2f, 0f)), 3, 60, 0f, center: false), 3, 30, 0.2f, center: false);
    private static BulletSpawnDefinition counterClockwiseFan = new FanSpawn(new FanSpawn(new SingleSpawn(new BulletSpawnPos(Vector2.zero, 0f, 2f, 0f)), 3, -60, 0f, center: false), 3, -30, 0.2f, center: false);

    // Start is called before the first frame update
    void Start()
    {
        var pattern = new RepeatSpawn(SpawnPattern.InterleavedFan(15, 7, 0.5f, SpawnPattern.Single(2f)), 5, 1f);

        DropInOut.Create(dropInOutPrefab, transform.position, transform.position + Vector3.down * 10f, transform.position, 10, pattern, transform.position + Vector3.down * 15f);
    }

    public void DropInSpawnLeave(Vector2 posA, Vector2 posB, Vector2 moveDelta, Vector2 targetDeltaA, Vector2 targetDeltaB, BulletSpawnDefinition defA, BulletSpawnDefinition defB)
    {
        Vector2 AMovePos = posA + moveDelta;
        Vector2 BMovePos = posB + moveDelta;

        Vector2 ATargetPos = AMovePos + targetDeltaA;
        Vector2 BTargetPos = BMovePos + targetDeltaB;

        DropInOut.Create(dropInOutPrefab, posA, AMovePos, posA, 10f, defA, ATargetPos);
        DropInOut.Create(dropInOutPrefab, posB, BMovePos, posB, 10f, defB, BTargetPos);
    }

    public void MirrorDropInSpawnLeave(BulletSpawnDefinition defA, BulletSpawnDefinition defB, Vector2 targetAVec, Vector2 targetBVec, Vector2 targetCVec, Vector2 targetDVec)
    {
        DropInSpawnLeave(transform.position, transform.position + Vector3.right * 10f, Vector2.down * 10f, targetAVec, targetBVec, defA, defB);
        DropInSpawnLeave(transform.position + Vector3.right * 10f + Vector3.down * 30f, transform.position + Vector3.down * 30f, Vector3.up * 10f, targetCVec, targetDVec, defA, defB);
    }
}
