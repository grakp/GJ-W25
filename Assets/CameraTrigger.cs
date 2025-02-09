using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 targetCameraPosition;  // The target position for the camera

    private void Start()
    {
        targetCameraPosition = transform.position;
        targetCameraPosition.z = -10f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SlideCamera();
        }
    }

    private void SlideCamera()
    {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController != null)
        {
            cameraController.SlideToPosition(targetCameraPosition);
        }
    }
}
