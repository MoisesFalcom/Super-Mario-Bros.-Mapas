using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void UpdateMovementAnimations(Vector2 velocity)
{
    float speedX = Mathf.Abs(velocity.x);
    animator.SetFloat("Speed", speedX);

}


public void SetIsGrounded(bool grounded)
{
    animator.SetBool("isGrounded", grounded);
}

    public void PlayJump()
    {
        animator.SetTrigger("jump");
    }

    
}
