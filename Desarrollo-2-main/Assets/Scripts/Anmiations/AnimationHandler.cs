using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    /// <summary>
    /// Sets the running animation state
    /// </summary>
    public void SetRunBoolAnimation(bool isMoving)
    {
        animator.SetBool("isRunning", isMoving);
    }

    /// <summary>
    /// Sets the aiming animation state
    /// </summary>
    public void SetAmingBoolAnimation(bool isAming)
    {
        animator.SetBool("isAming", isAming);
    }

    /// <summary>
    /// Sets the jumping animation state
    /// </summary>
    public void SetJumpBoolAnimation(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }

    /// <summary>
    /// Sets the falling animation state
    /// </summary>
    public void SetFallingBoolAnimation(bool isFalling)
    {
        animator.SetBool("isFalling", isFalling);
    }
}
