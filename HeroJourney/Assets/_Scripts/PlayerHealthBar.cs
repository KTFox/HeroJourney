using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    void Start()
    {
        healthBar.maxValue = GetComponent<PlayerController>().health;
        healthBar.value = GetComponent<PlayerController>().health;
    }

    void Update()
    {
        healthBar.value = GetComponent<PlayerController>().currentHealth;
    }
}
