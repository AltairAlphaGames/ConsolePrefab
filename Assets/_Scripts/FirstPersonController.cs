using System;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    [SerializeField] private float sprintLimit = 100f;
    [SerializeField] private float sprintDrain = 1.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;

    [Header("Inputs Customisation")]
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";
    [SerializeField] private string jumpInput = "Jump";
    [SerializeField] private string sprintInput = "Sprint";
    [SerializeField] private string interactionInput = "Interaction";

    [Header("Footstep Sounds")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float sprintStepInterval = 0.3f;
    [SerializeField] private float velocityThreshold = 2.0f;

    private int lastPlayedIdx = -1;
    private bool isMoving;
    private float nextStepTime;
    private Camera mainCamera;
    private float verticalRotation;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Prevent movement when the menu is open
        if (IsMenuOpen())
        {
            return;  // Exit Update if menu is open
        }

        HandleMovement();
        HandleRotation();
        HandleFootsteps();
        HandleInteraction();
    }

    Boolean IsMenuOpen()
    {
        GameObject[] menuList = GameObject.FindGameObjectsWithTag("tgmenu");

        if (menuList == null)
        {
            return false;
        }
        
        foreach (GameObject obj in menuList)
        {
            if (obj.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    void HandleInteraction()
    {
        bool intInput = Input.GetButtonUp(interactionInput);

        if (!intInput)
        {
            return;
        }


        //Execute interaction routine for object
        Debug.Log("Interaction Key Pressed");
    }

    void HandleMovement()
    {
        float vertInput = Input.GetAxis(verticalMoveInput);
        float horzInput = Input.GetAxis(horizontalMoveInput);
        bool spInput = Input.GetButton(sprintInput);

        float speedMultiplier = 1f;
        if (spInput && sprintLimit > 0f)
        {
            sprintLimit -= sprintDrain;
            speedMultiplier = sprintMultiplier;
        }

        float verticalSpeed = vertInput * walkSpeed * speedMultiplier;
        float horizontalSpeed = horzInput * walkSpeed * speedMultiplier;

        Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        HandleGravityAndJumping();

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        characterController.Move(currentMovement * Time.deltaTime);

        isMoving = vertInput + horzInput != 0;
    }

    void HandleGravityAndJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (Input.GetButton(jumpInput))
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }

    }

    void HandleRotation()
    {
        float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;

        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis(MouseYInput) * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0 , 0);
    }

    void HandleFootsteps()
    {
        float currentStepInterval = (Input.GetButton(sprintInput) && sprintLimit > 0f ? sprintStepInterval : walkStepInterval);

        if (characterController.isGrounded && isMoving && Time.time > nextStepTime && characterController.velocity.magnitude > velocityThreshold)
        {
            PlayFootstep();
            nextStepTime = Time.time + currentStepInterval;
        }
    }

    void PlayFootstep()
    {
        int randomIdx = 0;

        if (footstepSounds.Length > 1)
        {
            randomIdx = UnityEngine.Random.Range(0, footstepSounds.Length);
           
            if (randomIdx >= lastPlayedIdx)
            {
                randomIdx++;
            }
            
            if (randomIdx > footstepSounds.Length - 1)
            {
                randomIdx = 0;
            }
        }

        lastPlayedIdx = randomIdx;
        footstepSource.clip = footstepSounds[randomIdx];
        footstepSource.Play();
    }
}
