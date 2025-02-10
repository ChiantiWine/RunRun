using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCoolDown = 1f;
    [SerializeField] float adjustChangeMoveSpeedAmount = -2f;

    const string hitString = "Hit";
    float coolDownTimer = 0f;

    LevelGenerator levelGenerator;

     void Start() 
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    void Update() 
    {
        coolDownTimer += Time.deltaTime;    
    }
    void OnCollisionEnter(Collision other) 
    {
        if (coolDownTimer < collisionCoolDown) return;

        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        animator.SetTrigger(hitString);
        coolDownTimer = 0f;
    }
}
