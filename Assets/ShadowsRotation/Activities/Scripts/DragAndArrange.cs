using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndArrange : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject DropAreaParent;
    [SerializeField] private List<string> resultMsg = new List<string>();
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private AudioClip[] resultAudioClips;
    [SerializeField] private AudioSource resultAudioSource;
    [SerializeField] bool isQ1;

    private List<string> resultValidation = new List<string>();
    private List<string> resultValidationQ2 = new List<string>() { "Polar day", "Equator", "Summer", "Winter", "Polar night" };
    private List<string> resultValidationQ1 = new List<string>() { "Longer Shadow", "Medium Shadow", "Shorter Shadow" };

    private void Start()
    {
        if(isQ1)
            resultValidation = resultValidationQ1;
        else
            resultValidation = resultValidationQ2;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            dropped.transform.SetParent(transform);
            dropped.transform.localScale = Vector3.one;
            RectTransform droppedrt = dropped.GetComponent<RectTransform>();

            // Center anchor & pivot
            droppedrt.anchorMin = droppedrt.anchorMax = new Vector2(0.5f, 0.5f);
            droppedrt.pivot = new Vector2(0.5f, 0.5f);

            // Snap to center
            droppedrt.anchoredPosition = Vector2.zero;
            CheckResult();
        }
    }

    void CheckResult()
    {
        UIDragItem[] childs = DropAreaParent.GetComponentsInChildren<UIDragItem>();

        if (childs.Length < resultValidation.Count) return;

        for (int i = 0; i < childs.Length; i++)
        {
            if (!isValidResult(childs[i].gameObject, i))
            {
                result.text = resultMsg[1];
                PlayResultAudio(1);
                Reset();
                return;
            }
        }

        result.text = resultMsg[0];
        PlayResultAudio(0);
        nextButton.SetActive(true);
    }

    void PlayResultAudio(int i)
    {
        if (resultAudioClips.Length > 0)
        {
            resultAudioSource.clip = resultAudioClips[i];
            resultAudioSource.Play();
        }
    }

    void Reset()
    {
        UIDragItem[] childs = DropAreaParent.GetComponentsInChildren<UIDragItem>();
        foreach (UIDragItem dragItem in childs)
        {
            dragItem.Reset();
        }
    }

    bool isValidResult(GameObject g, int index)
    {
        if (resultValidation[index] == g.name)
        {
            return true;
        }

        return false;
    }
}
