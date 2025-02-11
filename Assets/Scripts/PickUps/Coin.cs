using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] int scoreAmount = 10;
    ScoreManager scoreManager;
    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;      
    }
    protected override void OnPickUp()
    {
        scoreManager.IncreaseScore(scoreAmount);
    }
}
