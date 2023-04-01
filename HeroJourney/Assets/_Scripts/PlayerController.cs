using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float jumpSpeed = 850f;
    [SerializeField] float climbSpeed;

    private float defaultGravity;
    private Vector2 moveInput;
    private BoxCollider2D foot;
    private CapsuleCollider2D body;
    private Rigidbody2D rb;

    void Awake()
    {
        foot = gameObject.GetComponent<BoxCollider2D>();
        body = gameObject.GetComponent<CapsuleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        defaultGravity = rb.gravityScale;
    }

    void Update()
    {
        Move();
        Climb();
    }

    #region Movement
    void OnMove(InputValue value)
    {
        moveInput.x = value.Get<float>();
    }

    void Move()
    {
        transform.position += new Vector3(moveInput.x * moveSpeed * Time.deltaTime, 0, 0);
    }

    void OnJump(InputValue value)
    {
        if (foot.IsTouchingLayers(LayerMask.GetMask("Platform")) && !body.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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

    void Climb()
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 0;

            transform.position += new Vector3(0, moveInput.y * climbSpeed * Time.deltaTime, 0);
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }
    }
    #endregion
}
