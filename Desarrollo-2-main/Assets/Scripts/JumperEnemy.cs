using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : Enemy
{
    public float jumpForce;
    float jumpCoolDown;

    public Rigidbody rb;

    private void Update()
    {
        jumpCoolDown += Time.deltaTime;

        if (jumpCoolDown >= 3)
        {
            Jump();

            jumpCoolDown = 0;
        }
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
