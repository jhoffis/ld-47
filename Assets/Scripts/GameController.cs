using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // I am somehow the instance
        }
    }
}