using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovedControlls : MonoBehaviour
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
    float rotationfactorPerFrame = 15.0f;
    float runMultiplier = 3.0f;
    int zero = 0;
    float gravity = -9.8f;
    float groundedGravity = -.05f;
   
    bool isJumpPressed = false;
    [SerializeField] private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = .01f;
    [SerializeField] private float maxJumpTime = 0.2f;
    bool isJumping = false;

    private Vector3 PosBeforeJump;
    private bool JumpApexReached = false;
 

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

        //setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        Debug.Log("Start Velocity of jump : " + initialJumpVelocity);
    }

    void handleJump()
    {
        if (!isJumpPressed)
            return;
        /*float currentPos = this.transform.position.y;
        if (!isJumping && characterController.isGrounded && isJumpPressed )
        {
            animator.SetBool("isJumping", true);
            //if(!isJumping)
            PosBeforeJump = this.transform.position;
            isJumping = true;
            currentMovement.y += initialJumpVelocity * .05f;
            currentRunMovement.y = initialJumpVelocity * .05f;
           
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded) 
        { 
            isJumping = false;
        }*/
        //float currentPos = this.transform.position.y;
        /*
        if (!isJumping && characterController.isGrounded)
        {
            
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            Debug.Log(currentMovement.y);
            //currentRunMovement.y = initialJumpVelocity * .05f;

        }
        else 
        {
            isJumping = false;
        }
        */

        //currentMovement.y = initialJumpVelocity;

        currentMovement.y = initialJumpVelocity;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void onJump (InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        animator.SetBool("isJumping", true);
        PosBeforeJump = this.transform.position;
        //JumpApexReached = false;
        //Debug.Log(isJumpPressed);
    }
      
  
    void onRun (InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
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
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
           transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationfactorPerFrame * Time.deltaTime);
        }
        
    }

    void onMovementInput (InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        currentMovementInput = context.ReadValue<Vector2>();
        
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier; 
        isMovementPressed = currentMovementInput.x != zero || currentMovementInput.y != zero;
        
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
        /*
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else 
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .05f;
            currentMovement.y += nextYVelocity;
            currentRunMovement.y += nextYVelocity;
        }*/

        if (!(this.transform.position.y < PosBeforeJump.y + maxJumpHeight))
        {
            currentMovement.y = -initialJumpVelocity *1.2f;
            
            if (animator.GetBool("isJumping"))
            {
                animator.SetBool("isJumping", false);
                isJumping = false;
            }
                    
        }
        characterController.Move(currentMovement * Time.deltaTime);
    } 
  
    void Update()
    {
        
        handleRotation();
        handleAnimation();
        

        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else if (characterController.isGrounded)
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
