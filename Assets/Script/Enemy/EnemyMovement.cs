using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 1f;

    private Rigidbody2D enemyRb;
    private Transform playerTransform;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (playerTransform == null)
        {
            enemyRb.linearVelocity = Vector2.zero;
            return;
        }

        float distance = Vector2.Distance(playerTransform.position, enemyRb.position);

        if (distance <= stopDistance)
        {
            enemyRb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)playerTransform.position - enemyRb.position).normalized;
        enemyRb.linearVelocity = direction * moveSpeed;
    }
}
