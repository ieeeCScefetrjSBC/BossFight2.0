using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float sensitivity = 2f;
    public GameObject eyes;


    float rotX;
    float rotY;

    private float speed = 6.0F;
    private float jumpSpeed = 25.0F;
    private float gravity = 45.0F;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
        
                //Movimentação do Player
                CharacterController controller = GetComponent<CharacterController>();
                if (controller.isGrounded)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        speed = 15.0F;
                    }
                    else { speed = 9.0F; }

                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= speed;

                    if (Input.GetButton("Jump"))
                        moveDirection.y = jumpSpeed;

                }
                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);

            }
    }

