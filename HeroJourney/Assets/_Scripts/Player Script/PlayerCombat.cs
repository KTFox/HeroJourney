using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] Transform attackPoint;

    [Header("Player info")]
    public float health = 100f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackRange = 0.7f;

    private bool isAttacking;
    private bool beAttacked;
    private Collider2D foot;
    private Animator animator;

    [HideInInspector] public float currentHealth;

    void Awake()
    {
        animator = GetComponent<Animator>();
        foot = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        currentHealth = health;
    }

    void Update()
    {
        CheckAnimationState();
    }

    void OnHit(InputValue value)
    {
        if (!beAttacked && !isAttacking && !foot.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (value.isPressed)
            {
                animator.SetTrigger("attack");
            }
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<PatrolEnemy>() != null)
            {
                enemy.GetComponent<PatrolEnemy>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponent<Boss>() != null)
            {
                enemy.GetComponent<Boss>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("beAttacked");
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }

    void CheckAnimationState()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Being attacekd state
        if (stateInfo.IsName("Player_BeAttacked") && stateInfo.normalizedTime < 1.0f)
        {
            beAttacked = true;
        }
        else { beAttacked = false; }

        // Attacking state
        if (stateInfo.IsName("Player_Attack") && stateInfo.normalizedTime < 1.0f)
        {
            isAttacking = true;
        }
        else { isAttacking = false; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
