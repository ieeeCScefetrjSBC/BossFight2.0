using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{
    // ESSE SCRIPT EST� SENDO CHAMADO NOS SCRIPTS "InverterControles", "HeliceDeGelo" E "Sopro"
    
    // --- PUBLIC VARIABLES ---

    public AudioSource moveAudio;                   // Player's walking sound
   
    //public Animator playerAnim;

    public float validJumpTime        = 0.1f;       // Time during which the player will jump when he touches the ground after a jump command has been issued;
    public float groundInputMag       = 2f;         // Determines how fast the player's speed will change when he is moved on the ground;
    public float airInputMag          = 50f;        // Determines how fast the player's speed will change when he is moved midair;
    public float frozenForceInputMag  = 100f;       // DEBUG
    
    public float walkSpeed            = 9f;         // Walking speed after full acceleration;
    public float runSpeed             = 15f;        // Running speed after full acceleration;
    public float jumpSpeed            = 16f;        // Initial vertical speed right after jumping;
    public float platformJumpSpeed    = 50f;        // Initial vertical speed right after being propelled by jump platform;
    public float wallJumpForce        = 20f;
    public float wallJumpAngle        = 60f;

    public float minMidairTargetSpeed = 9f;         // How fast player can get midair (horizontal speed) if he jumps while still;
    public float midairAccelAllowed   = 1.10f;      // Percentual increase in horizontal speed allowed (after reaching target speed) after jump;
    public float groundMoveDamp       = 11f;        // Base of the power by which speed will be decreased after no input is given while grounded;
    public float groundCheckDistance  = 0.5f;
    public float wallOffset           = 1f;
    public float wallJumpDamp         = 0.3f;

    public bool wallJumpActive = false;
    
    // --- PRIVATE VARIABLES ---

    private Rigidbody rb;                               // Player's Rigidbody component;
    private CapsuleCollider capsule;                    // Player's Capsule Collider component;
    private Animator playerAnimator;                    // Player's Animator component;
    //private HeliceDeGelo iceHelixScript;

    private Vector3 moveInput = Vector3.zero;           // Direction of player input;
    private Vector3 capsuleCenterPosition;
    private Vector3 topCapsulePosition;
    private Vector3 bottomCapsulePosition;
    private RaycastHit wallHit;

    private float capsuleRadius;                        // Radius of the capsule;
    private float cilinderHalfLenght;                   // Distance from the capsule's center to one of the capsule's half sphere's center;
    private float capsuleHeightMultiplier = 1.00f;      // Capsule's height will be distorted when used in Physics.CapsuleCast()
                                                        // in order to guarantee ground detection;
    private float capsuleRadiusMultiplier = 0.95f;      // Capsule's radius will be extended when used in Physics.CapsuleCast()
                                                        // in order to guarantee wall detection for wall jumps;

    private float targetSpeed          = 9f;            // Final speed which player will acquire after full acceleration;
    private float last_xzSpeedOnGround = 0f;            // Horizontal speed player had when he left the ground;
    private float timeJumped           = 0f;            // Time of simulation at which player jumped;
    private float currentWallJumpForce = 0f;
    private int   groundLayerMask      = 1 << 8;

    private string groundTag;                       // Tag of ground object touched;

    private bool isGrounded      = false;           // True when player is on ground;
    private bool isMoving        = false;           // True when player is moving on the current frame;
    private bool wasMoving       = false;           // True when player was moving on the last frame;
    private bool hasJumped       = false;           // True when a jump command was issued less than validJumpTime secs ago;
    private bool wallApproach    = false;
    private bool invertedControl = false;           // True when player movement should be inverted;
    
    private float freezeTimer = 0f;
    private float freezeRecoveryTime = 5f;
    private float forceRecoveryValue = 5f;
    private bool  isFrozen = false;

    void Start()
    {
        rb             = gameObject.GetComponent<Rigidbody>();
        capsule        = gameObject.GetComponent<CapsuleCollider>();
        playerAnimator = gameObject.GetComponent<Animator>();

        // The following must not be changed later
        capsuleRadius      = capsule.radius;
        cilinderHalfLenght = (capsule.height / 2) - capsule.radius;

        // The following must be updated according to the player's position
        capsuleCenterPosition = transform.position + capsule.center;
        topCapsulePosition    = capsuleCenterPosition + Vector3.up * cilinderHalfLenght * capsuleHeightMultiplier;
        bottomCapsulePosition = capsuleCenterPosition - Vector3.up * cilinderHalfLenght * capsuleHeightMultiplier;

        currentWallJumpForce = wallJumpForce;
        freezeTimer = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetInvertedControl(bool invertedControlOn)
    {
        this.invertedControl = invertedControlOn;
    }

    public void setForce_Congelamento(float force)
    {
        this.frozenForceInputMag -= force;
    }

    public void setBool_Congelado(bool frozen)
    {
        this.isFrozen = frozen;
    }

    public void setTempo_Recuperacao(float time)
    {
        this.freezeRecoveryTime = time; 
    }

    public void setValorParaRecuperar(float value)
    {
        this.forceRecoveryValue = value;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void CheckGrounded()
    {
        RaycastHit hit;

        if (Physics.CapsuleCast(topCapsulePosition, bottomCapsulePosition, capsuleRadius * capsuleRadiusMultiplier,
                                Vector3.down, out hit, groundCheckDistance, groundLayerMask))
        {
            groundTag = hit.collider.tag;
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    void Update()
    {
        // -- Updating variables for Physics.CapsuleCast() --
        capsuleCenterPosition = transform.position + capsule.center;
        topCapsulePosition    = capsuleCenterPosition + Vector3.up * cilinderHalfLenght * capsuleHeightMultiplier;
        bottomCapsulePosition = capsuleCenterPosition - Vector3.up * cilinderHalfLenght * capsuleHeightMultiplier;

        // -- Evaluating if player should jump --
        float timeSinceJump = Time.time - timeJumped;
        CheckGrounded();

        if (isGrounded)
            currentWallJumpForce = wallJumpForce;

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
                targetSpeed = minMidairTargetSpeed * midairAccelAllowed;
            else
                targetSpeed = last_xzSpeedOnGround * midairAccelAllowed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
            targetSpeed = runSpeed;
        else
            targetSpeed = walkSpeed;

        // -- Checking if ground or air control --
        float moveInputMag;

        if (isFrozen) // DEBUG
            moveInputMag = frozenForceInputMag;
        else if (isGrounded)
            moveInputMag = groundInputMag;
        else
            moveInputMag = airInputMag;

        // -- Calculating movement direction from player input --
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveInput = new Vector3(playerInput[0], 0f, playerInput[1]);
        moveInput = moveInput.normalized * moveInputMag;
        moveInput = transform.TransformDirection(moveInput);

        if (invertedControl)
            moveInput = (-1) * moveInput;

        // -- Checking if is moving towards wall midair --
        Vector3 checkDirection = moveInput;

        if (moveInput == Vector3.zero)
            checkDirection = Vector3.up;

        wallApproach = (Physics.CapsuleCast(topCapsulePosition, bottomCapsulePosition, capsuleRadius * capsuleRadiusMultiplier,
                             checkDirection, out wallHit, wallOffset) && !isGrounded);

        // -- Animation --
        if (!isGrounded)
            playerInput = Vector2.zero;

        playerAnimator.SetFloat("InputH", playerInput[0]);
        playerAnimator.SetFloat("InputV", playerInput[1]);

        // -- Setting freeze mechanics --
        if (isFrozen)
        {
            freezeTimer += Time.deltaTime;
            if (freezeTimer >= freezeRecoveryTime)
            {
                frozenForceInputMag += forceRecoveryValue;
                isFrozen = false;
            }
        }
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
            float newSpeed = xzVelocity.magnitude;

            newSpeed *= 1 / Mathf.Pow(groundMoveDamp, Time.deltaTime);

            if (newSpeed < 0.01f)
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

        // -- Wall jumping and fixing wall sticking --

        if (wallApproach)
        {
            Vector3 wallNormal = wallHit.normal;

            if (hasJumped && currentWallJumpForce > 0f && wallJumpActive)
            {
                float angleInRad = wallJumpAngle * Mathf.Deg2Rad;

                Vector3 wallJumpDirection = wallNormal * Mathf.Cos(angleInRad) + Vector3.up * Mathf.Sin(angleInRad);
                rb.AddForce(wallJumpDirection * currentWallJumpForce, ForceMode.VelocityChange);

                currentWallJumpForce *= wallJumpDamp;
                if (currentWallJumpForce < 0.01f)
                    currentWallJumpForce = 0f;

                hasJumped = false;
            }
            else
            {
                Vector3 nextPosition = transform.position + rb.velocity * Time.deltaTime;
                float dist2wall = (transform.position - wallHit.point).magnitude;
                float nextDist2wall = (nextPosition - wallHit.point).magnitude;

                if (dist2wall < wallOffset * 0.75f)
                    transform.position = transform.position + wallNormal * wallOffset * 0.1f;
                
                if (nextDist2wall < wallOffset)
                    rb.AddForce(-moveInput, ForceMode.Acceleration);
            }
        }
    }
}