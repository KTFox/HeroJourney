using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] EnemyInfo info;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] DetectArea detectArea; 

    [Header("Enemy info")]
    public float maxHealth;
    public float moveSpeed;
    public float attackDamage;
    public Vector3 attackOffset;
    public float attackRange; 

    [HideInInspector] public float currentHealth;
    [HideInInspector] public bool beAttacked;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public Animator animator;

    private Transform currentPoint;

    void Awake()
    {
        animator = GetComponent<Animator>();
        maxHealth = info.health;
    }

    void Start()
    {
        SetRandomPoint();

        currentHealth = maxHealth;
        moveSpeed = info.moveSpeed;
        attackDamage = info.damage;
        attackOffset = info.attackOffset;
        attackRange = info.attackRange;
    }

    void Update()
    {
        CheckAnimationState();
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
        if (attackRange < distance && !isAttacking && !beAttacked) 
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else if (attackRange >= distance && !isAttacking && !beAttacked)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("attack");
        }     
    }

    public virtual void Attack() 
    {
        //Attacking action
    }

    public virtual void CheckAnimationState()
    {
        // Check animation state
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("beAttacked");

        beAttacked = true;
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            ScoreManager.instance.increasePointKill();
        }
    }

    void EnemyPatrolling()
    {
        animator.SetBool("isWalking", true);

        // Set patrol point
        float distance = Vector2.Distance(transform.position, currentPoint.position);
        if (currentPoint == pointA && distance < 0.5f)
        {
            currentPoint = pointB;
        }
        else if (currentPoint == pointB && distance < 0.5f)
        {
            currentPoint = pointA;
        }

        // Enemy patrolling
        Vector3 rotation = transform.rotation.eulerAngles;
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
        transform.eulerAngles = rotation;
    }

    void SetRandomPoint()
    {
        int pointIndex = Random.Range(0, 2);

        if (pointIndex == 0)
        {
            currentPoint = pointA;
        }
        else { currentPoint = pointB; }
    }

    void OnDrawGizmos()
    {
        // Draw attack sphere
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);

        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
