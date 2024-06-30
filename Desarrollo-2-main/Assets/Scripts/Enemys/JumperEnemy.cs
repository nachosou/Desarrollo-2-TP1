using UnityEngine;

public class JumperEnemy : Enemy
{
    public float jumpForce;
    float jumpCoolDown;

    public Rigidbody rb;

    private void Update()
    {
        HandleJumpCooldown();
    }

    /// <summary>
    /// Handles the jump cooldown and triggers the jump if the cooldown has elapsed
    /// </summary>
    private void HandleJumpCooldown()
    {
        jumpCoolDown += Time.deltaTime;

        if (jumpCoolDown >= 3)
        {
            Jump();
            jumpCoolDown = 0;
        }
    }

    /// <summary>
    /// Makes the enemy jump by applying an upward force
    /// </summary>
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
