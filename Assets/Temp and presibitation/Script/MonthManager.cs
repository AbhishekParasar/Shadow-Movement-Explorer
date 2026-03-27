using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MonthManager : MonoBehaviour
{

    [System.Serializable]
    public class ButtonConfig
    {
        [Tooltip("The text displayed on the button.")]
        public string buttonName = "Custom Button";

        [Tooltip("The integer value that the button holds and prints on click.")]
        public int buttonValue = 0;
    }


    [Header("UI References")]
    [Tooltip("The generic button prefab (must have a TextMeshProUGUI child).")]
    [SerializeField]
    private Button buttonPrefab;

    [Tooltip("The parent object where buttons are instantiated (should have a GridLayoutGroup).")]
    [SerializeField]
    private Transform parentContainer;

    [Tooltip("The TextMeshPro component used for logging the button clicks.")]
    [SerializeField]
    private TextMeshProUGUI outputText;


    [Header("Button Configurations")]
    [Tooltip("Edit the names and values for the 12 buttons here.")]
    public List<ButtonConfig> buttonConfigs = new List<ButtonConfig>();

    private const int BUTTON_COUNT = 12;
    private bool buttonsGenerated = false;

    private List<GameObject> generatedButtons = new List<GameObject>();


    private void OnValidate()
    {
        while (buttonConfigs.Count < BUTTON_COUNT)
        {
            ButtonConfig newConfig = new ButtonConfig
            {
                buttonName = $"Button {buttonConfigs.Count + 1}",
                buttonValue = (buttonConfigs.Count + 1) * 10
            };
            buttonConfigs.Add(newConfig);
        }

        if (buttonConfigs.Count > BUTTON_COUNT)
        {
            buttonConfigs.RemoveRange(BUTTON_COUNT, buttonConfigs.Count - BUTTON_COUNT);
        }
    }

    private void Start()
    {
        // Calling the generator here ensures buttons appear as soon as the game starts.
        GenerateButtons();
    }

    public void GenerateButtons()
    {
        if (buttonsGenerated)
        {
            Debug.LogWarning("Buttons already generated. Clearing and regenerating.");
            ClearButtons();
        }

        if (buttonPrefab == null || parentContainer == null || outputText == null)
        {
            Debug.LogError("ButtonManager references are not fully assigned in the Inspector.");
            return;
        }

        for (int i = 0; i < BUTTON_COUNT; i++)
        {
            ButtonConfig config = buttonConfigs[i];

            Button newButton = Instantiate(buttonPrefab, parentContainer);
            generatedButtons.Add(newButton.gameObject);

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = config.buttonName;
            }
            else
            {
                Debug.LogError($"Button {i + 1} prefab is missing a TextMeshProUGUI child component.");
            }

            int index = i;
            newButton.onClick.AddListener(() => OnButtonClicked(buttonConfigs[index]));
        }

        buttonsGenerated = true;
        outputText.text = "Generated " + BUTTON_COUNT + " buttons!";
        Debug.Log("Successfully generated 12 buttons.");
    }

    private void ClearButtons()
    {
        foreach (var buttonGO in generatedButtons)
        {
            Destroy(buttonGO);
        }
        generatedButtons.Clear();
        buttonsGenerated = false;
    }

    private void OnButtonClicked(ButtonConfig config)
    {
        if (outputText != null)
        {
            outputText.text = $"Button Clicked: **{config.buttonName}**\nValue: **{config.buttonValue}**";
        }
        else
        {
            Debug.LogWarning($"Button Clicked: {config.buttonName}, Value: {config.buttonValue}. Output Text is not assigned.");
        }
    }
}