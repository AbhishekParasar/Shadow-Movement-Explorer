using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragStepItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int correctStepIndex; // Step number in sequence

    private RectTransform rect;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private Vector3 originalLocalScale;
    private Vector2 originalAnchorMin;
    private Vector2 originalAnchorMax;
    private Vector2 originalSizeDelta;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Save original transform info
        originalParent = transform.parent;
        originalLocalPosition = rect.localPosition;
        originalLocalScale = rect.localScale;
        originalAnchorMin = rect.anchorMin;
        originalAnchorMax = rect.anchorMax;
        originalSizeDelta = rect.sizeDelta;

        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform); // Move to top-level canvas for drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetToOriginalParent()
    {
        transform.SetParent(originalParent, false);
        rect.localPosition = originalLocalPosition;
        rect.localScale = originalLocalScale;
        rect.anchorMin = originalAnchorMin;
        rect.anchorMax = originalAnchorMax;
        rect.sizeDelta = originalSizeDelta;

        Debug.Log($"{gameObject.name} returned to original parent with correct stretch.");
    }
}
