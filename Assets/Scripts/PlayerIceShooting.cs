using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerIceShooting : MonoBehaviour
{
    public GameObject ice;
    public GameObject player;
    public Transform firePoint;
    public float iceSpeed = 30f;
    public float maxRange = 5f;  // Adjust this range
    public float spawnDistance = 1f;  // Distance from the player to spawn the platform
    public float platformMass = 100f;  // Set mass after platform stops
    private bool shootIcePlatform;

    Vector2 lookDirection;
    float lookAngle;

    [SerializeField] private GameObject icePlatformPrefab;

    private Rigidbody2D playerRb;

    public int shotsLeft;
    public int platformsLeft;
    [SerializeField] private AudioSource noAmmoSound;
    public Text ammoCounter;
    public Text platformCounter;


    [SerializeField] private AudioSource platformSound;
    [SerializeField] private AudioSource projectileSound;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        ammoCounter.text = shotsLeft.ToString();
        platformCounter.text = platformsLeft.ToString();
    }

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
        if (Input.GetMouseButtonDown(1))
        {
            shootIcePlatform = true;
        }

        if (platformsLeft < 3)
        {
            StartCoroutine(RechargePlatforms());
        }

    }

    IEnumerator RechargePlatforms()
    {

        yield return new WaitForSeconds(4);
        platformsLeft += 1;
        platformCounter.text = platformsLeft.ToString();
        if (platformsLeft == 3)
        {
            yield return null;
        }
    }


    private void FixedUpdate()
    {
        if (shootIcePlatform)
        {
            ShootIcePlatform();
        }
    }

    private void ShootProjectile()
    {
        if (shotsLeft > 0)
        {
            GameObject iceClone = Instantiate(ice);
            iceClone.transform.position = firePoint.position;
            iceClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            iceClone.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * iceSpeed;
            iceClone.tag = "Projectile";

            projectileSound.Play();
            shotsLeft -= 1;
            ammoCounter.text = shotsLeft.ToString();
            platformCounter.text = platformsLeft.ToString();
        } else
        {
            noAmmoSound.Play();
        }
    }

    private void ShootIcePlatform()
    {
        if (shotsLeft > 0 && platformsLeft > 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = transform.position;
            Vector2 direction = (mousePosition - playerPosition).normalized;
            Vector2 spawnPosition = playerPosition + direction * spawnDistance;
            Vector2 targetPosition = playerPosition + direction * Mathf.Min(Vector2.Distance(playerPosition, mousePosition), maxRange);

            GameObject newPlatform = Instantiate(icePlatformPrefab, spawnPosition, Quaternion.identity);
            newPlatform.GetComponent<Rigidbody2D>().linearVelocity = direction * iceSpeed + playerRb.linearVelocity;  // Add player's velocity

            StartCoroutine(MovePlatformTowards(newPlatform, targetPosition));
            shootIcePlatform = false;

            platformSound.Play();
            shotsLeft -= 1;
            platformsLeft -= 1;
            ammoCounter.text = shotsLeft.ToString();
            platformCounter.text = platformsLeft.ToString();

        } else
        {
            noAmmoSound.Play();
            shootIcePlatform = false;
        }
    }
    private IEnumerator MovePlatformTowards(GameObject platform, Vector3 targetPosition)
    {
        Rigidbody2D rb = platform.GetComponent<Rigidbody2D>();
        Collider2D collider = platform.GetComponent<Collider2D>();

        while (platform != null && Vector3.Distance(platform.transform.position, targetPosition) > 0.1f)
        {
            Vector3 newPosition = Vector3.MoveTowards(platform.transform.position, targetPosition, iceSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            rb.linearVelocity = Vector2.zero;  // Reset velocity
            rb.mass = platformMass;

            // Check for collisions
            if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                Destroy(platform);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
