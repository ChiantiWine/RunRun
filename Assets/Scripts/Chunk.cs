using UnityEngine;

public class Chunk : MonoBehaviour
{
   [SerializeField] GameObject fencePrefab;
   [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f};

   void Start()
   {
        SpawnFance();
   }

   void SpawnFance()
   {
        int RandomLaneIndex = Random.Range(0, lanes.Length);
        Vector3 spawnPosition = new Vector3(lanes[RandomLaneIndex], transform.position.y, transform.position.z);
        Instantiate(fencePrefab, spawnPosition, Quaternion.identity);
   }
}
