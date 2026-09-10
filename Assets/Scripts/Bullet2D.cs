using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 1;

    [Header("Visual Facing")]
    public bool spriteDefaultFacingRight = true; // tick if original sprite is facing right direction
    public float rotateOffsetZ = 0f; // use if sprite direction 90 degree wrong

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

        // Rotate visual to match direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // If sprite default faces LEFT, rotate 180 degree
        if (!spriteDefaultFacingRight) angle += 180f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotateOffsetZ);

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponentInParent<PlayerHealth>();

        if (ph != null)
        {
            Debug.Log("Player Hit");
            ph.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}