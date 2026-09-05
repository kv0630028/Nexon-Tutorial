using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int enemyKillScore = 100;

    private int currentScore;

    public int EnemyKillScore => enemyKillScore;
    public int CurrentScore => currentScore;

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log($"Score: {currentScore}");
    }
}
