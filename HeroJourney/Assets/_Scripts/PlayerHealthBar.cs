using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    void Start()
    {
        healthBar.maxValue = GetComponent<PlayerCombat>().health;
        healthBar.value = GetComponent<PlayerCombat>().health;
    }

    void Update()
    {
        healthBar.value = GetComponent<PlayerCombat>().currentHealth;
    }
}
