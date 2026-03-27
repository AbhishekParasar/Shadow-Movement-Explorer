using UnityEngine;

public class SaptrashConstellation : MonoBehaviour
{
    public Transform[] stars; // Drag star GameObjects here in the Inspector
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Set the initial number of points (7 points in your case)
        lineRenderer.positionCount = stars.Length;

        // Set the positions for the first 7 points
        for (int i = 0; i < stars.Length; i++)
        {
            lineRenderer.SetPosition(i, stars[i].position);
        }

        // Now connect the last point (7th) to the 4th point (index 3)
        // Add 1 more point to the line
        lineRenderer.positionCount = stars.Length + 1;

        // Set the position for the new point (connect the 7th to the 4th)
        lineRenderer.SetPosition(stars.Length, stars[3].position); // Connect to the 4th point (index 3)
    }

    void Update()
    {
        // Optionally update the positions if stars move dynamically
        for (int i = 0; i < stars.Length; i++)
        {
            lineRenderer.SetPosition(i, stars[i].position);
        }

        // Ensure the 7th point connects to the 4th point
        lineRenderer.SetPosition(stars.Length, stars[3].position); // Connect to the 4th point (index 3)
    }
}
