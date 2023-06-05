using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Particle System")]
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem fallParticle1;
    [SerializeField] ParticleSystem movementParticle;

    [Range(0,10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0,0.2f)]
    [SerializeField] float dustFormationPeriod;
    float counter;

    [SerializeField] ParticleSystem jumpParticle;

    [Header("Object References")]
    public Rigidbody2D rb;
    private Animator anim;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState { idle, running, jumping, falling, sliding }
    private MovementState state = MovementState.idle;

    [Header("Horizontal Movement")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 10f;
    [SerializeField] private float _linearDrag = 7f;
    private float mx;
    private float my;
    private bool _changingDirection => (rb.velocity.x > 0f && mx < 0f) || (rb.velocity.x < 0f && mx > 0f);
    private bool isFacingRight = true;

    [Header("Vertical Movement")]
    public float jumpForce = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private float _hangTime = .15f;
    [SerializeField] private float _jumpBufferLength = .1f;
    private float _hangTimeCounter;
    private float _jumpBufferCounter;
    [SerializeField] private float jumpInputDelay = 0.18f;
    private float jumpInputDelayTimer = 0f;
    private bool readyToJump;
    

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


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        mx = GetInput().x;
        my = GetInput().y;
        
        DelayInput();

        if (Input.GetButtonDown("Jump")) {
            _jumpBufferCounter = _jumpBufferLength;
        } else {
            _jumpBufferCounter -= Time.deltaTime; 
        }

        if (isTouchingDoubleJump && Input.GetButtonDown("Jump")) {
            DoubleJump();
            canJump--;
        }

        if (isWallSliding && Input.GetButtonDown("Jump") && canWallJump) {
            WallJump();
            canWallJumpTimer = 0.7f;
        }
        
        counter += Time.deltaTime;

        if(isGrounded && Mathf.Abs(rb.velocity.x) > occurAfterVelocity) {
            if (counter > dustFormationPeriod) {
                // movementParticle.Play();
                counter = 0;
            }
        }

        UpdateAnimation();
    }

    void FixedUpdate() {

        MoveCharacter();
        
        if (isGrounded) {
            ApplyLinearDrag();
            _hangTimeCounter = _hangTime;
        } else {
            ApplyAirLinearDrag();
            FallMultiplier();
            _hangTimeCounter -= Time.fixedDeltaTime;
        }

        if (_hangTimeCounter > 0f && _jumpBufferCounter > 0f && readyToJump) {
            jumpInputDelayTimer = jumpInputDelay;
            Jump();
        }
        

        if (mx<0) {
            isFacingRight = false;
            transform.localScale = new Vector2(-1, transform.localScale.y);
            jumpParticle.transform.localScale = new Vector2(-1, transform.localScale.y);
            movementParticle.transform.localScale = new Vector2(-1, transform.localScale.y);
            fallParticle.transform.localScale = new Vector2(-1, transform.localScale.y);
            fallParticle1.transform.localScale = new Vector2(-1, transform.localScale.y);
        } else if (mx>0) {
            isFacingRight = true;
            transform.localScale = new Vector2(1, transform.localScale.y);
            jumpParticle.transform.localScale = new Vector2(1, transform.localScale.y);
            movementParticle.transform.localScale = new Vector2(1, transform.localScale.y);
            fallParticle.transform.localScale = new Vector2(1, transform.localScale.y);
            fallParticle1.transform.localScale = new Vector2(1, transform.localScale.y);
        }

        if (rb.velocity.x > 3f && isFacingRight == false && isGrounded && rb.velocity.y <= 0) {
            movementParticle.Play();
        } else if (rb.velocity.x < -3f && isFacingRight == true && isGrounded && rb.velocity.y <= 0) {
            movementParticle.Play();
        }

        

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
        jumpParticle.Play();
        jumpSoundEffect.Play();
        ApplyLinearDrag();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _hangTimeCounter = 0f;
        _jumpBufferCounter = 0f;
    }
    void DoubleJump(){
        jumpParticle.Play();
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    void WallJump(){
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
  
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("teleport")){
            transform.position = new Vector2(85,18);
        }
        if (collision.gameObject.CompareTag("RedButton")){
            transform.position = new Vector2(-72,20);
        }
    }



    private static Vector2 GetInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter() {
        rb.AddForce(new Vector2(mx, 0f) * _movementAcceleration);

        if(Mathf.Abs(rb.velocity.x) > _maxMoveSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * _maxMoveSpeed, rb.velocity.y);
        }
    }

    private void ApplyLinearDrag() {
        if(Mathf.Abs(mx) < 0.4f || _changingDirection) {
            rb.drag = _linearDrag;
        } else {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag() {
        rb.drag = _airLinearDrag;
    }

    private void FallMultiplier(){
        if (rb.velocity.y < 0) {
            rb.gravityScale = _fallMultiplier;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.gravityScale = _lowJumpFallMultiplier;
        } else {
            rb.gravityScale = 1f;
        }
    }

    private void DelayInput()
    {
        switch (jumpInputDelayTimer) 
        {
            case <= 0:
                readyToJump = true;
                break;
            default:
                readyToJump = false;
                jumpInputDelayTimer -= Time.deltaTime;
                break;
        }   
    }

}
