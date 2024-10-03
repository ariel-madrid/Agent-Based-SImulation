using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentAgent : MonoBehaviour
{
    public float moveSpeed = 10.0f;        // Velocidad de movimiento del estudiante
    public float changeDirectionTime = 2.0f; // Tiempo para cambiar de dirección
    public float movementRadius = 50f;   // Radio de movimiento alrededor de la posición inicial

    private Vector3 initialPosition;      // Posición inicial del estudiante
    private Vector3 moveDirection;        // Dirección de movimiento actual
    private float timer;                  // Temporizador para cambiar dirección

    void Start()
    {
        // Guardar la posición inicial del estudiante
        initialPosition = transform.position;

        // Inicializar el temporizador y la dirección aleatoria inicial
        timer = changeDirectionTime;
        SetRandomDirection();
    }

    void Update()
    {
        // Mover al estudiante en la dirección actual
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Comprobar si se debe cambiar de dirección
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetRandomDirection();
            timer = changeDirectionTime;
        }

        // Limitar el movimiento del estudiante dentro de su radio
        StayWithinRadius();
    }

    // Establecer una dirección de movimiento aleatoria
    void SetRandomDirection()
    {
        // Generar una dirección aleatoria en el plano XZ
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0, randomZ).normalized;
    }

    // Mantener al estudiante dentro de su radio de movimiento
    void StayWithinRadius()
    {
        Vector3 offset = transform.position - initialPosition;

        // Si el estudiante sale del radio, lo redirigimos de vuelta
        if (offset.magnitude > movementRadius)
        {
            // Direccionar el movimiento de vuelta hacia la posición inicial
            Vector3 directionBack = (initialPosition - transform.position).normalized;
            moveDirection = directionBack;
        }
    }
}
