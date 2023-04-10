using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss info")]
    public float health = 300f;
    public float attackDamage = 25f;
    public float attackRange = 2f;
    public Vector3 attackOffset;
    private Transform player;

    [HideInInspector] public float currentHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        currentHealth = health;
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D hitPlayer = Physics2D.OverlapCircle(pos, attackRange, LayerMask.GetMask("Player"));

        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    public void LookAtPlayer()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        if (transform.position.x < player.position.x)
        {
            rotation.y = 0f;
        }
        else if (transform.position.x > player.position.x)
        {
            rotation.y = 180f;
        }
        transform.eulerAngles = rotation;
    }

    public void TakeDamage(float damgage)
    {
        currentHealth -= damgage;

        if (currentHealth > 0 && currentHealth <= 150)
        {
            GetComponent<Animator>().SetBool("hasEnraged", true);
        }
        else if (currentHealth <= 0)
        {
            GetComponent<Animator>().SetBool("hasDead", true);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
