using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

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
    }
    private void FixedUpdate()
    {
        if(isMoving)
        {
            moveDirection = orientation.forward * speed.y + orientation.right * speed.x;

            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
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
        Debug.Log(move.magnitude);

        isMoving = (move.magnitude > 0.5f);
        speed = isMoving ? move:Vector2.zero;
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
}
