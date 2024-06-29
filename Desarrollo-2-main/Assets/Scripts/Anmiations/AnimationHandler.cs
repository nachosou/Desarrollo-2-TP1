using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetRunBoolAnimation(bool isMoving)
    {
        animator.SetBool("isRunning", isMoving);
    }

    public void SetAmingBoolAnimation(bool isAming)
    {
        animator.SetBool("isAming", isAming);
    }

    public void SetJumpBoolAnimation(bool isJumping) 
    {
        animator.SetBool("isJumping", isJumping);
    }

    //public void ExecuteRunningJumpAnimation(bool isJumping)
    //{
    //    animator.SetBool("isJumping", isJumping);
    //}
}
