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

    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
    Vector2 speed;

    [SerializeField] Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new Playercontrols();   
        input.Enable();
    }

    private void Start()
    {
        rb.freezeRotation = true;
        input.GamePlay.Move.performed += MovePlayer;
        readyToJump = true;
    }

    private void Update()
    {
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

        if(Input.GetKey(jumpKey) && readyToJump && grounded) 
        { 
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }
    private void FixedUpdate()
    {
        if(isMoving)
        {
            moveDirection = orientation.forward * speed.y + orientation.right * speed.x;

            Debug.Log(readyToJump);

            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);  
            }
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
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

        animator.SetBool("isRunning", isMoving);
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
    }
}
