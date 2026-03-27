using UnityEngine;

public class AudioManagerQuestion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;
    public AudioClip audioClip;



    public void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();


    }
}
