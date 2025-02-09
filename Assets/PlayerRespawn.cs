using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject deathParticlePrefab;



    public AudioSource deathSound;

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
        // In case player collides with something else
        gameObject.GetComponent<Collider2D>().enabled = false;

        // Instantiate death particle effect
        GameObject deathParticles = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        if (deathParticles != null)
        {
            Destroy(deathParticles, 2f);
        }
        deathSound.Play();
        yield return new WaitForSeconds(respawnDelay);

        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

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
