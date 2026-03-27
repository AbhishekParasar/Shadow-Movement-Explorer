using System.Collections.Generic;
using UnityEngine;

public class LiquidState : MonoBehaviour
{
    public GameObject moleculePrefab;
    public GameObject containerMesh; // Assign your container object with a MeshCollider
    public int moleculeCount = 100;
    public float maxConnectionDistance = 1f;
    public float cohesionForce = 1f;
    public float randomForceMagnitude = 3f;
    public float drag = 1f; // Damping effect

    private List<GameObject> molecules = new List<GameObject>();
    private Bounds containerBounds;

    void Start()
    {
        containerMesh = GameObject.FindGameObjectWithTag("container");

        containerBounds = containerMesh.GetComponent<MeshCollider>().bounds;
        CreateLiquid();
    }

    void FixedUpdate()
    {
        ApplyLiquidBehavior();
    }

    void CreateLiquid()
    {
        for (int i = 0; i < moleculeCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(containerBounds.min.x, containerBounds.max.x),
                Random.Range(containerBounds.min.y, containerBounds.max.y),
                Random.Range(containerBounds.min.z, containerBounds.max.z));

            GameObject molecule = Instantiate(moleculePrefab, randomPosition, Quaternion.identity, transform);
            Rigidbody rb = molecule.AddComponent<Rigidbody>();
            rb.mass = 0.1f;
            rb.linearDamping = drag;
            rb.useGravity = true;
            molecules.Add(molecule);
        }
    }

    void ApplyLiquidBehavior()
    {
        foreach (GameObject molA in molecules)
        {
            Rigidbody rbA = molA.GetComponent<Rigidbody>();

            // Apply cohesion force
            foreach (GameObject molB in molecules)
            {
                if (molA != molB && Vector3.Distance(molA.transform.position, molB.transform.position) < maxConnectionDistance)
                {
                    Vector3 direction = (molB.transform.position - molA.transform.position).normalized;
                    rbA.AddForce(direction * cohesionForce);
                }
            }

            // Apply random perturbation for motion
            Vector3 randomForce = new Vector3(
                Random.Range(-randomForceMagnitude, randomForceMagnitude),
                Random.Range(-randomForceMagnitude, randomForceMagnitude),
                Random.Range(-randomForceMagnitude, randomForceMagnitude));
            rbA.AddForce(randomForce);

            // Constrain molecules within the container
            ConstrainToContainer(rbA);
        }
    }

    void ConstrainToContainer(Rigidbody rb)
    {
        Vector3 pos = rb.transform.position;

        // Check if the molecule is outside the container bounds
        if (!containerBounds.Contains(pos))
        {
            Vector3 closestPoint = containerBounds.ClosestPoint(pos);
            Vector3 direction = (closestPoint - pos).normalized;
            rb.AddForce(direction * cohesionForce * 10f, ForceMode.VelocityChange); // Push molecule back in bounds
        }
    }
}
