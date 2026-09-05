using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform playerTransform;
    private PlayerHealth playerHealth;
    private float cooldownTimer;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (playerTransform == null || playerHealth == null)
        {
            return;
        }

        cooldownTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance <= attackRange && cooldownTimer <= 0f)
        {
            playerHealth.TakeDamage(attackDamage);
            cooldownTimer = attackCooldown;
        }
    }
}
