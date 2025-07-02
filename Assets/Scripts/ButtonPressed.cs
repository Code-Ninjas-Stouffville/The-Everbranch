using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPressed : MonoBehaviour
{
    public string sceneName = "";
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //test
    }
}
