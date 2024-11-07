using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 3f;
    public float runSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    // Variables de stamina
    public Image staminaBar;      // Arrastra la barra de degradado en el Inspector
    public float maxStamina = 5f; // Duración máxima de la energía para correr en segundos
    public float staminaRechargeDelay = 2f; // Tiempo de espera para recargar la energía

    private float stamina;
    private bool isStaminaDepleted;
    private float staminaRechargeTimer;

    // Control de movimiento
    public bool canMove { get; private set; } = true;

    void Start()
    {
        stamina = maxStamina; // Inicializa con la energía máxima
    }

    void Update()
    {
        if (!canMove) return;

        // Verificar si estamos tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Obtener los inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Control de carrera y reducción de stamina
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !isStaminaDepleted)
        {
            speed = runSpeed;
            stamina -= Time.deltaTime; // Reducir la energía mientras corre

            // Si se agota la energía, activar cooldown de recarga
            if (stamina <= 0)
            {
                stamina = 0;
                isStaminaDepleted = true;
                staminaRechargeTimer = staminaRechargeDelay; // Inicia el tiempo de espera
            }
        }
        else
        {
            speed = 3f; // Velocidad normal

            // Iniciar recarga de energía después del cooldown
            if (isStaminaDepleted)
            {
                // Solo comienza el tiempo de espera si se ha soltado Shift
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    staminaRechargeTimer -= Time.deltaTime;
                    if (staminaRechargeTimer <= 0)
                    {
                        isStaminaDepleted = false; // Permitir recargar
                    }
                }
            }
            else if (stamina < maxStamina)
            {
                stamina += Time.deltaTime; // Recuperar energía
                if (stamina > maxStamina) stamina = maxStamina;
            }
        }

        // Actualizar el degradado visual de la barra de stamina
        UpdateStaminaBar();

        // Mover personaje
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = stamina / maxStamina;
        }
    }

    public void AllowMovement() { canMove = true; }
    public void PreventMovement() { canMove = false; }
}
