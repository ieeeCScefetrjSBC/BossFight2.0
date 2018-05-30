using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour
{

    public AudioSource moveAudio;
    //public Animator playerAnim;
    public float groundCheckDistance = 1.1f;
    public float validJumpTime = 0.1f;

    public float velocityInputMag = 4.75f;
    public float accelInputMag = 20f;

    public float walkSpeed = 3f;
    public float runSpeed = 15f;
    public float jumpSpeed = 15f;
    public float platformJumpSpeed = 30f;
    public float minMidairTargetSpeed = 5f;
    public float midairAccelFactor = 1.20f;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private float capsuleRadius;
    private float cilinderHalfLenght;
    private float capsuleExtensionMultiplier = 1.01f;

    private Vector3 last_xzVelocityOnGround = Vector3.zero;
    private float timeJumped = 0f;

    private string groundTag;

    private bool isGrounded = false;
    private bool isMoving = false;
    private bool wasMoving = false;
    private bool runKeyPressed = false;
    private bool hasJumped = false;
    private bool invertedControl = false;

    // Use this for initialization
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

        //Debug.Log("lalalala");
        //Debug.Log("transform.position: " + transform.position);
        //Debug.Log("topCapsulePosition: " + topCapsulePosition);
        //Debug.Log("bottomCapsulePosition: " + bottomCapsulePosition);
        //Debug.Log("collidersTouched Lenght: " + collidersTouched.Length);
        //if (collidersTouched.Length > 0)
        //{
        //    Debug.Log("collidersTouched[0].gameObject.name: " + collidersTouched[0].gameObject.name);
        //    Debug.Log("AEW POURRAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!");
        //}

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
        float timeSinceJump = Time.time - timeJumped;
        CheckGrounded();

        if (timeSinceJump > validJumpTime)
            hasJumped = false;

        if (Input.GetButtonDown("Jump"))
        {
            timeJumped = Time.time;
            hasJumped = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 moveInput;
        Vector3 xzVelocity;
        float moveInputMag;
        float targetSpeed;

        // -- Setting move speed constraint --
        if (!isGrounded)
        {
            float last_xzSpeedOnGround = last_xzVelocityOnGround.magnitude;
            if (last_xzSpeedOnGround < minMidairTargetSpeed)
                targetSpeed = minMidairTargetSpeed * midairAccelFactor;
            else
                targetSpeed = last_xzVelocityOnGround.magnitude * midairAccelFactor;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
            targetSpeed = runSpeed;
        else
            targetSpeed = walkSpeed;

        // -- Check if ground or air control --
        if (isGrounded)
            moveInputMag = velocityInputMag;
        else
            moveInputMag = accelInputMag;

        // -- Calculate movement direction from player input --
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveInput = moveInput.normalized * moveInputMag;
        moveInput = transform.TransformDirection(moveInput);

        // -- Applying horizontal movement --
        if (isGrounded)
            rb.AddForce(moveInput, ForceMode.VelocityChange);
        else
            rb.AddForce(moveInput, ForceMode.Acceleration);

        // -- Fixing velocity if above target speed
        xzVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (isGrounded)
            last_xzVelocityOnGround = xzVelocity;

        if (xzVelocity.magnitude > targetSpeed)
            rb.velocity = xzVelocity.normalized * targetSpeed + new Vector3(0f, rb.velocity.y, 0f);

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