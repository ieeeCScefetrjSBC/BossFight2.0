using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{
    // --- PUBLIC VARIABLES ---

    public AudioSource moveAudio;                   // Player's walking sound
   
    //public Animator playerAnim;

    public float validJumpTime        = 0.1f;       // Time during which the player will jump when he touches the ground after a jump command has been issued;
    public float groundMoveInputMag   = 2f;         // How fast the player's speed will change when he is moved on the ground;
    public float airMoveInputMag      = 50f;        // How fast the player's speed will change when he is moved midair;
    
    public float walkSpeed            = 9f;         // Walking speed after full acceleration;
    public float runSpeed             = 15f;        // Running speed after full acceleration;
    public float jumpSpeed            = 13f;        // Initial vertical speed right after jumping;
    public float platformJumpSpeed    = 50f;        // Initial vertical speed right after being propelled by jump platform;

    public float minMidairTargetSpeed = 9f;         // How fast player can get midair (horizontal speed) if he jumps while still;
    public float midairAccelFactor    = 1.10f;      // Percentual increase in horizontal speed allowed after jump;
    public float groundDeAccelFactor  = 11f;        // Base of the power by which speed will be decreased after no input is given while grounded;
    
    // --- PRIVATE VARIABLES ---

    private Rigidbody rb;                           // Player's Rigidbody component;
    private CapsuleCollider capsule;                // Player's Capsule Collider component;
    private Animator playerAnimator;
    private Vector3 moveInput = Vector3.zero;       // Direction of player input;
    
    private float targetSpeed          = 9f;        // Final speed which player will acquire after full acceleration;
    private float last_xzSpeedOnGround = 0f;        // Horizontal speed player had when he left the ground;
    private float timeJumped           = 0f;        // Time of simulation at which player jumped;
    
    private string groundTag;                       // Tag of ground object touched;

    private bool isGrounded      = false;           // True when player is on ground;
    private bool isMoving        = false;           // True when player is moving on the current frame;
    private bool wasMoving       = false;           // True when player was moving on the last frame;
    private bool hasJumped       = false;           // True when a jump command was issued less than validJumpTime secs ago;
    private bool invertedControl = false;           // True when player movement should be inverted;
    
    void Start()
    {
        rb      = gameObject.GetComponent<Rigidbody>();
        capsule = gameObject.GetComponent<CapsuleCollider>();
        playerAnimator = gameObject.GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetInvertedControl (bool invertedControlOn)
    {
        invertedControl = invertedControlOn;
    }

    private void CheckGrounded()
    {
        int groundLayerMask      = 1 << 8;
        float capsuleRadius      = capsule.radius;                          // Radius of the capsule;
        float cilinderHalfLenght = (capsule.height / 2) - capsule.radius;   // Distance from the capsule's center to one of the capsule's half sphere's center;
        float capsuleExtensionMultiplier = 1.01f;                           // Capsule's height will be extended when used in Physics.OverlapCapsule()
                                                                            // in order to guarantee ground detection;

        Vector3 capsuleCenterPosition = transform.position + capsule.center;
        Vector3 topCapsulePosition    = capsuleCenterPosition + Vector3.up * cilinderHalfLenght * capsuleExtensionMultiplier;
        Vector3 bottomCapsulePosition = capsuleCenterPosition - Vector3.up * cilinderHalfLenght * capsuleExtensionMultiplier;

        Collider[] collidersTouched = Physics.OverlapCapsule(topCapsulePosition, bottomCapsulePosition, capsuleRadius, groundLayerMask);
        
        if (collidersTouched.Length == 0)
        {
            isGrounded = false;
            return;
        }

        foreach (Collider collider in collidersTouched)
        {
            groundTag = collider.gameObject.tag;

            isGrounded = true;
            break;
        }
    }

    void Update()
    {
        // -- Evaluating if player should jump --
        float timeSinceJump = Time.time - timeJumped;
        CheckGrounded();
        
        if (timeSinceJump > validJumpTime)
            hasJumped = false;

        if (Input.GetButtonDown("Jump"))
        {
            timeJumped = Time.time;
            hasJumped  = true;
        }

        // -- Setting move speed constraint --
        if (!isGrounded)
        {
            if (last_xzSpeedOnGround < minMidairTargetSpeed)
                targetSpeed = minMidairTargetSpeed * midairAccelFactor;
            else
                targetSpeed = last_xzSpeedOnGround * midairAccelFactor;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
            targetSpeed = runSpeed;
        else
            targetSpeed = walkSpeed;

        // -- Checking if ground or air control --
        float moveInputMag;

        if (isGrounded)
            moveInputMag = groundMoveInputMag;
        else
            moveInputMag = airMoveInputMag;

        // -- Calculating movement direction from player input --
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveInput = new Vector3(playerInput[0], 0f, playerInput[1]);
        moveInput = moveInput.normalized * moveInputMag;
        moveInput = transform.TransformDirection(moveInput);

        if (invertedControl)
            moveInput = (-1) * moveInput;

        // -- Animation --
        if (!isGrounded)
            playerInput = Vector2.zero;

        playerAnimator.SetFloat("InputH", playerInput[0]);
        playerAnimator.SetFloat("InputV", playerInput[1]);
    }

    void FixedUpdate()
    {
        // -- Applying horizontal movement --
        if (isGrounded)
            rb.AddForce(moveInput, ForceMode.VelocityChange);
        else
            rb.AddForce(moveInput, ForceMode.Acceleration);
        
        // -- Extracting horizontal velocity --
        Vector3 xzVelocity;

        xzVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (isGrounded)
            last_xzSpeedOnGround = xzVelocity.magnitude;

        // -- Fixing movement if above target speed limit --
        if (xzVelocity.magnitude > targetSpeed)
            rb.velocity = xzVelocity.normalized * targetSpeed + new Vector3(0f, rb.velocity.y, 0f);

        // -- DeAccelerating player to a stop if there's no input when grounded --
        if (moveInput == Vector3.zero && rb.velocity.magnitude > 0f && isGrounded)
        {
            float stoppingSpeed = 0.01f;
            float newSpeed = xzVelocity.magnitude;

            newSpeed *= 1 / Mathf.Pow(groundDeAccelFactor, Time.deltaTime);

            if (newSpeed < stoppingSpeed)
                newSpeed = 0f;

            xzVelocity = xzVelocity.normalized * newSpeed;
            rb.velocity = new Vector3(xzVelocity.x, rb.velocity.y, xzVelocity.z);
        }

        // -- Jumping --
        if (isGrounded && (hasJumped || groundTag == "PlataformaPulo"))
        {
            float ySpeed;

            if (groundTag == "PlataformaPulo")
                ySpeed = platformJumpSpeed;
            else
                ySpeed = jumpSpeed;

            rb.velocity = new Vector3(rb.velocity.x, ySpeed, rb.velocity.z);
            //rb.velocity += new Vector3(0f, ySpeed, 0f);
            hasJumped = false;
        }
    }
}