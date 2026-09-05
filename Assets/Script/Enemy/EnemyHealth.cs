using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;

    private float currentHp;
    private ScoreManager scoreManager;

    void Awake()
    {
        currentHp = maxHp;
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0f)
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreManager.EnemyKillScore);
            }
            Destroy(gameObject);
        }
    }
}
