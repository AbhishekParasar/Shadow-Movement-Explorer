using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform); // move above UI
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // If not dropped on a valid area
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent);
        }
    }

    public void Reset()
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(originalParent);
    }
}
