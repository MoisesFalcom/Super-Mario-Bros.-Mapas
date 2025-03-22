using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    private Vector2 movementInput;
    private bool isFacingRight = true;

    [Header("Configuración de Salto")]
    public int maxJumps = 1; // Número de saltos permitidos
    public float jumpPower = 10f;
    private int jumpsRemaining; // Saltos disponibles
    private bool isGrounded; // Indica si el personaje está tocando el suelo

    private Animator animator; 

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

    private void Update()
    {
        GroundCheck(); // Verifica si está en el suelo
        ProcessGravity(); // Aplica gravedad personalizada

        // Leer input del jugador
        movementInput = moveAction.ReadValue<Vector2>();

        // Movimiento en X
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        Flip();
        

        // Salto
        // Salto
if (jumpAction.triggered && jumpsRemaining > 0)
{
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    jumpsRemaining--; 

    if (animator != null) // 🔥 Verifica que animator no sea null antes de usarlo
    {
        animator.SetTrigger("jump");
    }
}

    }

    private void GroundCheck()
    {
        bool wasGrounded = isGrounded; // Guarda el estado anterior

        // Detecta si el personaje está tocando el suelo
        isGrounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);

        // Si aterriza en el suelo, reinicia los saltos disponibles
        if (isGrounded && !wasGrounded)
        {
            jumpsRemaining = Mathf.Max(1, maxJumps); // Asegura que al menos tenga 1 salto si maxJumps > 0
        }
    }

    private void ProcessGravity()
    {
        if (rb.linearVelocity.y < 0) // Si el personaje está cayendo
        {
            rb.gravityScale = baseGravity * fallGravityMult;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
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