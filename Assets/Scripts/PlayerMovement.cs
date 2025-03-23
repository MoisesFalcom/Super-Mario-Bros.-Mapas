using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    private Vector2 movementInput;
    private bool isFacingRight = true;

    [Header("Configuración de Salto")]
    public int maxJumps = 1;
    public float jumpPower = 10f;
    private int jumpsRemaining;
    private bool isGrounded;

    [Header("Física y Gravedad")]
    public float baseGravity = 1f;
    public float fallGravityMult = 2.5f;
    public float maxFallSpeed = 10f;

    [Header("Referencias")]
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
    public InputAction moveAction;
    public InputAction jumpAction;

    // 🔄 Referencia al controlador de animaciones
    private PlayerAnimatorController animatorController;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Start()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
    }

    private void Update()
    {
        GroundCheck();
        movementInput = moveAction.ReadValue<Vector2>();

        // Movimiento horizontal
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        Flip();


        // 🔄 Actualizar animaciones con base en física real
        if (animatorController != null)
{
    animatorController.UpdateMovementAnimations(rb.linearVelocity);
    animatorController.SetIsGrounded(isGrounded);
}


        // Salto
        if (jumpAction.triggered && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpsRemaining--;

            if (animatorController != null)
            {
                animatorController.PlayJump();
            }
        }
    }

    private void FixedUpdate()
    {
        ProcessGravity();
    }

    private void GroundCheck()
    {
        bool wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            jumpsRemaining = Mathf.Max(1, maxJumps);
        }
    }

    private void ProcessGravity()
    {
        float verticalVelocity = rb.linearVelocity.y;

        // Cambia la gravedad si está cayendo
        rb.gravityScale = (verticalVelocity < -0.01f)
            ? baseGravity * fallGravityMult
            : baseGravity;

        // Limita velocidad máxima de caída
        if (verticalVelocity < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }

    private void Flip()
    {
        if (isFacingRight && movementInput.x < 0 || !isFacingRight && movementInput.x > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
