using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    void Start()
    {
        healthBar.maxValue = GetComponent<Boss>().health;
        healthBar.value = GetComponent<Boss>().health;
    }

    void Update()
    {
        healthBar.value = GetComponent<Boss>().currentHealth;
    }
}
