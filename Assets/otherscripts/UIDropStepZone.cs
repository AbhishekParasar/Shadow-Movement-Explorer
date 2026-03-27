using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class UIDropStepZone : MonoBehaviour, IDropHandler
{
    public int stepIndex; // Slot step index
    public TMP_Text feedbackText;
    private Image bg;
    private Color defaultColor;

    void Start()
    {
        bg = GetComponent<Image>();
        defaultColor = bg.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        UIDragStepItem item = eventData.pointerDrag.GetComponent<UIDragStepItem>();
        if (item == null) return;

        if (item.correctStepIndex == stepIndex)
        {
            // Correct → snap inside slot
            item.transform.SetParent(transform, false); // child of slot
            FitToParent(item.GetComponent<RectTransform>());

            item.enabled = false;

            bg.color = Color.green;
            if (feedbackText != null)
                feedbackText.text = $"Step {stepIndex + 1} Correct ✔";

            Debug.Log($"{item.name} correctly dropped in step {stepIndex}");
        }
        else
        {
            // Incorrect → back to original parent
            item.ResetToOriginalParent();

            bg.color = defaultColor;
            if (feedbackText != null)
                feedbackText.text = $"Step {stepIndex + 1} Incorrect ❌";

            Debug.Log($"{item.name} incorrectly dropped in step {stepIndex}");
        }
    }

    // Make RectTransform perfectly fit parent
    void FitToParent(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.localScale = Vector3.one;
        rect.localPosition = Vector3.zero;
    }
}
