using UnityEditor;
using UnityEngine;

public class ForceClosed : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        // Stop Play Mode when testing in the Editor
        EditorApplication.isPlaying = false;
#else
        // Quit the actual application in builds
        Application.Quit();
#endif
    }
}
