using UnityEngine;

public class SaveAreaScript : MonoBehaviour
{
    private void Awake()
    {
        UpdateArea();
    }

    private void UpdateArea()
    {
        Rect safeArea = Screen.safeArea;
        RectTransform areaRectTransform = GetComponent<RectTransform>();

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        areaRectTransform.anchorMin = anchorMin;
        areaRectTransform.anchorMax = anchorMax;
    }
}
