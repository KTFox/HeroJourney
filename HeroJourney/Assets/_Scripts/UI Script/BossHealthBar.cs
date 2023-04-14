using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    void Start()
    {
        healthBar.maxValue = GameObject.Find("Boss").GetComponent<Boss>().health;
        healthBar.value = GameObject.Find("Boss").GetComponent<Boss>().health;
    }

    void Update()
    {
        healthBar.value = GameObject.Find("Boss").GetComponent<Boss>().currentHealth; ;
    }
}
