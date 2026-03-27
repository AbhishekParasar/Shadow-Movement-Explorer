using UnityEngine;
using System.Collections.Generic;

public class SolidState : MonoBehaviour
{
    public GameObject moleculePrefab;
    public int gridSize = 5;
    public float spacing = 2f;
    public float vibrationIntensity = 0.1f;
    public float vibrationSpeed = 2.0f;

    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Vector3> vibrationDirections = new List<Vector3>();
    private List<float> jitterTimers = new List<float>();

    void OnEnable()
    {
        CreateMatrix();
    }

    void OnDisable()
    {
        // Clean up previous molecules
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        initialPositions.Clear();
        vibrationDirections.Clear();
        jitterTimers.Clear();
    }

    void Update()
    {
        UpdateVibrationDirections();
        VibrateMolecules();
    }

    public void CreateMatrix()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 position = new Vector3(x, y, z) * spacing;
                    GameObject molecule = Instantiate(moleculePrefab, position, Quaternion.identity, transform);
                    initialPositions.Add(molecule.transform.localPosition);

                    // Assign initial random vibration direction and jitter timer
                    vibrationDirections.Add(Random.insideUnitSphere.normalized);
                    jitterTimers.Add(Random.Range(0.1f, 0.5f)); // Randomize initial update intervals
                }
            }
        }
    }

    void UpdateVibrationDirections()
    {
        for (int i = 0; i < vibrationDirections.Count; i++)
        {
            jitterTimers[i] -= Time.deltaTime;

            // Randomly update the vibration direction when the timer reaches zero
            if (jitterTimers[i] <= 0)
            {
                vibrationDirections[i] = Random.insideUnitSphere.normalized;
                jitterTimers[i] = Random.Range(0.1f, 0.5f); // Reset timer with a new random interval
            }
        }
    }

    void VibrateMolecules()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform molecule = transform.GetChild(i);
            Vector3 originalPosition = initialPositions[i];
            Vector3 vibrationDirection = vibrationDirections[i];

            // Apply jittery vibration with random intensity modulation
            float randomIntensity = vibrationIntensity * Random.Range(0.8f, 1.2f);
            molecule.localPosition = originalPosition + vibrationDirection * Random.Range(0.05f, randomIntensity);
        }
    }
}
