using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] EnemyInfo info;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] DetectArea detectArea; //Checking area whenever player entry

    [Header("Enemy info")]
    [SerializeField] float health;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackDamage;
    [SerializeField] float attackRange; //Range that enemy can attack player
    [SerializeField] float attackDelay; //Time between enemy attack actions

    private bool isAttacking;
    private Animator animator;
    private Transform currentPoint;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentPoint = pointA;
        health = info.health;
        moveSpeed = info.moveSpeed;
        attackDamage = info.damage;
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
        float distance = Vector2.Distance(transform.position, targetPos) - 0.5f; //Distance between player and enemy

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
            Invoke(nameof(AttackComplete), attackDelay);
        }

        // Deal damge to player

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
    }

    void AttackComplete()
    {
        isAttacking = false;
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
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
