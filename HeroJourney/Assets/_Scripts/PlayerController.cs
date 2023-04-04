using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player info")]
    [SerializeField] float moveSpeed = 500f;
    [SerializeField] float jumpForce = 850f;
    [SerializeField] float climbSpeed = 400f;
    [SerializeField] float attackDelay = 0.5f;

    private float defaultGravity;
    private bool facingRight;
    private bool isAttacking;
    private Vector2 moveInput;
    private BoxCollider2D foot;
    private Rigidbody2D rb;
    private Animator animator;
  
    void Awake()
    {
        foot = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        defaultGravity = rb.gravityScale;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed * Time.deltaTime, rb.velocity.y); //Player horizontal moving

        // Player ladder climbing
        if (foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed * Time.deltaTime);
        }
        else { rb.gravityScale = defaultGravity; }

        // Deal damage to enemy

        // Flip 
        if (moveInput.x > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            FlipSprite();
        }

        // Change animation state
        if (foot.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            animator.SetFloat("speed", Mathf.Abs(moveInput.x));
            animator.SetBool("isJumping", false);
        }
        else if (!foot.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            animator.SetBool("isJumping", true);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput.x = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        if (foot.IsTouchingLayers(LayerMask.GetMask("Platform")) && !foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (value.isPressed)
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
    }

    void OnClimb(InputValue value)
    {
        moveInput.y = value.Get<float>();
    }

    void OnHit(InputValue value)
    {
        if (value.isPressed)
        {
            if (!isAttacking && !foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                isAttacking = true;
                animator.SetTrigger("attack");
                Invoke(nameof(AttackComplete), attackDelay);
            }
        }      
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void FlipSprite()
    {
        Vector3 currentScale = transform.localScale;

        currentScale.x *= -1;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }

}
