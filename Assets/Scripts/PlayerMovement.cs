using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCollision playerCollision;
    public Rigidbody2D rb;

    [Header("Player Stats")]
    public float speed = 10f;
    public float jumpForce = 50f;
    public float slideSpeed = 5f;
    public float wallJumpLerp = 10f;

    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;

    private bool groundTouch;

    public int side = 1;

    private float horizontal;
    private float vertical;
    private Vector2 direction;
    private bool isLeft = true; // true = left, false = right

    void Start()
    {
        playerCollision = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical);

        if (playerCollision.onWall && Input.GetButton("Fire3") && canMove)
        {
            if (side != playerCollision.wallSide)
            {
                Flip();
            }
            wallGrab = true;
            wallSlide = false;
        }
        if (Input.GetButtonUp("Fire3") || !playerCollision.onWall || !canMove) // Let go of wall
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (playerCollision.onGround)
        {
            wallJumped = false;
        }

        if (wallGrab)
        {
            rb.gravityScale = 0;
            if (horizontal > .2f || horizontal < -.2f)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            float speedModifier = vertical > 0 ? .5f : 1;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        if (playerCollision.onWall && !playerCollision.onGround)
        {
            if (horizontal != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }
        if (!playerCollision.onWall || playerCollision.onGround)
        {
            wallSlide = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (playerCollision.onGround)
            {
                Jump(Vector2.up);
            }
            else if (playerCollision.onWall && !playerCollision.onGround)
            {
                WallJump();
            }
        }
        if (wallGrab || wallSlide || !canMove)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        Flip();
        Walk(direction);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.linearVelocity = new Vector2(dir.x * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, (new Vector2(dir.x * speed, rb.linearVelocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.linearVelocity += dir * jumpForce;
    }

    private void WallJump()
    {
        if ((side == 1 && playerCollision.onRightWall) ||
            (side == -1 && !playerCollision.onRightWall))
        {
            side *= -1;
            Flip();
        }

        Vector2 wallDir = playerCollision.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f));

        wallJumped = true;
    }

    private void WallSlide()
    {
        if (playerCollision.wallSide != side)
        {
            Flip();
        }

        if (!canMove)
            return;

        bool pushingWall = false;
        if ((rb.linearVelocity.x > 0 && playerCollision.onRightWall) || (rb.linearVelocity.x < 0 && playerCollision.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.linearVelocity.x;

        rb.linearVelocity = new Vector2(push, -slideSpeed);
    }

    private void Flip()
    {
        if (isLeft && horizontal > 0f || !isLeft && horizontal < 0f)
        {
            isLeft = !isLeft;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
