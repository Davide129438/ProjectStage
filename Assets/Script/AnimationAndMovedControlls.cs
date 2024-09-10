using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMoveControls : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 3.0f;
    int zero = 0;
    float gravity = -9.8f;
    float groundedGravity = -.05f;

    bool isJumpPressed = false;
    [SerializeField] private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = 1.0f;
    [SerializeField] private float maxJumpTime = 0.5f;
    bool isJumping = false;

    float currentJumpVelocity;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if (isJumpPressed && !isJumping && characterController.isGrounded)
        {
            isJumping = true;
            currentJumpVelocity = initialJumpVelocity;

            currentMovement.y = currentJumpVelocity;
            currentRunMovement.y = currentJumpVelocity;

            animator.SetBool("isJumping", true);
        }
        else if (isJumping)
        {
            currentJumpVelocity += gravity * Time.deltaTime;
            currentMovement.y = currentJumpVelocity;
            currentRunMovement.y = currentJumpVelocity;

            if (currentMovement.y < 0 && characterController.isGrounded)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
                currentMovement.y = groundedGravity;
                currentRunMovement.y = groundedGravity;
            }
        }
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    // Metodo aggiornato per il movimento relativo alla telecamera
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();

        // Ottieni i vettori di direzione della telecamera
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Annulla l'asse Y per ottenere solo il piano orizzontale
        forward.y = 0f;
        right.y = 0f;

        // Normalizza i vettori
        forward.Normalize();
        right.Normalize();

        // Calcola il movimento basato sugli input e la direzione della telecamera
        currentMovement = forward * currentMovementInput.y + right * currentMovementInput.x;
        currentRunMovement = currentMovement * runMultiplier;

        isMovementPressed = currentMovementInput.x != zero || currentMovementInput.y != zero;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            // Ruota verso la direzione del movimento
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded && !isJumping)
        {
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if (!characterController.isGrounded)
        {
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    void Update()
    {
        handleRotation();
        handleAnimation();

        // Muovi il personaggio in base al movimento e allo stato di corsa
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        handleGravity();
        handleJump();
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
