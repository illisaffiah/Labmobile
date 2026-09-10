using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Bullet")]
    public float speed = 12f;
    public float lifeTime = 2.5f;
    public int damage = 1;

    [Header("Visual Facing")]
    public bool spriteDefaultFacingRight = true;
    public float rotateOffsetZ = 0f; // adjust if sprite looks sideways (90 or -90 degrees)

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction)
    {
        direction = direction.normalized;

        // Move
        rb.linearVelocity = direction * speed;

        // Rotate sprite to face movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (!spriteDefaultFacingRight) angle += 180f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotateOffsetZ);

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
                enemy.Die();

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
