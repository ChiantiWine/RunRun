using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCoolDown = 1f;

    const string hitString = "Hit";

    float coolDownTimer = 0f;

    void Update() 
    {
        coolDownTimer += Time.deltaTime;    
    }
    void OnCollisionEnter(Collision other) 
    {
        if (coolDownTimer < collisionCoolDown) return;

        animator.SetTrigger(hitString);
       
       coolDownTimer = 0f;
    }
}
