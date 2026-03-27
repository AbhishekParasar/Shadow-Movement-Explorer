using UnityEngine;

public class MasterGameAudioManager : MonoBehaviour
{
    public bool isAudioPlayed;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   

    public void PlayAudio(AudioClip audioClip)
    {
        if(!isAudioPlayed)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            //isAudioPlayed = true;

        }
        


    }
    public void PlayAudioTransitions(AudioClip audioClip)
    {
      
       
            audioSource.clip = audioClip;
            audioSource.Play();
            //isAudioPlayed = true;
       



    }
}
