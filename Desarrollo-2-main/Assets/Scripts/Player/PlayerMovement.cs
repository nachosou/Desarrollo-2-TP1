using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;

    private bool readyToJump;
    private bool grounded;
    private bool isMoving;
    private bool isJumping;
    private bool isFalling;
    private Vector2 direction;

    [SerializeField] PlayerInput input;
    private Rigidbody rb;
    private AnimationHandler animationHandler;
    public Transform orientation;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        animationHandler = GetComponent<AnimationHandler>();
        input.currentActionMap.FindAction("Jump").started += PlayerMovement_performed;
        input.currentActionMap.FindAction("Move").performed += PlayerMovement_started;
        input.currentActionMap.FindAction("Move").canceled += PlayerMovement_canceled;
    }

    /// <summary>
    /// Called when the move input action is performed
    /// </summary>
    private void PlayerMovement_started(InputAction.CallbackContext obj)
    {
        Vector2 move = obj.ReadValue<Vector2>();
        isMoving = (move.magnitude > 0.5f);
        direction = isMoving ? move.normalized : Vector2.zero;

        animationHandler.SetRunBoolAnimation(isMoving);
    }

    /// <summary>
    /// Called when the move input action is canceled
    /// </summary>
    private void PlayerMovement_canceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;

        animationHandler.SetRunBoolAnimation(false);
    }

    /// <summary>
    /// Controls player speed to prevent exceeding maximum movement speed
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    /// <summary>
    /// Updates player state and applies physics calculations
    /// </summary>
    private void Update()
    {
        HandleGroundedState();
        HandleMovement();
        SpeedControl();
    }

    private void OnDisable()
    {
        if (input.currentActionMap != null)
        {
            input.currentActionMap.FindAction("Jump").started -= PlayerMovement_performed;
            input.currentActionMap.FindAction("Move").performed -= PlayerMovement_started;
            input.currentActionMap.FindAction("Move").canceled -= PlayerMovement_canceled;
        }
    }

    /// <summary>
    /// Checks and updates player's grounded state
    /// </summary>
    private void HandleGroundedState()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        rb.drag = grounded ? groundDrag : 0f;

        animationHandler.SetFallingBoolAnimation(!grounded);
    }

    /// <summary>
    /// Handles player movement based on current state (grounded or in air)
    /// </summary>
    private void HandleMovement()
    {
        if (isMoving)
        {
            Vector3 moveDirection = orientation.forward * direction.y + orientation.right * direction.x;

            float forceMultiplier = grounded ? 10f : 5f * airMultiplier;
            rb.AddForce(moveDirection.normalized * (moveSpeed * forceMultiplier), ForceMode.Force);
        }
    }

    /// <summary>
    /// Called when the jump input action is performed
    /// </summary>
    private void PlayerMovement_performed(InputAction.CallbackContext obj)
    {
        TryJump();
    }

    /// <summary>
    /// Initiates a jump action if conditions are met
    /// </summary>
    private void TryJump()
    {
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            isJumping = true;

            animationHandler.SetJumpBoolAnimation(isJumping);

            Invoke(nameof(Jump), isMoving ? 0f : 0.3f);
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    /// <summary>
    /// Performs the jump action by applying vertical force
    /// </summary>
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Resets the jump state after a cooldown period
    /// </summary>
    private void ResetJump()
    {
        readyToJump = true;
        isJumping = false;

        animationHandler.SetJumpBoolAnimation(isJumping);
    }
}
