using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TemperatureManager : MonoBehaviour
{
    public Button increaseTemperature, decreaseTemperature;
    private AdvancedParticleSimulation advancedParticleSimulation;
    public Image thermostatfillimage;

    public bool isIncreasing = false;
    public bool isDecreasing = false;

    private float stepValue = 1.5f; // Temperature change per click
    private float tempChangeSpeed = 5f; // Speed at which the temperature changes per second

    private void Awake()
    {
        advancedParticleSimulation = FindAnyObjectByType<AdvancedParticleSimulation>();
        Debug.Log("AdvancedParticleSimulation initialized.");

        // Add EventTrigger components for press-and-hold functionality
        AddEventTrigger(increaseTemperature.gameObject, EventTriggerType.PointerDown, OnIncreaseButtonDown);
        AddEventTrigger(increaseTemperature.gameObject, EventTriggerType.PointerUp, OnIncreaseButtonUp);

        AddEventTrigger(decreaseTemperature.gameObject, EventTriggerType.PointerDown, OnDecreaseButtonDown);
        AddEventTrigger(decreaseTemperature.gameObject, EventTriggerType.PointerUp, OnDecreaseButtonUp);

        // Add EventTrigger components for click functionality
        AddEventTrigger(increaseTemperature.gameObject, EventTriggerType.PointerClick, OnIncreaseButtonClick);
        AddEventTrigger(decreaseTemperature.gameObject, EventTriggerType.PointerClick, OnDecreaseButtonClick);
    }

    private void Update()
    {
        // Handle press-and-hold functionality
        if (isIncreasing)
        {
            ChangeTemperature(tempChangeSpeed * Time.deltaTime);
        }

        if (isDecreasing)
        {
            ChangeTemperature(-tempChangeSpeed * Time.deltaTime);
        }
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    // Handle press-and-hold for increase button
    private void OnIncreaseButtonDown(BaseEventData data)
    {
        Debug.Log("Increase button pressed.");
        isIncreasing = true;
    }

    private void OnIncreaseButtonUp(BaseEventData data)
    {
        Debug.Log("Increase button released.");
        isIncreasing = false;
    }

    // Handle press-and-hold for decrease button
    private void OnDecreaseButtonDown(BaseEventData data)
    {
        Debug.Log("Decrease button pressed.");
        isDecreasing = true;
    }

    private void OnDecreaseButtonUp(BaseEventData data)
    {
        Debug.Log("Decrease button released.");
        isDecreasing = false;
    }

    // Handle single clicks for increase and decrease buttons
    private void OnIncreaseButtonClick(BaseEventData data)
    {
        Debug.Log("Increase button clicked.");
        ChangeTemperature(stepValue);
    }

    private void OnDecreaseButtonClick(BaseEventData data)
    {
        Debug.Log("Decrease button clicked.");
        ChangeTemperature(-stepValue);
    }

    private void ChangeTemperature(float delta)
    {
        advancedParticleSimulation.temperature += delta;
        advancedParticleSimulation.temperature = Mathf.Clamp(
            advancedParticleSimulation.temperature,
            -200f, // Minimum temp for argon simulation
            -150f  // Maximum temp for argon simulation
        );
        advancedParticleSimulation.UpdateStateByTemperature();
        UpdateTemperatureUI();

        Debug.Log($"Temperature changed to {advancedParticleSimulation.temperature}");
    }

    public void UpdateTemperatureUI()
    {
        // Round to one decimal place
        float roundedTemperature = Mathf.Round(advancedParticleSimulation.temperature * 1000f) / 1000f;

        thermostatfillimage.fillAmount = (roundedTemperature + 200f) / 50f; // Adjust for argon range
        Debug.Log($"Temperature updated to {roundedTemperature}, fill amount set to {thermostatfillimage.fillAmount}");
    }

    public void SetSolidMode()
    {
        Debug.Log("Setting Solid Mode.");
        advancedParticleSimulation.SetToSolid();
        advancedParticleSimulation.temperature = -189.7f;
        UpdateTemperatureUI();
    }

    public void SetLiquidMode()
    {
        Debug.Log("Setting Liquid Mode.");
        advancedParticleSimulation.currentState = AdvancedParticleSimulation.State.Liquid;
        advancedParticleSimulation.temperature = -185f;
        UpdateTemperatureUI();
    }

    public void SetGasMode()
    {
        Debug.Log("Setting Gas Mode.");
        advancedParticleSimulation.currentState = AdvancedParticleSimulation.State.Gas;
        advancedParticleSimulation.temperature = -184f;
        UpdateTemperatureUI();
    }
}
