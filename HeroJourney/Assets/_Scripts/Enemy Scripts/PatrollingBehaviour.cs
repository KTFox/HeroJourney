using UnityEngine;

public class PatrollingBehaviour : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] EnemyInfo info;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] DetectArea detectArea; //Checking area whenever player entry

    [Header("Enemy info")]
    public float health;
    [SerializeField] float moveSpeed;
    public float attackDamage;
    public Vector3 attackOffset;
    public float attackRange; //Range that enemy can attack player
    [SerializeField] float attackDelay; //Time between enemy attack actions

    [HideInInspector] public float currentHealth;

    private bool isAttacking;
    public bool beAttacked;
    private Animator animator;
    private Transform currentPoint;

    void Awake()
    {
        animator = GetComponent<Animator>();
        health = info.health;
    }

    void Start()
    {
        currentPoint = pointA;
        currentHealth = health;
        moveSpeed = info.moveSpeed;
        attackDamage = info.damage;
        attackOffset = info.attackOffset;
        attackRange = info.attackRange;
        attackDelay = info.attackDelay;
    }

    void FixedUpdate()
    {
        if (detectArea.target != null)
        {
            EnemyLogic();
        }
        else if (detectArea.target == null && !isAttacking)
        {
            EnemyPatrolling();
        }       
    }

    void EnemyLogic()
    {
        GameObject target = detectArea.target; //Player object
        var targetPos = new Vector2(target.transform.position.x, transform.position.y); //Player position
        float distance = Vector2.Distance(transform.position, targetPos) - 1f; //Distance between player and enemy

        // Flip sprite
        Vector3 rotation = transform.rotation.eulerAngles;
        if (!isAttacking)
        {
            if (transform.position.x < targetPos.x)
            {
                rotation.y = 0f;
            }
            else if (transform.position.x > targetPos.x)
            {
                rotation.y = 180f;
            }
            transform.eulerAngles = rotation;
        }

        // Chasing and attacking action
        if (attackRange < distance && !isAttacking) 
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else if (attackRange >= distance && !isAttacking)
        {
            isAttacking = true;

            animator.SetBool("isWalking", false);
            animator.SetTrigger("attack");

            Invoke(nameof(AttackComplete), attackDelay); //Set delay time for attack
        }     
    }

    public virtual void Attack() //This method will be called in animation event
    {
        //Attacking action
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("beAttacked");

        beAttacked = true;
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        Invoke(nameof(NotBeAttacked), 0.5f);
    }

    void NotBeAttacked()
    {
        beAttacked = false;
    }

    void EnemyPatrolling()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        float distance = Vector2.Distance(transform.position, currentPoint.position);

        animator.SetBool("isWalking", true);

        // Set patrol point
        if (currentPoint == pointA && distance < 0.5f)
        {
            currentPoint = pointB;
        }
        else if (currentPoint == pointB && distance < 0.5f)
        {
            currentPoint = pointA;
        }

        // Enemy patrolling
        if (currentPoint == pointA)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(pointA.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            rotation.y = 180f;
        }
        else if (currentPoint == pointB)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(pointB.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation; //Flip sprite
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawWireSphere(pos, attackRange);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
