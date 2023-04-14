using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] float jumpForce = 20f;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Renderer>().enabled = true;

            AudioSystem.instance.PlaySound("SpikeTrap");

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);

            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(10);
        }
    }
}
