using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkPointChunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [Tooltip("chunk 프리팹의 크기가 변경된 경우가 아니면 chunk 길이 값을 변경하지 마십시오.")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] int checkPointChunkInterval = 8;

    List<GameObject> chunks = new List<GameObject>();
    int chunksSpawned = 0;

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        moveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        // 최대 최소 스피드 설정
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);
       
        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            // 중력 최대 최소 설정
            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ ); // 속도에 따라 물리 법칙 적용

            cameraController.ChangeCameraFOV(speedAmount);
        }
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunksSpawn();
        GameObject newChunkGo = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGo);
        Chunk newChunk = newChunkGo.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);

        chunksSpawned++;
    }

    private GameObject ChooseChunksSpawn()
    {
        GameObject chunkToSpawn;

        if (chunksSpawned % checkPointChunkInterval == 0 && chunksSpawned != 0)
        {
            chunkToSpawn = checkPointChunkPrefab;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;
       

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
           spawnPositionZ = chunks[chunks.Count -1].transform.position.z + chunkLength;
        }
        
        return spawnPositionZ;
    }
  
    // 캐릭터 화면이 X, 카메라 뒤로 넘어가면 Chunk 삭제 -> 캐릭터 화면에서는 Chunk가 그대로 보임
    void moveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {   // chunk 각 개별로 조절
            GameObject chunk = chunks[i];
            // Unity에서 float끼리 곱하면 성능이 느려짐 - () 추가가
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            // chunkLength를 빼는 이유 -> 플레이어 시야에서는 보여야 함
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                // List에서도 삭제 : 불필요한 오브젝트 삭제(Level Generator에서 Debug 모드에서 확인 가능능)
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
