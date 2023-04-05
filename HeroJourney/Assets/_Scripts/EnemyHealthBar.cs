using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Vector3 offset;

    void Start()
    {
        healthBar.maxValue = GetComponent<PatrolEnemyBehaviour>().health;
        healthBar.value = GetComponent<PatrolEnemyBehaviour>().health;
    }

    void Update()
    {
        healthBar.value = GetComponent<PatrolEnemyBehaviour>().currentHealth;
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
