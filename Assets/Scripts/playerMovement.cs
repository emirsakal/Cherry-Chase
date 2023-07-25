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
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState { idle, running, jumping, falling, sliding, doubleJumping }
    private MovementState state = MovementState.idle;

    [Header("Horizontal Movement")]
    [SerializeField] private float _movementAcceleration = 30f; // 50f 
    [SerializeField] private float _maxMoveSpeed = 7f; // 7f
    [SerializeField] private float _linearDrag = 7f;
    private float mx;
    private float my;
    private bool _changingDirection => (rb.velocity.x > 0f && mx < 0f) || (rb.velocity.x < 0f && mx > 0f);
    private bool isFacingRight = true;

    [Header("Vertical Movement")]
    public float jumpForce = 8f;
    [SerializeField] private float _airLinearDrag = 2f; // 2.5f
    [SerializeField] private float _fallMultiplier = 3f; // 8f
    [SerializeField] private float _lowJumpFallMultiplier = 2f; // 2f
    [SerializeField] private float _hangTime = .15f;
    [SerializeField] private float _jumpBufferLength = .1f;
    private float _hangTimeCounter;
    private float _jumpBufferCounter;
    [SerializeField] private float jumpInputDelay = 0.18f;
    private float jumpInputDelayTimer = 0f;
    private bool readyToJump;
    

    [Header("Grounded")]
    public int canJump;
    public int canDoubleJump;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;
    public bool isGrounded = false;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public LayerMask wallLayer;
    public float wallRadius = 0.2f;
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = -0.8f;
    public float wallDistance = 0.7f; // 0.9f
    public bool isWallSliding = false;
    public bool isTouchingWall = false;
    RaycastHit2D WallCheckHit;
    float jumpTime;

    bool canWallJump;
    public float canWallJumpTimer = 2f;
    bool timer;

    [Header("Double Jump")]
    public LayerMask doubleLayer;
    public bool isTouchingDoubleJump = false;

    private float switchCooldown = 0.5f; // Cooldown time before player can trigger the particle effect again
    private float switchTimer = 0f;

    void Start() {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movementParticle.Stop();
        anim.SetInteger("state", (int)MovementState.idle);
    }

    void Update(){
        mx = GetInput().x;
        my = GetInput().y;
        
        switchTimer += Time.deltaTime;

        // Detect player direction change with cooldown
        if (switchTimer >= switchCooldown)
        {
            if ((isFacingRight && Input.GetAxis("Horizontal") < 0 && isGrounded) ||
                (!isFacingRight && Input.GetAxis("Horizontal") > 0 && isGrounded))
            {
                isFacingRight = !isFacingRight;
                movementParticle.Play();
                switchTimer = 0f; // Reset the timer
            }
        }

        DelayInput();

        if (Input.GetButtonDown("Jump")) {
            _jumpBufferCounter = _jumpBufferLength;
        } else {
            _jumpBufferCounter -= Time.deltaTime; 
        }

        if(canJump < 0) {
            canJump = 0;
        }
        if(canDoubleJump < 0) {
            isTouchingDoubleJump = false;
            canDoubleJump = 0;
        }

        if(canDoubleJump > 1 && Input.GetButtonDown("Jump") && !isGrounded) {
            canDoubleJump--;
        }
                
        if (canDoubleJump == 1 && Input.GetButtonDown("Jump") && canJump == 0) { // canDoubleJump'ı canJump variable'ına çevirip double jump için de canJump'ı kullanırsan ve bu if statement'ı silersen karakter sadece hangTimer'ın içindeyken zıplayabilir.
            DoubleJump(); // This was DoubleJump() but I removed it since it causes duplicate jump sound effects.
        }


        if (isWallSliding && Input.GetButtonDown("Jump") && canWallJump && !isGrounded) {
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
        
        if (isGrounded)
        {
            rb.drag = _linearDrag;
            _hangTimeCounter = _hangTime;

            // Adjust movement acceleration and maximum move speed for consistent feel
            float targetVelocityX = mx * _maxMoveSpeed;
            float acceleration = Mathf.Abs(targetVelocityX - rb.velocity.x) < 0.01f ? 0f : _movementAcceleration;
            rb.AddForce(new Vector2((targetVelocityX - rb.velocity.x) * acceleration, 0f));
        }
        else
        {
            rb.drag = _airLinearDrag;
            FallMultiplier();
            _hangTimeCounter -= Time.fixedDeltaTime;
        }


        if (_hangTimeCounter > 0f && _jumpBufferCounter > 0f && readyToJump) {
            jumpInputDelayTimer = jumpInputDelay;
            canJump--;
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

        if (touchingDoubleJump) { // I added the isGrounded = true; because of the running animation bug when the characther does a double jump. (Between first and second jump)
            isTouchingDoubleJump = true;
            isGrounded = true;
        }

        

        // Wall Jump
        if(isFacingRight) {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayer);
            // Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        } else {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            // Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.red);
        }

        if (WallCheckHit) {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        } else if (jumpTime < Time.time) {
            isWallSliding = false;
        }

        if (isWallSliding && mx != 0) { // && mx != 0 eğer A ve D tuşuna basarak durmasını istiyorsan
            anim.SetInteger("state", (int)MovementState.jumping);
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

        if (isGrounded) {
            if (mx != 0 && rb.velocity.y < 0.1f )
                {
                    anim.SetInteger("state", (int)MovementState.running);
                }
            else if (mx == 0 && rb.velocity.y < 0.1f)
                {
                    anim.SetInteger("state", (int)MovementState.idle);
                }
        } else if (rb.velocity.y < 0f ) {
            anim.SetInteger("state", (int)MovementState.falling);
        }

        if (isTouchingWall && !isGrounded && rb.velocity.y < 0.1f) {
            anim.SetInteger("state", (int)MovementState.sliding);
        }

        if (isTouchingWall && rb.velocity.y > .1f) {
            anim.SetInteger("state", (int)MovementState.jumping);
        }

    }

    void Jump(){
        anim.SetInteger("state", (int)MovementState.jumping);
        canJump--;
        canDoubleJump--;
        jumpParticle.Play();
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Apply the same jump force regardless of the movement speed
        float jumpForceMultiplier = Mathf.Sqrt(2f * jumpForce * Physics2D.gravity.magnitude);
        rb.AddForce(Vector2.up * jumpForceMultiplier, ForceMode2D.Impulse);

        _hangTimeCounter = 0f;
        _jumpBufferCounter = 0f;
    }

    void DoubleJump(){        // I removed this because of the twice jump sound problem.
        anim.SetInteger("state", (int)MovementState.doubleJumping);
        canJump--;
        canDoubleJump--;
        jumpParticle.Play();
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Apply the same jump force regardless of the movement speed
        float doubleJumpForceMultiplier = Mathf.Sqrt(jumpForce * -2f * Physics2D.gravity.y);
        rb.AddForce(Vector2.up * doubleJumpForceMultiplier, ForceMode2D.Impulse);

        _hangTimeCounter = 0f;
        _jumpBufferCounter = 0f;
        isTouchingDoubleJump = false;
    }

    void WallJump(){
        jumpSoundEffect.Play();
        float wallJumpForceMultiplier = Mathf.Sqrt(2f * jumpForce * Physics2D.gravity.magnitude);
        rb.AddForce(Vector2.up * wallJumpForceMultiplier, ForceMode2D.Impulse);
    }
  
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("teleport")){
            transform.position = new Vector2(85,18);
        }
        if (collision.gameObject.CompareTag("YellowButton") || collision.gameObject.CompareTag("RedButton") || collision.gameObject.CompareTag("GreenButton") || collision.gameObject.CompareTag("BlueButton")){
            buttonClick.Play();
        }
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 7){
            canJump = 1;
            canDoubleJump = 0;
        }
        if(collision.gameObject.layer == 8) {
            canDoubleJump = 2;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Fan") && rb.velocity.y > 0.1f){
            anim.SetInteger("state", (int)MovementState.jumping);
        }
    }


    private static Vector2 GetInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter() {
        float moveSpeed = Mathf.Abs(rb.velocity.x);
        float targetVelocityX = mx * _maxMoveSpeed;

        // Apply acceleration based on the target velocity
        float acceleration = moveSpeed < Mathf.Abs(targetVelocityX) ? _movementAcceleration : _linearDrag;
        rb.AddForce(new Vector2((targetVelocityX - rb.velocity.x) * acceleration, 0f));

        // Limit the maximum move speed
        if (moveSpeed > _maxMoveSpeed)
        {
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
