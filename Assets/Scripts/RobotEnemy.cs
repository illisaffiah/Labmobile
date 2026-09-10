using UnityEngine;

public class EnemyFlyChaseShoot : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform firePoint;
    public Bullet2D BulletPrefab;

    [Header("Chase Settings")]
    public float detectRange = 8f;
    public float stopRange = 2.5f;
    public float moveSpeed = 3f;

    [Header("Hover Settings")]
    public float hoverAmplitude = 0.3f;
    public float hoverFrequency = 2f;

    [Header("Shooting Settings")]
    public float shootRange = 7f;
    public float minShootInterval = 0.8f;
    public float maxShootInterval = 1.8f;

    [Header("Facing Settings")]
    [Tooltip("Tick if sprite default facing RIGHT. Untick if sprite default facing LEFT.")]
    public bool spriteDefaultFacingRight = true;

    [Tooltip("FirePoint local X offset from center.")]
    public float firePointOffsetX = 0.4f;

    private Vector3 basePosition;
    private float nextShootTime;
    private SpriteRenderer sr;

    void Start()
    {
        basePosition = transform.position;
        sr = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (firePoint != null)
            firePointOffsetX = Mathf.Abs(firePoint.localPosition.x);

        ScheduleNextShot();
    }

    void Update()
    {
        if (player == null) return;

        Hover();
        HandleChase();
        HandleShooting();
        HandleFacing();
    }

    void Hover()
    {
        float offsetY = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = new Vector3(
            transform.position.x,
            basePosition.y + offsetY,
            transform.position.z
        );
    }

    void HandleChase()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectRange && distance > stopRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }
    }

    void HandleShooting()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= shootRange && Time.time >= nextShootTime)
        {
            ShootAtPlayer();
            ScheduleNextShot();
        }
    }

    void ShootAtPlayer()
    {
        if (BulletPrefab == null || firePoint == null) return;

        Bullet2D bullet = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = player.position - firePoint.position;
        bullet.Fire(direction);
    }

    void ScheduleNextShot()
    {
        nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }

    //============================
    // SIMPLE ADJUSTABLE FACING SYSTEM
    //============================

    void HandleFacing()
    {
        if (sr == null || player == null) return;

        bool playerOnRight = player.position.x > transform.position.x;

        // if sprite default facing RIGHT:
        // - Player right --> no flip
        // - Player left  --> flip
        // if sprite default facing LEFT:
        // - Player right --> flip
        // - Player left  --> no flip

        if (spriteDefaultFacingRight)
            sr.flipX = !playerOnRight;
        else
            sr.flipX = playerOnRight;

        // Move FirePoint to correct side
        if (firePoint != null)
        {
            Vector3 lp = firePoint.localPosition;
            lp.x = playerOnRight ? firePointOffsetX : -firePointOffsetX;
            firePoint.localPosition = lp;
        }
    }
}