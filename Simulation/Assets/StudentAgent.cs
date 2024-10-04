using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Importar la librería para NavMesh

public class StudentAgent : MonoBehaviour
{
    public float moveSpeed = 10.0f;         // Velocidad de movimiento del estudiante (se puede usar para ajustar la velocidad del NavMeshAgent)
    public Transform targetZone;            // Zona objetivo hacia la cual el estudiante se moverá
    public float targetReachedThreshold = 1.0f; // Distancia mínima para considerar que el objetivo ha sido alcanzado
    public float movementRadius = 50f;      // Radio de movimiento máximo desde la posición inicial (opcional)

    private NavMeshAgent navMeshAgent;      // Referencia al componente NavMeshAgent
    private Vector3 initialPosition;        // Posición inicial del estudiante

    void Start()
    {
        // Guardar la posición inicial del estudiante
        initialPosition = transform.position;

        // Obtener el componente NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Asegurarse de que haya un objetivo asignado
        if (targetZone != null)
        {
            // Establecer la velocidad del NavMeshAgent
            navMeshAgent.speed = moveSpeed;

            // Establecer la zona objetivo como el destino del NavMeshAgent
            SetTargetDestination();
        }
        else
        {
            Debug.LogWarning("No se ha asignado ninguna zona objetivo.");
        }
    }

    void Update()
    {
        if (targetZone != null)
        {
            // Mover al estudiante hacia la zona objetivo
            MoveTowardsTarget();
        }

        // Limitar el movimiento del estudiante dentro de su radio (si es necesario)
        StayWithinRadius();
    }

    // Establecer la zona objetivo como el destino del NavMeshAgent
    void SetTargetDestination()
    {
        navMeshAgent.SetDestination(targetZone.position);
    }

    // Mover al estudiante hacia la zona objetivo
    void MoveTowardsTarget()
    {
        // Comprobar la distancia al objetivo
        float distanceToTarget = Vector3.Distance(transform.position, targetZone.position);

        // Si el estudiante ha llegado al objetivo
        if (distanceToTarget <= targetReachedThreshold)
        {
            // Detener el movimiento del agente
            navMeshAgent.isStopped = true;
            Debug.Log("El estudiante ha llegado a la zona objetivo.");
        }
    }

    // Mantener al estudiante dentro de su radio de movimiento (si es necesario)
    void StayWithinRadius()
    {
        Vector3 offset = transform.position - initialPosition;

        // Si el estudiante sale del radio, lo redirigimos de vuelta
        if (offset.magnitude > movementRadius)
        {
            // Direccionar el movimiento de vuelta hacia la posición inicial
            navMeshAgent.SetDestination(initialPosition);
        }
    }
}
