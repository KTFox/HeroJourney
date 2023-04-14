using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("BasePlatform");
        PlayerPrefs.SetFloat("CurrentHealth", 100);
        PlayerPrefs.SetInt("EnemyKilled", 0);
    }

    public void QuitButton()
    {
        Application.Quit();
        PlayerPrefs.SetFloat("CurrentHealth", 100);
        PlayerPrefs.SetInt("EnemyKilled", 0);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("BasePlatform");
        PlayerPrefs.SetFloat("CurrentHealth", 100);
        PlayerPrefs.SetInt("EnemyKilled", 0);
    }
}
