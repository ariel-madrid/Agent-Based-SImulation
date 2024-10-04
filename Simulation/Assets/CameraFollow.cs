using UnityEngine;

public class FollowStudent : MonoBehaviour
{
    public Transform student;         // Referencia al estudiante que la cámara debe seguir
    public Vector3 offset;            // Desplazamiento de la cámara respecto al estudiante
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento
    public float minDistance = 2.0f;   // Distancia mínima que la cámara debe mantener respecto al estudiante

    private Vector3 desiredPosition;  // Posición deseada de la cámara

    void LateUpdate()
    {
        // Posición objetivo de la cámara (la posición del estudiante + el offset)
        desiredPosition = student.position + offset;

        // Mantener la altura de la cámara igual a su posición original en Y
        desiredPosition.y = transform.position.y;

        // Verificar si hay obstáculos entre la cámara y el estudiante
        Vector3 direction = (desiredPosition - student.position).normalized;
        float distance = Vector3.Distance(desiredPosition, student.position);

        RaycastHit hit;
        if (Physics.Raycast(student.position, direction, out hit, distance))
        {
            // Si hay un obstáculo, retroceder la cámara hacia la dirección opuesta
            Vector3 newPosition = hit.point + (-direction * minDistance); // Retroceder de la posición de colisión

            // Mantener la altura de la cámara igual a su posición original en Y
            newPosition.y = transform.position.y;

            // Suavizar la transición hacia la nueva posición
            desiredPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }

        // Suavizar la transición entre la posición actual y la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualizar la posición de la cámara
        transform.position = smoothedPosition;

        // Hacer que la cámara mire siempre hacia el estudiante
        transform.LookAt(student);
    }
}
