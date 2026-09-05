using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;

    private float currentHp;

    public float CurrentHp => currentHp;
    public float MaxHp => maxHp;
    public bool IsDead => currentHp <= 0f;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead)
        {
            return;
        }

        currentHp = Mathf.Max(currentHp - damage, 0f);

        OnHealthChanged?.Invoke(currentHp, maxHp);

        if (IsDead)
        {
            OnDeath?.Invoke();
        }
    }
}