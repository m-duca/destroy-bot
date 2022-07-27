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
    [Range(0f, 1f)] private float frictionAmount;

    private float moveInput = 0;

    // Jump Variables
    [Header("Jump Settings:")]
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    [Range(0f, 1f)] private float coyoteTime;

    [SerializeField] private bool canJump = false;

    private bool isJumping = false;
    private bool jumpInputPressed = false;
    private float latestGroundTime = 0;
    private bool isCoyoteTime = false;

    // Ground Check Variables
    [Header("Ground Detection:")]
    [SerializeField]
    private Vector2 groundCheckSize;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform checkGroundPoint;

    // Gravity Variables
    [Header("Gravity Settings:")]
    [SerializeField] 
    [Range(0f, 2f)] private float fallGravityMultiplier;

    #region Engine Functions

    // Start is called before the first frame update
    private void Start()
    {
   

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
        Animate();
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
        FallGravity();
    }


    #endregion


    #region Custom Functions

    private void ApplyMovement()
    {
        float targetSpeed = moveInput * moveSpeed;

        float speedDif = targetSpeed - Player.rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        Player.rb.AddForce(movement * Vector2.right);
    }

    private void ApplyJump()
    {
        if (isCoyoteTime){
            Player.rb.velocity = new Vector2(Player.rb.velocity.x, 0);
        }

        Player.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        canJump = false;
        isJumping = true;
        jumpInputPressed = false;
    }

    private void ApplyFriction()
    {
        if (latestGroundTime > 0 && moveInput == 0){
            float amount = Mathf.Min(Mathf.Abs(Player.rb.velocity.y), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(Player.rb.velocity.x);

            Player.rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }


    private void FlipX()
    {

        if (moveInput < 0) {
            Player.spr.flipX = true;
        }
        else if (moveInput > 0) {
            Player.spr.flipX = false;
        }

    }

    private void Animate()
    {
        Player.animator.SetFloat("move", Mathf.Abs(moveInput));
        Player.animator.SetBool("jump", isJumping);
    }

    private void CheckGround() 
    {
        if(Physics2D.OverlapBox(checkGroundPoint.position, groundCheckSize, 0, groundLayer)){
            canJump = true;
            isJumping = false;
            latestGroundTime += Time.fixedDeltaTime;
        }
        else{
            if (!isCoyoteTime){
                StartCoroutine(CoyoteTime(coyoteTime));
            }
        }
    }

    private IEnumerator CoyoteTime(float time)
    {
        isCoyoteTime = true;
        yield return new WaitForSeconds(time);
        canJump = false;
        latestGroundTime = 0;
        isCoyoteTime = false;
    }

    private void FallGravity()
    {
        if (Player.rb.velocity.y < 0 && isJumping){
            Player.rb.velocity += Vector2.down * fallGravityMultiplier;
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
