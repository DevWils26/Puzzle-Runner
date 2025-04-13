using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public Vector2Int mazeSize = new Vector2Int(0, 0);
    public Vector3 mazeOrigin = Vector3.zero;
    public int skeletonCount = 5;

    void Start()
    {
        for (int i = 0; i < skeletonCount; i++)
        {
            SpawnSkeleton();
        }
    }

    void SpawnSkeleton()
    {
        int x = Random.Range(0, mazeSize.x);
        int z = Random.Range(0, mazeSize.y);


        Vector3 spawnPos = new Vector3(
            mazeOrigin.x + x + 0.5f,
            mazeOrigin.y,
            mazeOrigin.z + z + 0.5f
        );

        Instantiate(skeletonPrefab, spawnPos, Quaternion.identity);
    }
}