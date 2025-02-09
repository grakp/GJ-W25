using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{

    public Sprite frozenSprite;
    public bool isFrozen;
    public float freezeDuration;
    public bool canBeFrozen = true;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Sprite originalSprite;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isFrozen && canBeFrozen)
        {
            StartCoroutine(FreezeTemporarily());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFrozen)
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                other.GetComponent<SpriteRenderer>().enabled = false;
                playerRespawn.Respawn();
            }
        }

        if (other.CompareTag("Projectile") && canBeFrozen)
        {
            isFrozen = true;
        }

        if (other.CompareTag("Platform"))
        {
            Destroy(other.gameObject);
        }

    }

    IEnumerator FreezeTemporarily()
    {
        // Store the current sprite from the animation
        originalSprite = spriteRenderer.sprite;

        // Disable the animator and switch to the freeze sprite
        animator.enabled = false;
        spriteRenderer.sprite = frozenSprite;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Restore the animator (it resumes from where it left off)
        animator.enabled = true;
        isFrozen = false;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

}
