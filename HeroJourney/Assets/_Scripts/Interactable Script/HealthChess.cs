using UnityEngine;

public class HealthChess : MonoBehaviour
{
    private Animator animator;
    private bool hasOpened;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasOpened)
        {
            animator.SetBool("open", true);

            if (collision.GetComponent<PlayerCombat>().currentHealth + 50 <= collision.GetComponent<PlayerCombat>().health)
            {

                collision.GetComponent<PlayerCombat>().currentHealth += 50;
            }
            else { collision.GetComponent<PlayerCombat>().currentHealth = collision.GetComponent<PlayerCombat>().health; }
            
            hasOpened = true;
        }
    }
}
