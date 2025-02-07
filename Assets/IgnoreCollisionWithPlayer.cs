using UnityEngine;

public class IgnoreCollisionWithPlayer : MonoBehaviour
{

    void FixedUpdate()
    {
        Collider2D bulletCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();

        if (playerCollider != null && bulletCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }
    }
}
