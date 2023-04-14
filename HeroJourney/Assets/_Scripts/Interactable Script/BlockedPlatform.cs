using UnityEngine;
using UnityEngine.UI;

public class BlockedPlatform : MonoBehaviour
{
    [SerializeField] GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("EnemyKilled") < 7)
            {
                panel.SetActive(true);
            }
            else { Destroy(gameObject); }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(false);
        }
    }
}
