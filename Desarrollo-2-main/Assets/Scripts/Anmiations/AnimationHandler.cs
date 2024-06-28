using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ExecuteRunAnimation(bool isMoving)
    {
        animator.SetBool("isRunning", isMoving);
    }

    public void ExecuteAmingAnimation(bool isAming)
    {
        animator.SetBool("isAming", isAming);
    }
}
