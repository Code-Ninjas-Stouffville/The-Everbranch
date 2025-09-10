using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    [Header("Buying")]
    public int cost;
    public bool bought = false;
    public GameObject[] Upgrades;

    public GameManager Souls;
    void Update()
    {
        for (int i = 0; i < Upgrades.Length; i++) {
            if (Upgrades[i].GetComponent<Cost>().bought == false)
            {
                return;
            }
        }
        gameObject.GetComponent<Button>().interactable=true;
    }
    public void Buy()
    {
        if (GameManager.Instance.Souls >= cost && bought == false)
        {
            GameManager.Instance.Souls -= cost;
            gameObject.GetComponent<Cost>().bought = true;
        }
    }
}
