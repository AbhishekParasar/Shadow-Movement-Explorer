using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioTextSynNew : MonoBehaviour
{
    public AudioSource audioSource;  // Audio source for playing the audio
    public TMP_Text displayText;     // Text UI element to display the lines
    public string fullText;          // Complete text to split into lines
    public float timePerLetter = 0.05f; // Time delay between each letter
    public float delayBetweenLines = 1f; // Time delay between lines
    public AudioClip audioClip;      // Audio clip to be played with the text

    private string[] lines;          // Array of lines from the text
    private int currentLine = 0;     // Index of the current line being displayed
    public Button actionButton;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Reset current line index
        currentLine = 0;

        // Split the full text into lines
        lines = fullText.Split(new[] { '\n' }, System.StringSplitOptions.None);

        // Set the audio clip if available
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }

        // Start playing the audio
        if (audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }

        // Start displaying the first line
        if (lines.Length > 0)
        {
            StopAllCoroutines(); // Ensure no ongoing coroutines interfere
            StartCoroutine(DisplayLine(lines[currentLine]));
        }

        // Disable the button until the process completes
        if (actionButton != null)
        {
            //actionButton.interactable = false;
            actionButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        displayText.text = ""; // Clear the display text

        // Display each letter with a delay
        foreach (char letter in line)
        {
            displayText.text += letter;
            yield return new WaitForSeconds(timePerLetter);
        }

        // Wait before showing the next line
        yield return new WaitForSeconds(delayBetweenLines);

        currentLine++; // Move to the next line
        if (currentLine < lines.Length)
        {
            StartCoroutine(DisplayLine(lines[currentLine]));
        }
        else
        {
            // Enable the button when all lines are complete
            if (actionButton != null)
            {
                //actionButton.interactable = true;
                actionButton.gameObject.SetActive(true);
            }
        }
    }

    public void Reset()
    {
        // Stop all coroutines to halt any ongoing text display process
        StopAllCoroutines();

        // Reset the display text
        displayText.text = "";

        // Reset the current line index
        currentLine = 0;

        // Stop audio playback
        if (audioSource.clip != null)
        {
            audioSource.Stop();
        }

        // Disable the button during reset
        if (actionButton != null)
        {
            // actionButton.interactable = false;
            actionButton.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Reset(); // Reset everything when the GameObject is disabled
    }

    private void OnEnable()
    {
        Initialize(); // Reinitialize everything when the GameObject is re-enabled
    }
}
