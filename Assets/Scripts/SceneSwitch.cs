using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public static SceneSwitch Instance { get; private set; }

    public string sceneName = "";
    public string Weapon = "";

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate Singletons
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeMessage(string what)
    {
        Weapon = what;
    }
}
