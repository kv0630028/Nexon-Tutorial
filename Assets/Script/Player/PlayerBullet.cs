using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D bulletRb;

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        bulletRb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
