using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;

    private float currentHp;

    void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0f)
        {
            Debug.Log("Player Died");
        }
    }
}
