using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 5000f;
    public float arrowDamage;

    private GameObject player;
    private Rigidbody2D rb;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set direction of arrow
        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * arrowSpeed * Time.deltaTime;

        // Set rotation of arrow
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // Set arrow damage
        arrowDamage = GameObject.Find("BowBoneCharacter").GetComponent<BowBone>().attackDamage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(arrowDamage);
        }

        Destroy(gameObject);
    }
}
