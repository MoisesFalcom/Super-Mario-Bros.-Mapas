// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Controlador dedicado a manejar los parámetros de animación del jugador.

using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    // Inicializa las referencias a componentes al despertar el objeto
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Actualiza la animación de movimiento con base en la velocidad horizontal
    public void UpdateMovementAnimations(Vector2 velocity)
    {
        float speedX = Mathf.Abs(velocity.x);
        animator.SetFloat("Speed", speedX);
    }

    // Establece si el jugador está en el suelo para controlar la animación correspondiente
    public void SetIsGrounded(bool grounded)
    {
        animator.SetBool("isGrounded", grounded);
    }

    // Activa la animación de salto por medio de un trigger
    public void PlayJump()
    {
        animator.SetTrigger("jump");
    }
}

