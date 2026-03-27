using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject bioticDropArea;
    [SerializeField] GameObject abioticDropArea;
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] private List<string> resultMsg = new List<string>();
    [SerializeField] private GameObject nextButton;
    [SerializeField] private AudioClip[] resultAudioClips;
    [SerializeField] private AudioSource resultAudioSource;

    private List<string> bioticValidation = new List<string>() { "Plants", "Rabbit", "Wolf", "Mushroom" };
    private List<string> abioticValidation = new List<string>() { "Sunlight", "Water", "CO2", "Soil" };

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            dropped.transform.SetParent(transform);
            dropped.transform.localScale = Vector3.one;
            CheckResult();
        }

        void CheckResult()
        {
            UIDragItem[] bioticChilds = bioticDropArea.GetComponentsInChildren<UIDragItem>();
            UIDragItem[] abioticChilds = abioticDropArea.GetComponentsInChildren<UIDragItem>();
            int count = bioticChilds.Length + abioticChilds.Length;
            if (count < 8) return;

            if(bioticChilds.Length>4 || abioticChilds.Length> 4)
            {
                result.text = resultMsg[1];
                PlayResultAudio(1);
                Reset();
            }
            else
            {
               if(isValidResult())
                {
                    result.text = resultMsg[0];
                    PlayResultAudio(0);
                    nextButton.SetActive(true);
                }
               else
                {
                    result.text = resultMsg[1];
                    PlayResultAudio(1);
                    Reset();
                }
            }
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
            UIDragItem[] bioticChilds = bioticDropArea.GetComponentsInChildren<UIDragItem>();
            UIDragItem[] abioticChilds = abioticDropArea.GetComponentsInChildren<UIDragItem>();

            foreach (UIDragItem dragItem in bioticChilds)
            {
                dragItem.Reset();
            }

            foreach (UIDragItem dragItem in abioticChilds)
            {
                dragItem.Reset();
            }
        }

        bool isValidResult()
        {
            UIDragItem[] bioticChilds = bioticDropArea.GetComponentsInChildren<UIDragItem>();
            UIDragItem[] abioticChilds = abioticDropArea.GetComponentsInChildren<UIDragItem>();
            foreach (UIDragItem dragItem in bioticChilds)
            {
                if(!bioticValidation.Contains(dragItem.gameObject.name))
                {
                    return false;
                }
            }

            foreach (UIDragItem dragItem in abioticChilds)
            {
                if (!abioticValidation.Contains(dragItem.gameObject.name))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
