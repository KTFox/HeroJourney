using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Vector3 offset;

    void Start()
    {
        healthBar.maxValue = GetComponent<PatrolEnemy>().maxHealth;
        healthBar.value = GetComponent<PatrolEnemy>().maxHealth;
    }

    void Update()
    {
        healthBar.value = GetComponent<PatrolEnemy>().currentHealth;
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
