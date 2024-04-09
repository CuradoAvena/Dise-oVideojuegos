using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{
    // Variables a utilizar
    public CharacterController controller;
    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    public bool canMove { get; private set; } = true;

    void Update()
    {
        if (!canMove) return;

        // Estoy pisando a groundMask?
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Ubicar Inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Setear Mover personaje
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Setear Brinco personaje
      //si... (la condicion a cumplir)
      //si presiono la tecla barra espaciadora y estoy en el piso
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // lo que pasa si se cumle la condicion
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Setear la gravedad personaje
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void AllowMovement() { canMove = true; }
    public void PreventMovement() { canMove = false; }
}
