using UnityEngine;
using UnityEngine.AI;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    const string playerString = "Player";

    void Update() 
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);    
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(playerString))
        {
            OnPickUp();
            Destroy(gameObject);
        }
    }

    // class abstract 적용(자체 인스턴스화 X, 게임 오브젝트에 부착 X)
    protected abstract void OnPickUp();
}
