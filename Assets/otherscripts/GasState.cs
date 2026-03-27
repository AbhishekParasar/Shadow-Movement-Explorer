using UnityEngine;

public class GasState : MonoBehaviour
{
    public GameObject moleculePrefab;
    public int moleculeCount = 50;
    public float containerSize = 10f;
    public float randomForce = 5f;

    void Start()
    {
        CreateGas();
    }

    void FixedUpdate()
    {
        MoveMolecules();
    }

    void CreateGas()
    {
        for (int i = 0; i < moleculeCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-containerSize, containerSize),
                Random.Range(-containerSize, containerSize),
                Random.Range(-containerSize, containerSize));

            GameObject molecule = Instantiate(moleculePrefab, randomPosition, Quaternion.identity, transform);
            molecule.AddComponent<Rigidbody>().useGravity = false;
        }
    }

    void MoveMolecules()
    {
        foreach (Transform molecule in transform)
        {
            Rigidbody rb = molecule.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-randomForce, randomForce),
                    Random.Range(-randomForce, randomForce),
                    Random.Range(-randomForce, randomForce));

                rb.AddForce(randomDirection);
            }
        }
    }
}
