using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Controller : MonoBehaviour
{
    public CharacterController characterController;

    private void Awake() 
    {
        if(gameObject.GetComponent<CharacterController>())
        {
            characterController = gameObject.GetComponent<CharacterController>();
            Debug.Log("Character Controller Found");
        }
        else 
        {
            characterController = gameObject.AddComponent<CharacterController>();
            Debug.Log("Character Controller Not Found.....Adding the component!");
        }
    }

    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpSpeed = 5f;
    public float gravity = 9.81f;
    private Vector3 moveDirection;
    private float verticalVelocity;

    private bool isSliding = false;
    private float slideTimer = 0f;
    private float slideDuration = 1.5f; // Adjust this as needed
    private float slideSpeed = 10f; // Adjust this as needed
    private float originalHeight;
    private Vector3 originalCenter;
    private float slideHeightTransitionDuration = 0.3f; // Adjust this as needed
    private float slideCenterTransitionDuration = 0.3f;
    public bool isCrouching = false;
    private float crouchHeight = 0.5f; // Adjust this as needed
    private float crouchCenter = 0.25f; // Adjust this as needed
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalHeight = characterController.height;
        originalCenter = characterController.center;
    }

    private void Update()
    {
        // Check for grounded state
        bool isGrounded = characterController.isGrounded;

        // Calculate movement direction based on input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput);
        Vector3 rotatedDirection = transform.TransformDirection(inputDirection);
        moveDirection = rotatedDirection * movementSpeed;

        // Apply gravity
        if (isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            // Jumping
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // Move the character controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Rotate the player based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        Vector3 currentRotation = transform.localEulerAngles;
        transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y + mouseX, currentRotation.z);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSlide();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
        // Continue sliding
        if (isSliding)
        {
            Slide();
        }

        // Check for slide end input (e.g., release left shift key)
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            EndSlide();
        }
    }
    private void StartSlide()
    {
        if (!isSliding)
        {
            isSliding = true;
            slideTimer = 0f;

            // Adjust character controller properties for sliding
            characterController.height = 0.5f * originalHeight;
            characterController.center = 0.5f * originalHeight * Vector3.up;
        }
    }

    private void Slide()
    {
        if (slideTimer < slideDuration)
        {
            // Apply forward force
            Vector3 slideDirection = transform.forward * slideSpeed;
            characterController.Move(slideDirection * Time.deltaTime);
            slideTimer += Time.deltaTime;
        }
        else
        {
            EndSlide();
        }
    }

    private void EndSlide()
    {
        if (isSliding)
        {
            isSliding = false;

            // Gradually transition back to the original height and center
            StartCoroutine(SmoothHeightTransition(originalHeight, slideHeightTransitionDuration));
            StartCoroutine(SmoothCenterTransition(originalCenter, slideCenterTransitionDuration));
        }
    }
    private void ToggleCrouch()
    {
        if (!isSliding)
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                characterController.height = crouchHeight;
                characterController.center = new Vector3(0f, crouchCenter, 0f);
            }
            else
            {
                characterController.height = originalHeight;
                characterController.center = originalCenter;
            }
        }
    }
    private IEnumerator SmoothHeightTransition(float targetHeight, float duration)
    {
        float startTime = Time.time;
        float initialHeight = characterController.height;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            characterController.height = Mathf.Lerp(initialHeight, targetHeight, t);
            yield return null;
        }

        characterController.height = targetHeight;
    }

    private IEnumerator SmoothCenterTransition(Vector3 targetCenter, float duration)
    {
        float startTime = Time.time;
        Vector3 initialCenter = characterController.center;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            characterController.center = Vector3.Lerp(initialCenter, targetCenter, t);
            yield return null;
        }

        characterController.center = targetCenter;
    }

}
