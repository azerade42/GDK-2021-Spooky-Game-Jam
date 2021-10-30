using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float xBounds;
    [SerializeField] float yBounds;
    private int direction = 0;

    [SerializeField] int numJumps;
    private int storedJumps;

    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;

    private bool isGrounded;
    private bool isDashing;

    [SerializeField] float dashLength;
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;

    [SerializeField] int numDashes;
    private int storedDashes;
    private float timeSinceLastDash = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        storedJumps = numJumps;
        storedDashes = numDashes;
    }

    void Update()
    {
        ProcessPlayerInput();

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xBounds, xBounds), Mathf.Clamp(transform.position.y, -Mathf.Infinity, yBounds));
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void ProcessPlayerInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float xMovement = xInput * speed;

        if (!isDashing)
            rb.velocity = new Vector2(xMovement, rb.velocity.y);

        if (xMovement < 0)
        {
            direction = -1;
            spriteRenderer.flipX = true;
        }
        else if (xMovement > 0)
        {
            direction = 1;
            spriteRenderer.flipX = false;
        }

        if (isGrounded == true)
        {
            numJumps = storedJumps;
            numDashes = storedDashes;
        }

        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E) && timeSinceLastDash > dashCooldown && numDashes > 0)
        {
            //TELEPORTATION JUTSU rb.AddForce(new Vector2(direction * 10000, 0));
            StartCoroutine(Dash());
        }
        else
        {
            timeSinceLastDash += Time.deltaTime;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        timeSinceLastDash = 0f;
        numDashes--;
        rb.velocity = new Vector2(dashSpeed * direction, 0f);
        rb.AddForce(new Vector2(dashLength * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = gravity;
        isDashing = false;
    }

    private void Jump()
    {
        numJumps--;
        rb.velocity = Vector2.up * jumpSpeed;
    }
}
