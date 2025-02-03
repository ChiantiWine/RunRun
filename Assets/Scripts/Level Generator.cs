using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;

    void Start() 
    {
        Instantiate(chunkPrefab, transform.position, Quaternion.identity);    
    }
}
