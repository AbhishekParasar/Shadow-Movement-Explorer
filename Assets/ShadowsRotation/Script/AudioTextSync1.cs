using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AudioTextSync1 : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text displayText;
    public string fullText;
    public float timePerLetter = 0.05f;
    public float delayBetweenLines = 0.5f;
    public AudioClip audioClip;
    public Button actionButton;
    public bool useButton = true;  // TRUE => Button se band hoga, FALSE => Automatically band hoga

    private string[] lines;
    private int currentLine = 0;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentLine = 0;
        lines = fullText.Split(new[] { '\n' }, System.StringSplitOptions.None);

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }

        if (audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }

        if (lines.Length > 0)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayLine(lines[currentLine]));
        }

        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(false);
            actionButton.onClick.RemoveAllListeners();
            // actionButton.onClick.AddListener(() => CloseGameObject()); // Button click event
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        displayText.text = "";

        foreach (char letter in line)
        {
            displayText.text += letter;
            yield return new WaitForSeconds(timePerLetter);
        }

        yield return new WaitForSeconds(delayBetweenLines);

        currentLine++;
        if (currentLine < lines.Length)
        {
            StartCoroutine(DisplayLine(lines[currentLine]));
        }
        else
        {
            if (useButton)
            {
                if (actionButton != null)
                {
                    actionButton.gameObject.SetActive(true);
                }
            }
            else
            {
                CloseGameObject(); // Auto close if useButton is false
            }
        }
    }

    public void CloseGameObject()
    {
        gameObject.SetActive(false); // Band karna hai game object ko
    }

    private void OnDisable()
    {
        Reset();
    }

    private void OnEnable()
    {
        Initialize();
    }

    public void Reset()
    {
        StopAllCoroutines();
        displayText.text = "";
        currentLine = 0;

        if (audioSource.clip != null)
        {
            audioSource.Stop();
        }

        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(false);
        }
    }
}
