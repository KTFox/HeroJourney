using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Renderer>().enabled = true;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000,1000));

            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(10);
        }
    }
}
