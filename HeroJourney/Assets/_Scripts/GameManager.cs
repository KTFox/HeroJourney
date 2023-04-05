using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
