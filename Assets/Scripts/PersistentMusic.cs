using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}