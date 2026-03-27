using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Clips")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Audio Source Settings")]
    public AudioSource audioSource;

    private void Start()
    {
        // Ensure an AudioSource is assigned or attached to the GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
