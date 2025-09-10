using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class PriceUI : MonoBehaviour
{
    public RectTransform imageRect;   // Tooltip panel
    public Canvas canvas;             // Canvas the tooltip lives on
    public Vector2 offset = new Vector2(15f, 15f); // ← Add offset control in Inspector

    private TMP_Text priceText;       // TMP text inside imageRect

    void Start()
    {
        priceText = imageRect.GetComponentInChildren<TMP_Text>();
        if (priceText == null)
            Debug.LogWarning("PriceUI: No TMP_Text found in imageRect or its children!");
    }

    void Update()
    {
        // Follow cursor (parent-space)
        Vector2 mousePosition = Input.mousePosition;
        RectTransform parent = imageRect.parent as RectTransform;
        if (parent != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parent,
                mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out Vector2 localPoint
            );

            // Apply offset so tooltip goes slightly top-right of the cursor
            imageRect.anchoredPosition = localPoint + offset;
        }

        // Hover check
        if (IsHoveringOverButton(out GameObject buttonObject))
        {
            imageRect.gameObject.SetActive(true);
            var cost = buttonObject.GetComponent<Cost>() ??
                       buttonObject.GetComponentInParent<Cost>();

            if (cost.bought == true)
                priceText.text = "Bought!";
            else if (cost != null)
                priceText.text = "Cost: $" + cost.cost.ToString();
            else
                priceText.text = "N/A";
        }
        else
        {
            imageRect.gameObject.SetActive(false);
            priceText.text = "";
        }
    }

    bool IsHoveringOverButton(out GameObject buttonObject)
    {
        buttonObject = null;
        if (EventSystem.current == null) return false;

        var pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<UnityEngine.EventSystems.RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<UnityEngine.UI.Button>(out _))
            {
                buttonObject = result.gameObject;
                return true;
            }
        }

        return false;
    }
}
