using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] float idleTimeBeforeDestroy;
    private float lastTimeIdle = 0;
    private float timeSinceSceneReset = 0;

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float xBounds;
    [SerializeField] float yBounds;
    private int direction = 1;

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

    [SerializeField] Color defaultColor;
    [SerializeField] Color dashColor;
    [SerializeField] Color jumpColor;
    [SerializeField] Color emptyColor;

    GameManager GMI;

    void Start()
    {
        timeSinceSceneReset = 0;

        GMI = GameManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        storedJumps = numJumps;
        storedDashes = numDashes;
    }

    void Update()
    {
        timeSinceSceneReset += Time.deltaTime;

        if (GameManager.Instance.GameOver == true)
            return;

        if (isPlayerIdle())
        {
            GameManager.Instance.GameOver = true;
            return;
        }

        ProcessPlayerInput();

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xBounds, xBounds), Mathf.Clamp(transform.position.y, -Mathf.Infinity, yBounds));
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameOver == true)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void ProcessPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.QuitGame();
        }

        float xInput = Input.GetAxis("Horizontal");
        float xMovement = xInput * speed;

        if (!isDashing)
            rb.velocity = new Vector2(xMovement, rb.velocity.y);

        animator.SetFloat("moveSpeed", Mathf.Abs(xMovement));

        if (xMovement < 0)
        {
            lastTimeIdle = timeSinceSceneReset;
            direction = -1;
            spriteRenderer.flipX = true;
        }
        else if (xMovement > 0)
        {
            lastTimeIdle = timeSinceSceneReset;
            direction = 1;
            spriteRenderer.flipX = false;
        }

        if (isGrounded)
        {
            numJumps = storedJumps;
            numDashes = storedDashes;
            animator.SetBool("isJumping", false);
            animator.SetBool("isDashing", false);
            spriteRenderer.color = new Color(255, 255, 255);
        }

        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E) && timeSinceLastDash > dashCooldown && numDashes > 0)
        {
            lastTimeIdle = Time.time;
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
        animator.SetBool("isJumping", false);
        animator.SetBool("isDashing", true);

        isDashing = true;
        timeSinceLastDash = 0f;
        numDashes--;
        rb.velocity = new Vector2(dashSpeed * direction, 0f);
        rb.AddForce(new Vector2(dashLength * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;

        if (numDashes == 0)
        {
            spriteRenderer.color = new Color(150, 150, 0);
        }

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = gravity;
        isDashing = false;
    }

    private void Jump()
    {
        numJumps--;
        rb.velocity = Vector2.up * jumpSpeed;
        animator.SetBool("isDashing", false);

        StartCoroutine(StartJumpingAnimation());

        if (numJumps == 0)
        {
            spriteRenderer.color = new Color(0, 255, 255);
        }
    }

    public bool isPlayerIdle()
    {
        return timeSinceSceneReset - lastTimeIdle > idleTimeBeforeDestroy;
    }

    public Vector2 GetPlayerPos()
    {
        return gameObject.transform.position;
    }

    IEnumerator StartJumpingAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", true);
    }
}
