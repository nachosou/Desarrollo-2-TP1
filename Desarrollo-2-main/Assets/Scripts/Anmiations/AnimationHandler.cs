using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ExecuteRunAnimation(bool isMoving)
    {
        animator.SetBool("isRunning", isMoving);
    }
}
