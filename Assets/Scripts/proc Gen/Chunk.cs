using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Chunk : MonoBehaviour
{
   [SerializeField] GameObject fencePrefab;
   [SerializeField] GameObject applePrefab;
   [SerializeField] GameObject coinPrefab;
   [SerializeField] float appleSpawnChance = .3f;
   [SerializeField] float coinSpawnChance = .5f;
   [SerializeField] float coinSeperationLength = 2f; // 동전 사이의 거리
   
   [SerializeField] float[] lanes = { -3f, 0f, 3f};

    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    List<int> availableLanes = new List<int> {0, 1, 2};
   void Start()
   {
        SpawnFances();
        SpawnApple();
        SpawnCoins();
   }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    // Fence가 랜덤으로 1개에서 2개 생성되게 하기기
    void SpawnFances()
   {
    int fenceTospawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fenceTospawn; i++)
        {
            if (availableLanes.Count <= 0) break;

            int selectedLane = SelectedLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }
    void SpawnApple()
   {    
        if(Random.value > appleSpawnChance || availableLanes.Count <= 0) return;
        int selectedLane = SelectedLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
   }

   void SpawnCoins()
   {
        if(Random.value > coinSpawnChance || availableLanes.Count <= 0) return;
        int selectedLane = SelectedLane();
    
        int maxCoinsTospawn = 6;
        int coinToSpawn = Random.Range(1, maxCoinsTospawn);

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f); // 동전 chunkZPos 최상단부터 생성성

        for (int i = 0; i < coinToSpawn; i++)
        {

            float spawnPostionZ = topOfChunkZPos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPostionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        } 
    

   }

    int SelectedLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

}
