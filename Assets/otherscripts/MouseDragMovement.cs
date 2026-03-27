using UnityEngine;

public class MouseDrag360Movement : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of rotation
    private Vector3 lastMousePosition;

    void Update()
    {
        // Detect when the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Store the current mouse position
            lastMousePosition = Input.mousePosition;
        }

        // Detect if the left mouse button is being held
        if (Input.GetMouseButton(0))
        {
            // Calculate the difference in mouse movement
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            // Rotate the object
            float horizontalRotation = deltaMousePosition.x * rotationSpeed * Time.deltaTime; // Left-right
            float verticalRotation = -deltaMousePosition.y * rotationSpeed * Time.deltaTime; // Up-down (inverted for natural feel)

            // Apply rotation to the object
            transform.Rotate(Vector3.up, horizontalRotation, Space.World); // Rotate around global Y-axis
            transform.Rotate(Vector3.right, verticalRotation, Space.Self); // Rotate around local X-axis

            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }
    }
}
