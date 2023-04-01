using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float jumpSpeed = 850f;
    [SerializeField] float climbSpeed = 4f;

    private float defaultGravity;
    private bool facingRight;
    private Vector2 moveInput;
    private BoxCollider2D foot;
    private Rigidbody2D rb;

    void Awake()
    {
        foot = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        defaultGravity = rb.gravityScale;
    }

    void Update()
    {
        // Move player
        transform.position += new Vector3(moveInput.x * moveSpeed * Time.deltaTime, 0, 0);

        // Player climbing
        if (foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            transform.position += new Vector3(0, moveInput.y * climbSpeed * Time.deltaTime, 0);
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }

        // Flip sprite when change direction move
        if (moveInput.x > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            FlipSprite();
        }
    }

    #region Movement input
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
                rb.AddForce(new Vector2(0, jumpSpeed));
            }
        }
    }

    void OnClimb(InputValue value)
    {
        moveInput.y = value.Get<float>();
    }
    #endregion

    void FlipSprite()
    {
        Vector3 currentScale = transform.localScale;

        currentScale.x *= -1;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
