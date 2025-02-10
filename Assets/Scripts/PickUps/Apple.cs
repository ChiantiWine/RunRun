using UnityEngine;

public class Apple : PickUp
{
    [SerializeField] float adjustChangeMoveSpeedAmount = 3f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
    }
}
