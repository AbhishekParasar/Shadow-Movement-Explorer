// Updated CentralManager.cs
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CentralManager : MonoBehaviour
{
    public GameObject moleculePrefab;
    public int solidGridSize = 5;
    public int liquidMoleculeCount = 100;
    public int gasMoleculeCount = 50;

    public float temperature = 0; // Initial temperature
    public float solidToLiquidThreshold = 50f; // Temp at which solid becomes liquid
    public float liquidToGasThreshold = 100f; // Temp at which liquid becomes gas

    public Slider temperatureSlider; // Assign a UI slider
    public TMP_Text temperatureText; // Assign a UI text for display

    private List<GameObject> molecules = new List<GameObject>();
    private MatterState currentState = MatterState.Solid;

    public enum MatterState { Solid, Liquid, Gas }

    void Start()
    {
        // Initialize temperature slider
        temperatureSlider.minValue = 0;
        temperatureSlider.maxValue = 150;
        temperatureSlider.value = temperature;

        // Initialize molecules for reuse
        InitializeMolecules();
        SetState(MatterState.Solid);
    }

    void Update()
    {
        temperature = temperatureSlider.value;
        temperatureText.text = "Temperature: " + temperature.ToString("0") + "°C";
        HandleStateTransition();
    }

    void HandleStateTransition()
    {
        if (temperature < solidToLiquidThreshold && currentState != MatterState.Solid)
        {
            SetState(MatterState.Solid);
        }
        else if (temperature >= solidToLiquidThreshold && temperature < liquidToGasThreshold && currentState != MatterState.Liquid)
        {
            SetState(MatterState.Liquid);
        }
        else if (temperature >= liquidToGasThreshold && currentState != MatterState.Gas)
        {
            SetState(MatterState.Gas);
        }
    }

    void InitializeMolecules()
    {
        for (int i = 0; i < liquidMoleculeCount; i++)
        {
            GameObject molecule = Instantiate(moleculePrefab, Vector3.zero, Quaternion.identity);
            molecule.SetActive(false);
            molecules.Add(molecule);
        }
    }

    void SetState(MatterState newState)
    {
        StartCoroutine(SmoothTransition(newState));
    }

    IEnumerator SmoothTransition(MatterState newState)
    {
        float transitionDuration = 2f;
        float elapsedTime = 0;

        if (currentState == MatterState.Solid && newState == MatterState.Liquid)
        {
            // Expand the grid to a scattered liquid arrangement
            foreach (GameObject molecule in molecules)
            {
                molecule.SetActive(true);
            }
        }
        else if (currentState == MatterState.Liquid && newState == MatterState.Gas)
        {
            // Scatter molecules further for the gas state
        }
        else if (newState == MatterState.Solid)
        {
            // Re-align molecules into a solid grid
        }

        currentState = newState;
        yield return null;
    }
}
