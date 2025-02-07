using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private CameraController cameraController;
    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        Vector3 respawnPosition = CheckpointManager.Instance.GetLastCheckpointPosition();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // Camera shake
        if (cameraController != null)
        {
            StartCoroutine(cameraController.Shake(0.5f, 0.1f));
        }

        yield return new WaitForSeconds(respawnDelay);

        Vector3 respawnPosition = CheckpointManager.Instance.GetLastCheckpointPosition();
        if (respawnPosition != Vector3.zero)
        {
            transform.position = respawnPosition;
        }
        else
        {
            Debug.LogWarning("No checkpoint set.");
        }
    }
}
