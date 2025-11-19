using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityScale = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundPlayer;
    private Vector3 move;

    private MyInputMap mIM;

    private Vector2 inputData;

    private bool isJumping = false;
    private bool isRunning = false;

    public Transform cameraTransform;

    public float charaterRotationSpeed = 10.0f;

    public Animator playerAnim;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();

        mIM = new MyInputMap();

        mIM.PlayerWorld.Movement.performed += Movement_performed =>
        {
            inputData = Movement_performed.ReadValue<Vector2>();
        };

        mIM.PlayerWorld.Movement.canceled += Movement_canceled =>
        {
            inputData = Movement_canceled.ReadValue<Vector2>();
        };

        mIM.PlayerWorld.Jump.performed += Jump_performed =>
        {
            isJumping = Jump_performed.ReadValueAsButton();
        };

        mIM.PlayerWorld.Jump.canceled += Jump_canceled =>
        {
            isJumping = Jump_canceled.ReadValueAsButton();
        };

        mIM.PlayerWorld.Run.performed += Run_performed =>
        {
            isRunning = Run_performed.ReadValueAsButton();
        };

        mIM.PlayerWorld.Run.canceled += Run_canceled =>
        {
            isRunning = Run_canceled.ReadValueAsButton();
        };
    }

    private void OnEnable()
    {
        mIM.Enable();
    }

    private void OnDisable()
    {
        mIM.Disable();
    }

    private void Update()
    {
        groundPlayer = controller.isGrounded;

        if (groundPlayer && playerVelocity.y < 0.0f)
        {
            playerVelocity.y = 0.0f;
        }

        if (cameraTransform != null)
        {
            Vector3 camForward = cameraTransform.forward;

            camForward.y = 0.0f;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;

            camRight.y = 0.0f;
            camRight.Normalize();

            move = camForward * inputData.y + camRight * inputData.x;
        }
        else
        {
            move = transform.forward * inputData.y + transform.right * inputData.x;
        }

        move = Vector3.ClampMagnitude(move, 1f);

        if (move.sqrMagnitude > 0.0001f)
        {
            playerAnim.SetBool("IsWalking", true);
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * charaterRotationSpeed);

            if (isRunning)
            {
                playerAnim.SetBool("IsRunning", true);
                playerSpeed = 4f;
            }
            else
            {
                playerSpeed = 2f;
                playerAnim.SetBool("IsRunning", false);
            }
        }
        else
        {
            playerAnim.SetBool("IsWalking", false);
            if (playerAnim.GetBool("IsRunning"))
            {
                playerSpeed = 2f;
                playerAnim.SetBool("IsRunning", false);
            }
        }

        if (isJumping && groundPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -1.0f * gravityScale);
        }

        playerVelocity.y += gravityScale * Time.deltaTime;

        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
