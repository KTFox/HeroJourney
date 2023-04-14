using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI killText;

    void Start()
    {
        healthBar.maxValue = GameObject.Find("Player").GetComponent<PlayerCombat>().health;
        healthBar.value = PlayerPrefs.GetFloat("CurrentHealth");
    }

    void Update()
    {
        healthBar.value = PlayerPrefs.GetFloat("CurrentHealth");
        killText.text = "Kills: " + PlayerPrefs.GetInt("EnemyKilled");
    }
}
