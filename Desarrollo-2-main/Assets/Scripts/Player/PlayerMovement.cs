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

    bool readyToJump;

    public KeyCode jumpKey = KeyCode.Space;

    public Playercontrols input;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    bool isMoving;
    bool isJumping;
    bool isFalling;

    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
    Vector2 speed;

    public ProjectilesSO projectileData;
    private ProjectilesFactory projectileFactory;

    AnimationHandler animationHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animationHandler = GetComponent<AnimationHandler>();
        input = new Playercontrols();   
        input.Enable();
        projectileFactory = new ProjectilesFactory(projectileData);
    }

    private void Start()
    {
        rb.freezeRotation = true;
        input.GamePlay.Move.performed += MovePlayer;
        readyToJump = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireProjectile();
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SpeedControl();

        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if(!grounded) 
        {
            animationHandler.SetFallingBoolAnimation(!isFalling);
        }
        else
        {
            animationHandler.SetFallingBoolAnimation(isFalling);
        }

        if(Input.GetKey(jumpKey) && readyToJump && grounded) 
        { 
            readyToJump = false;

            isJumping = true;

            animationHandler.SetJumpBoolAnimation(isJumping);

            if (isMoving) 
            {
                Jump();            
            }
            else
            {
                Invoke(nameof(Jump), 0.3f);
            }
            
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            moveDirection = orientation.forward * speed.y + orientation.right * speed.x;

            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);  
            }
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 5f * airMultiplier), ForceMode.Force);
            }
        }
    }

    private void OnDestroy()
    {
        input.GamePlay.Move.performed -= MovePlayer;
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();

        isMoving = (move.magnitude > 0.5f);
        speed = isMoving ? move:Vector2.zero;

        animationHandler.SetRunBoolAnimation(isMoving);
    }

    private void FireProjectile()
    {
        Vector3 position = transform.position + transform.forward;
        Quaternion rotation = transform.rotation;
        projectileFactory.CreateProjectile(position, rotation);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }  
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }    

    private void ResetJump()
    {
        readyToJump = true;

        isJumping = false;

        animationHandler.SetJumpBoolAnimation(isJumping);
    }
}
