using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class PriceUI : MonoBehaviour
{
    public RectTransform imageRect;   // Tooltip panel
    public Canvas canvas;             // Canvas the tooltip lives on

    private Text priceText;       // TMP text inside imageRect

    void Start()
    {
        priceText = imageRect.GetComponentInChildren<Text>();
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
            imageRect.anchoredPosition = localPoint;
        }

        // Hover check
        if (IsHoveringOverButton(out GameObject buttonObject))
        {
            // Get Weapon from the hovered button (or its parent)
            var cost = buttonObject.GetComponent<Cost>() ??
                         buttonObject.GetComponentInParent<Cost>();

            if (cost != null)
            {
                // If your field is 'cost' instead of 'Cost', change the next line accordingly.
                priceText.text = "Cost: $" + cost.cost.ToString();
            }
            else
            {
                priceText.text = "N/A";
            }
        }
        else
        {
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
            // Use uGUI Button explicitly to avoid ambiguity with UIElements.Button
            if (result.gameObject.TryGetComponent<UnityEngine.UI.Button>(out _))
            {
                buttonObject = result.gameObject;   // assign first
                return true;                        // then return
            }
        }

        return false;
    }
}
