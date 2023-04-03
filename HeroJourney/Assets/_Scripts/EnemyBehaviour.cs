using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] DetectArea detectArea; //Checking area whenever player entry

    [Header("Enemy info")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float attackRange = 1f; //Range that enemy can attack player

    private float distance; //Distance between player and enemy
    private GameObject target;
    private Transform currentPoint;



    void Start()
    {
        currentPoint = pointA;
    }

    void Update()
    {
        SetPatrolPoint();   
    }

    void FixedUpdate()
    {
        if (detectArea.target != null) //Check whenever player entry detect area
        {
            EnemyLogic();
        }
        else if (detectArea.target == null)
        {
            EnemyPatrolling();
        }       
    }

    void EnemyLogic()
    {
        target = detectArea.target;
        var targetPos = new Vector2(target.transform.position.x, transform.position.y); //Player position
        distance = Vector2.Distance(transform.position, target.transform.position); //Distance between player and enemy
        Vector3 rotation = transform.rotation.eulerAngles;

        // Chasing and attacking action when player in detecting area
        if (attackRange < distance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        // Flip sprite while chasing player
        if (transform.position.x < targetPos.x)
        {
            rotation.y = 0f;
        }
        else if(transform.position.x > targetPos.x)
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }

    void EnemyPatrolling()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

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

    void SetPatrolPoint()
    {
        float distance = Vector2.Distance(transform.position, currentPoint.position);

        if (currentPoint == pointA && distance < 0.5f)
        {
            currentPoint = pointB;
        }
        else if (currentPoint == pointB && distance < 0.5f)
        {
            currentPoint = pointA;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
        Gizmos.DrawRay(transform.position, Vector3.left * attackRange);
    }
}
