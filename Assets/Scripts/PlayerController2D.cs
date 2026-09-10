using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform spriteRoot; // assign GokuSprite transform

    private float moveInput;
    private bool isRunning;
    private bool isGrounded;

    void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1) Input
        moveInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        isRunning = Input.GetKey(KeyCode.LeftShift) ||
                    Input.GetKey(KeyCode.RightShift);

        // 2) Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, groundLayer);

        // 3) Flip sprite based on movement direction
        if (moveInput != 0 && spriteRoot != null)
        {
            Vector3 s = spriteRoot.localScale;
            s.x = Mathf.Abs(s.x) * (moveInput > 0 ? 1 : -1);
            spriteRoot.localScale = s;
        }

        // 4) Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            animator.SetBool("IsGrounded", false);
            animator.SetTrigger("Jump");
        }

        // 5) Animator params
        float speed01 = Mathf.Abs(moveInput) * (isRunning ? 1f : 0.5f);
        animator.SetFloat("Speed", speed01);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // Movement apply in physics step
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInput * targetSpeed, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}