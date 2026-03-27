using System.Collections.Generic;
using UnityEngine;

public class LineDrawerWithPoints : MonoBehaviour
{
    public List<Transform> points;
    private LineRenderer lineRenderer;

    void Start()
    {
       
        lineRenderer = GetComponent<LineRenderer>();

       
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.yellow; 
    }

    void Update()
    {
        if (points == null || points.Count < 2)
        {
            Debug.LogWarning("Need two point for showing the lines");
            return;
        }

       
        lineRenderer.positionCount = points.Count;

      
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
