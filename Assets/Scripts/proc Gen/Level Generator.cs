using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
     [SerializeField] int startingChunksAmount = 12;

    List<GameObject> chunks = new List<GameObject>();
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
        moveSpeed += speedAmount;

        // 항상 최소 스피드 유지시킴킴
        if (moveSpeed < minMoveSpeed)
        {
            moveSpeed = minMoveSpeed;
        }
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount ); // 속도에 따라 물리 법칙 적용

            cameraController.ChangeCameraFOV(speedAmount);
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
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

        chunks.Add(newChunk);
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
