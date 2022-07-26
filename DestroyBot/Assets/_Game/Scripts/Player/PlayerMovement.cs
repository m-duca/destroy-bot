using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    // Move Variables
    [Header("Movement Settings:")]
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float decceleration;

    [SerializeField]
    private float velPower;

    [SerializeField]
    private float frictionAmount;

    private float moveInput = 0;

    // Jump Variables
    [Header("Jump Settings:")]
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    [Range(0f, 1f)] private float coyoteTime;

    [SerializeField]
    private Vector2 groundCheckSize;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform checkGroundPoint;

    [SerializeField] private bool canJump = false;
    private bool isJumping = false;
    private bool jumpInputPressed = false;
    private float latestGroundTime = 0;

    // Gravity Variables
    [Header("Gravity Settings:")]
    [SerializeField] private float fallGravityMultiplier;

    private float gravityScale = 2;

    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    #region Engine Functions

    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Move Input
        if (Input.GetKey(KeyCode.D)){
            moveInput = 1;
        }
        else if (Input.GetKey(KeyCode.A)) {
            moveInput = -1;
        }
        else {
            moveInput = 0;
        }

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpInputPressed = true;
        }

        FlipX();
        Debugging();

    }

    private void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();

        if (jumpInputPressed && canJump) {
            ApplyJump();
        }
        else {
            jumpInputPressed = false;
        }

        ApplyFriction();
        //ExtraGravity();
    }


    #endregion


    #region Custom Functions

    private void ApplyMovement()
    {
        float targetSpeed = moveInput * moveSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private void ApplyJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        canJump = false;
        isJumping = true;
        jumpInputPressed = false;
    }

    private void ApplyFriction()
    {
        if (latestGroundTime > 0 && moveInput == 0){
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }


    private void FlipX()
    {

        if (moveInput < 0) {
            spr.flipX = true;
        }
        else if (moveInput > 0) {
            spr.flipX = false;
        }

    }

    private void CheckGround() 
    {
        if(Physics2D.OverlapBox(checkGroundPoint.position, groundCheckSize, 0, groundLayer)){
            canJump = true;
            latestGroundTime += Time.fixedDeltaTime;
        }
        else{
            canJump = false;
            latestGroundTime = 0;
        }
    }

    private void ExtraGravity()
    {
        if (rb.velocity.y < 0){
            rb.gravityScale *= fallGravityMultiplier;
        }
        else{
            rb.gravityScale = gravityScale;
        }
    }

    private void Debugging()
    {

        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    #endregion

}
