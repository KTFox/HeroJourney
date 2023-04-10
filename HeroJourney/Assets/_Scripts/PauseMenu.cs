using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPausing;
    public GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPausing)
            {
                Resume();
            }
            else { Pause(); }
        }
    }

    public void Pause()
    {
        isPausing = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        isPausing = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
