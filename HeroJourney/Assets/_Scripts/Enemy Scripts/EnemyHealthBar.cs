using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Vector3 offset;

    void Start()
    {
        healthBar.maxValue = GetComponent<PatrollingBehaviour>().health;
        healthBar.value = GetComponent<PatrollingBehaviour>().health;
    }

    void Update()
    {
        healthBar.value = GetComponent<PatrollingBehaviour>().currentHealth;
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
