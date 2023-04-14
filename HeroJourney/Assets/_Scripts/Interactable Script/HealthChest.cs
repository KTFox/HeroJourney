using UnityEngine;

public class HealthChest : MonoBehaviour
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

            AudioSystem.instance.PlaySound("ChestOpen");

            if (PlayerPrefs.GetFloat("CurrentHealth") + 50 <= collision.GetComponent<PlayerCombat>().health)
            {

                PlayerPrefs.SetFloat("CurrentHealth", PlayerPrefs.GetFloat("CurrentHealth") + 50);
            }
            else { PlayerPrefs.SetFloat("CurrentHealth", collision.GetComponent<PlayerCombat>().health); }
            
            hasOpened = true;
        }
    }
}
