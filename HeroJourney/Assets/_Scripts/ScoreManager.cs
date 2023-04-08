using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int enemyKilled;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void increasePointKill()
    {
        enemyKilled++;
        print(enemyKilled);
    }
}
