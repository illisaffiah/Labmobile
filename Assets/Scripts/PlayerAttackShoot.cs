using UnityEngine;

public class PlayerAttackShoot : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public PlayerBullet bulletPrefab;
    public Animator animator;

    [Header("Audio")] // Add these two lines for sound!
    public AudioSource audioSource;
    public AudioClip shootSound;

    [Header("Controls")]
    public KeyCode attackKey = KeyCode.J;

    [Header("Shooting")]
    public float shootCooldown = 0.25f;

    [Header("Optional")]
    public bool aimToMouse = false;

    [Header("Facing Detection")]
    [Tooltip("If your player flips by localScale.x (recommended), tick this.")]
    public bool useLocalScaleFacing = true;

    private float nextShootTime;
    private SpriteRenderer sr;
    private float firePointOffSetX;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();

        if (firePoint != null)
            firePointOffSetX = Mathf.Abs(firePoint.localPosition.x);
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
            TryAttackShoot();
    }

    void TryAttackShoot()
    {
        if (Time.time < nextShootTime) return;
        if (firePoint == null || bulletPrefab == null) return;

        nextShootTime = Time.time + shootCooldown;

        // Play attack animation
        if (animator != null)
            animator.SetTrigger("Attack");

        // --- NEW: Play the sound effect! ---
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        // -----------------------------------

        // Spawn bullet
        PlayerBullet b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Fire
        Vector2 dir = GetShootDirection();
        b.Fire(dir);
    }

    Vector2 GetShootDirection()
    {
        // Aim to mouse (optional)
        if (aimToMouse && Camera.main != null)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mouse - firePoint.position);
        }

        // Side scroller: decide left/right
        bool facingRight = GetFacingRight();
        return facingRight ? Vector2.right : Vector2.left;
    }

    bool GetFacingRight()
    {
        if (useLocalScaleFacing)
        {
            return transform.localScale.x > 0;
        }
        else
        {
            if (sr != null)
                return !sr.flipX;

            return true;
        }
    }

    void LateUpdate()
    {
        if (firePoint == null) return;

        bool facingRight = GetFacingRight();

        Vector3 lp = firePoint.localPosition;
        lp.x = facingRight ? firePointOffSetX : -firePointOffSetX;
        firePoint.localPosition = lp;
    }
}