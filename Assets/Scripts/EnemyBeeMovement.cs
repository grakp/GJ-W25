using System;
using UnityEngine;

public class EnemyBeeMovement : MonoBehaviour
{

    public float dirX;
    public float moveSpeed;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Vector3 localScale;

    public GameObject enemy;

    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetComponent<Hazard>().isFrozen)
        {
            rb.linearVelocity = new Vector2(0, 0);
        } else { 
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.linearVelocity = new Vector2(0, -moveSpeed);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, moveSpeed);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
                dirX *= -1;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
                dirX *= -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Wall>())
        {
            if (currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
                dirX *= -1;
            }
            if (currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
                dirX *= -1;
            }
        }
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    private void CheckWhereToFace()
    {
        if (dirX > 0)
        {
            facingRight = true;
        } else if (dirX < 0)
        {
            facingRight = false;
        }
        if ((facingRight && localScale.x < 0) || (!facingRight && localScale.x > 0))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }
}
