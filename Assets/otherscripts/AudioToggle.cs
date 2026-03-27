using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    // Variable to track the current mute state
    private bool isMuted = false;

    public void ToggleMute()
    {
        // Find all AudioSource components in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Toggle the mute state for each AudioSource
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = !isMuted;
        }

        // Update the isMuted state
        isMuted = !isMuted;
    }
}
