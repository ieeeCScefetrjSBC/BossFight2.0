using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{
    // PUBLIC VARIABLES
    public AudioSource moveAudio;                   // Player walking sound
    //public Animator playerAnim;

    public float validJumpTime = 0.1f;              // Time during which player will still after jump button is pressed
    public float groundMoveInputMag = 2f;           // How fast player speed will change when he starts walking        
    public float airMoveInputMag = 50f;             // How fast player speed will change when he moves midair  

    public float walkSpeed = 9f;                    // Walking speed after full acceleration
    public float runSpeed = 15f;                    // Running speed after full acceleration
    public float jumpSpeed = 13f;                   // Initial vertical speed after jumping
    public float platformJumpSpeed = 30f;           // Initial vertical speed after being propelled by jump platform

    public float minMidairTargetSpeed = 9f;         // How fast player can get midair (horizontal speed) if he jumps while still
    public float midairAccelFactor = 1.10f;         // Percentual increase in horizontal speed allowed after jump
    public float groundDeAccelFactor = 11f;         // Base of the power by which speed will be decreased after no input is given while grounded
    
    // PRIVATE VARIABLES
    private Rigidbody rb;                               // Player Rigidbody
    private CapsuleCollider capsule;                    // Player Capsule Collider
    private float capsuleRadius;                        // Capsule radius
    private float cilinderHalfLenght;                   // Distance from the capsule's center to one of the capsule's half sphere center
    private float capsuleExtensionMultiplier = 1.01f;   // Capsule's height will be extended when used in Physics.OverlapCapsule()
                                                        // in order to guarantee ground detection

    Vector3 moveInput = Vector3.zero;               // Direction of player input
    private float targetSpeed = 9f;                 // Final speed which player will acquire after full acceleration
    private float last_xzSpeedOnGround = 0f;        // Horizontal speed player had when he left the ground
    private float timeJumped = 0f;                  // Time of simulation at which player jumped
    
    private string groundTag;                       // Tag of ground touched object

    private bool isGrounded = false;                // True when player is on ground
    private bool isMoving = false;                  // True when player is moving on the current frame
    private bool wasMoving = false;                 // True when player is moving on the last frame
    private bool runKeyPressed = false;             // True when run key is pressed on the current frame
    private bool hasJumped = false;                 // True when jump command was issued less than validJumpTime secs ago
    private bool invertedControl = false;           // True when player movement should be inverted
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        capsule = gameObject.GetComponent<CapsuleCollider>();
        capsuleRadius = capsule.radius;
        cilinderHalfLenght = (capsule.height / 2) - capsule.radius;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void CheckGrounded()
    {
        int groundLayerMask = 1 << 8;

        Vector3 capsuleCenterPosition = transform.position + capsule.center;
        Vector3 topCapsulePosition = capsuleCenterPosition + Vector3.up * cilinderHalfLenght * capsuleExtensionMultiplier;
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
            hasJumped = true;
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
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveInput = moveInput.normalized * moveInputMag;
        moveInput = transform.TransformDirection(moveInput);
    }

    void FixedUpdate()
    {
        Vector3 xzVelocity;
        
        // -- Applying horizontal movement --
        if (isGrounded)
            rb.AddForce(moveInput, ForceMode.VelocityChange);
        else
            rb.AddForce(moveInput, ForceMode.Acceleration);
        
        // -- Extracting horizontal velocity --
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
            hasJumped = false;
        }
    }
}