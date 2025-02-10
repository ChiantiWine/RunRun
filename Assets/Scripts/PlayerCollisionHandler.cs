using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    const string playerString = "Player";
    void OnCollisionEnter(Collision other) 
    {
        if ( other.CompareTag(playerString))
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
