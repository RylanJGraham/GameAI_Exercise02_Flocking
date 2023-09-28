using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; // The target point the camera should rotate around
    public float rotationSpeed = 5.0f;

    private float mouseX;
    private float mouseY;
    private float sensitivity = 2.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    private void Update()
    {
        // Get mouse input
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity; // Invert Y-axis for more intuitive control

        // Clamp the vertical rotation to prevent looking too far up or down
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Rotate the camera based on mouse input
        transform.LookAt(target);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        // Check for user input to unlock the cursor (for testing)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
