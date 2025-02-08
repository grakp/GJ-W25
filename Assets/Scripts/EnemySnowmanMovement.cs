
using UnityEngine;

public class EnemySnowmanMovement : MonoBehaviour
{
    public float jumpingPower;
    private Rigidbody2D rb;
    bool isGrounded;
    public GameObject enemy;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int randomNumber = Random.Range(1, 2000);
        if (randomNumber < 2 && isGrounded && !enemy.GetComponent<Hazard>().isFrozen)
        {
            rb.linearVelocity = new Vector2(0, jumpingPower);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = false;
        }
    }
}
