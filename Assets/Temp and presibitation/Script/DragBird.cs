using UnityEngine;

public class DragOnlyY : MonoBehaviour
{
    private float offsetY;
    private float initialX;
    private float initialZ;

    public float minYPosition = -5f;
    public float maxYPosition = 5f;

    void Start()
    {
        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    void OnMouseDown()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(transform.position).z));

        offsetY = transform.position.y - mouseWorld.y;
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(transform.position).z));

        float newY = mouseWorld.y + offsetY;
        newY = Mathf.Clamp(newY, minYPosition, maxYPosition);

        transform.position = new Vector3(initialX, newY, initialZ);
    }
}
