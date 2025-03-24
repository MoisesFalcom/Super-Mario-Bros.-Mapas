// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Controlador de movimiento y salto del jugador usando Input System.

using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovementController : MonoBehaviour
{
    // Parámetros de movimiento
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float JumpForce = 6f;

    // Entrada del jugador
    private float HorizontalInput;

    // Input System
    public InputAction MoveInputAction;
    public InputAction JumpInputAction;

    // Estado del jugador
    private bool IsFacingRight = false;
    private bool IsGrounded = false;

    // Componentes del GameObject
    private Rigidbody2D RigidBody;
    private Animator AnimatorController;

    private void OnEnable()
    {
        MoveInputAction.Enable();
        JumpInputAction.Enable();
    }

    private void OnDisable()
    {
        MoveInputAction.Disable();
        JumpInputAction.Disable();
    }

    private void Start()
    {
        // Referencia a componentes de físicas y animaciones
        RigidBody = GetComponent<Rigidbody2D>();
        AnimatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        // Lectura de la entrada horizontal
        HorizontalInput = MoveInputAction.ReadValue<Vector2>().x;

        // Volteo de sprite si cambia dirección
        FlipCharacter();

        // Verificación para salto
        if (JumpInputAction.triggered && IsGrounded)
        {
            RigidBody.linearVelocity = new Vector2(RigidBody.linearVelocity.x, JumpForce);
            IsGrounded = false;
            AnimatorController.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        // Movimiento horizontal
        RigidBody.linearVelocity = new Vector2(HorizontalInput * MoveSpeed, RigidBody.linearVelocity.y);

        // Actualización de valores de animación
        AnimatorController.SetFloat("xVelocity", Math.Abs(RigidBody.linearVelocity.x));
        AnimatorController.SetFloat("yVelocity", RigidBody.linearVelocity.y);
    }

    // Lógica para invertir la dirección del personaje visualmente
    private void FlipCharacter()
    {
        if ((IsFacingRight && HorizontalInput < 0f) || (!IsFacingRight && HorizontalInput > 0f))
        {
            IsFacingRight = !IsFacingRight;

            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * (IsFacingRight ? 1 : -1),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    // Detección de contacto con el suelo para permitir salto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
        AnimatorController.SetBool("isJumping", false);
    }
}
