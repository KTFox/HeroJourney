using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] Transform attackPoint;

    [Header("Player info")]
    public float health = 100f;
    [SerializeField] float moveSpeed = 500f;
    [SerializeField] float jumpForce = 850f;
    [SerializeField] float climbSpeed = 400f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackRange = 0.7f;
    [SerializeField] float attackDelay = 0.5f;

    private float defaultGravity;
    private bool facingRight;
    private bool isAttacking;
    private Vector2 moveInput;
    private BoxCollider2D foot;
    private Rigidbody2D rb;
    private Animator animator;

    [HideInInspector] public bool isDead;
    [HideInInspector] public float currentHealth;

    void Awake()
    {
        foot = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        defaultGravity = rb.gravityScale;
        currentHealth = health;
    }

    void FixedUpdate()
    {
        if (!isDead) //Check player is still living
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed * Time.deltaTime, rb.velocity.y); //Player horizontal moving

            // Player ladder climbing
            if (foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed * Time.deltaTime);
            }
            else { rb.gravityScale = defaultGravity; }

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
        else { animator.SetTrigger("dead"); }
    }

    void OnMove(InputValue value)
    {
        moveInput.x = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        if (!isDead && foot.IsTouchingLayers(LayerMask.GetMask("Platform")) && !foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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
        if (!isAttacking && !isDead && !foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (value.isPressed)
            {
                Attack();
                Invoke(nameof(AttackComplete), attackDelay);
            }
        }
    }

    void Attack()
    {
        isAttacking = true;

        animator.SetTrigger("attack");
   
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PatrolEnemyBehaviour>().TakeDamage(attackDamage);
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    void FlipSprite()
    {
        Vector3 currentScale = transform.localScale;

        currentScale.x *= -1;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
