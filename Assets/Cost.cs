using UnityEngine;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    public int cost;
    public int bought = 0;
    public int need = 1;
    void Update()
    {
        if (bought >= need)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
