using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the active instance and protect it from being destroyed on scene unloads
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

