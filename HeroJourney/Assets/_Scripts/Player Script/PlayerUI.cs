using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI killText;

    void Start()
    {
        healthBar.maxValue = GetComponent<PlayerCombat>().health;
        healthBar.value = GetComponent<PlayerCombat>().health;
    }

    void Update()
    {
        healthBar.value = GetComponent<PlayerCombat>().currentHealth;
        killText.text = "Kills: " + ScoreManager.instance.enemyKilled;
    }
}
