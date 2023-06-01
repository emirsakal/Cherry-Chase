using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Object References")]
    public Rigidbody2D rb;
    private Animator anim;
    public GameObject brokenObjects;

    private enum MovementState { idle, running, jumping, falling, sliding }
    private MovementState state = MovementState.idle;

    [Header("Horizontal Movement")]
    public float moveSpeed = 5f;

    [Header("Vertical Movement")]
    public float jumpForce = 5.3f;

    [Header("Grounded")]
    public int canJump;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;
    public bool isGrounded = false;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public LayerMask wallLayer;
    public float wallRadius = 0.2f;
    public float wallJumpTime = 0.05f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.9f;
    public bool isWallSliding = false;
    public bool isTouchingWall = false;
    RaycastHit2D WallCheckHit;
    float jumpTime;

    bool canWallJump;
    public float canWallJumpTimer = 0.7f;
    bool timer;

    [Header("Double Jump")]
    public LayerMask doubleLayer;
    public bool isTouchingDoubleJump = false;

    // Private Variables
    float mx;
    bool isFacingRight = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        if (isGrounded && Input.GetButtonDown("Jump")) {
            Jump();
        }
        if (isTouchingDoubleJump && Input.GetButtonDown("Jump")) {
            DoubleJump();
            canJump--;
        }

        if (isWallSliding && Input.GetButtonDown("Jump") && canWallJump) {
            WallJump();
            canWallJumpTimer = 0.7f;
        }

        UpdateAnimation();
    }

    void FixedUpdate() {
        mx = Input.GetAxisRaw("Horizontal");

        if (mx<0) {
            isFacingRight = false;
            transform.localScale = new Vector2(-1, transform.localScale.y);
        } else if (mx>0) {
            isFacingRight = true;
            transform.localScale = new Vector2(1, transform.localScale.y);
        }

        rb.velocity = new Vector2(mx * moveSpeed, rb.velocity.y);

        bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if(touchingGround) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        bool touchingWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, wallLayer);

        if(touchingWall) {
            isTouchingWall = true;
        } else {
            isTouchingWall = false;
        }

        bool touchingWallBelow = Physics2D.OverlapCircle(groundCheck.position, wallRadius, wallLayer);

        if (touchingWallBelow || touchingGround){
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        bool touchingDoubleJump = Physics2D.OverlapCircle(groundCheck.position, groundRadius, doubleLayer);

        if (touchingDoubleJump) {
            isTouchingDoubleJump = true;
            canJump = 1;
        } else {
            isTouchingDoubleJump = false;
        }

        if (canJump == 1) {
            isTouchingDoubleJump = true;
        } else {
            isTouchingDoubleJump = false;
        }

        // Wall Jump
        if(isFacingRight) {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayer);
            // Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        } else {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            // Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        }

        if (WallCheckHit && mx != 0) {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        } else if (jumpTime < Time.time) {
            isWallSliding = false;
        }

        if (isWallSliding) {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }

        if (timer) {
            canWallJumpTimer -= Time.deltaTime;
        }
        if (canWallJumpTimer < .1f) {
            timer = false;
            canWallJump = true;
        } else {
            timer = true;
            canWallJump = false;
        }
    }

    void UpdateAnimation() {

        if (isGrounded || isTouchingDoubleJump) {
            if (mx != 0 && rb.velocity.y < 0.1f)
                {
                    anim.SetInteger("state", (int)MovementState.running);
                }
            else if (mx == 0 && rb.velocity.y < 0.1f)
                {
                    anim.SetInteger("state", (int)MovementState.idle);
                }
        }

        else if (rb.velocity.y > 0f ) {
            anim.SetInteger("state", (int)MovementState.jumping);
        } else if (rb.velocity.y < 0f ) {
            anim.SetInteger("state", (int)MovementState.falling);
        }

        if (isTouchingWall) {
            anim.SetInteger("state", (int)MovementState.sliding);
        }

        if (isTouchingWall && rb.velocity.y > .1f) {
            anim.SetInteger("state", (int)MovementState.jumping);
        }

    }

    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    void DoubleJump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    void WallJump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("teleport")){
            transform.position = new Vector2(85,18);
        }
        if (collision.gameObject.CompareTag("RedButton")){
            transform.position = new Vector2(-72,20);
        }
        if (collision.gameObject.CompareTag("Broken")){
            Invoke("Destroy", 1f);
            Invoke("UnDestroy",3f);
        }
    }

    void Destroy(){
        brokenObjects.SetActive(false);
    }

    void UnDestroy(){
        brokenObjects.SetActive(true);
    }
}
