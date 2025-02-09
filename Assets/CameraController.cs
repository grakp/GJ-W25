using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Camera Transition")]
    [SerializeField] private float slideSpeed = 2f;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private bool isSliding = false;

    void Update()
    {
        if (isSliding)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, slideSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isSliding = false;
            }
        }
    }

    public void SlideToPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        isSliding = true;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = originalPosition;
    }
}
