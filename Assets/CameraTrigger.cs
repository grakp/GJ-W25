using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public Vector3 newCameraPosition;

    public enum CameraMovementDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public CameraMovementDirection cameraMovementDirection;

    private bool isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            SlideCamera();
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;  // Mark the player as outside the trigger zone
        }
    }

    void SlideCamera()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 slideAmount = Vector3.zero;

        // Determine the slide direction based on the movementDirection
        switch (cameraMovementDirection)
        {
            case CameraMovementDirection.LEFT:
                slideAmount = new Vector3(-cameraWidth, 0f, 0f);
                cameraMovementDirection = CameraMovementDirection.RIGHT;
                break;

            case CameraMovementDirection.RIGHT:
                slideAmount = new Vector3(cameraWidth, 0f, 0f);
                cameraMovementDirection = CameraMovementDirection.LEFT;
                break;

            case CameraMovementDirection.UP:
                slideAmount = new Vector3(0f, cameraHeight, 0f);
                cameraMovementDirection = CameraMovementDirection.DOWN;
                break;

            case CameraMovementDirection.DOWN:
                slideAmount = new Vector3(0f, -cameraHeight, 0f);
                cameraMovementDirection = CameraMovementDirection.UP;
                break;
        }

        Vector3 targetPosition = cameraController.transform.position + slideAmount;
        cameraController.SlideToPosition(targetPosition);
    }
}
