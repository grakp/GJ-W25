using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 targetCameraPosition;  // The target position for the camera
    [SerializeField] private float speedReductionFactor = 0.2f;  // Factor to reduce the player's speed

    private PlayerMovement player;
    private float originalSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        originalSpeed = player.speed;
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
            // Only change player speed if we're actually sliding to another position
            if (cameraController.transform.position != targetCameraPosition)
            {
                ChangePlayerSpeed();
            }
        }
    }

    private IEnumerator ResetSpeedDelay()
    {
        yield return new WaitForSeconds(1f);
        ResetPlayerSpeed();
    }

    private void ChangePlayerSpeed()
    {
        if (player != null)
        {
            player.speed *= speedReductionFactor;
        }
        StartCoroutine(ResetSpeedDelay());
    }

    private void ResetPlayerSpeed()
    {
        if (player != null)
        {
            player.speed = originalSpeed;
        }
    }
}
