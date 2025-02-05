using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;

    // GameObject[] chunks = new GameObject[12];
    List<GameObject> chunks = new List<GameObject>();
    void Start()
    {
        spawnChunks();
    }

    void Update()
    {
        moveChunks();
    }

    void spawnChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            float spawnPositionZ = CalculateSpawnPositionZ(i);
            Debug.Log($"-------------{spawnPositionZ}");

            Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
            GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);
            
            chunks[i] = newChunk;
        }
    }

    float CalculateSpawnPositionZ(int i)
    {
        float spawnPositionZ;

        if (i == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = transform.position.z + (i * chunkLength);
        }

        return spawnPositionZ;
    }
    void moveChunks()
    {

        for (int i = 0; i < chunks.Count; i++)
        {
            // Unity에서 float끼리 곱하면 성능이 느려짐 - () 추가가
            chunks[i].transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
        }
    }
}
