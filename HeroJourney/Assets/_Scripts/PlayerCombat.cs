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
    [SerializeField] float attackDelay = 0.5f;

    private bool isAttacking;
    private bool beAttacked;
    private bool isDead;
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
        if (!beAttacked)
        {
            isAttacking = true;

            animator.SetTrigger("attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));

            foreach(Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<PatrollingBehaviour>() != null)
                {
                    enemy.GetComponent<PatrollingBehaviour>().TakeDamage(attackDamage);
                }
                else if (enemy.GetComponent<Boss>() != null)
                {
                    enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                }
            }
            
        }      
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
            SceneManager.LoadScene(3);
        }

        Invoke(nameof(BeNotAttack), 0.3f);
    }

    void BeNotAttack()
    {
        beAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
