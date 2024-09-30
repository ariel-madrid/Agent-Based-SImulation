using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacker : MonoBehaviour
{
    public List<Transform> targets; // Lista de objetivos a seguir (estudiantes)
    public float chaseRange = 15f; // Rango dentro del cual comienza la persecución
    public float killDistance = 1.5f; // Distancia para "eliminar" al objetivo
    private NavMeshAgent agent;
    private Transform currentTarget; // El objetivo actual a perseguir
    public Color chaserColor = Color.red; // Color que cambia cuando el cazador está en alerta
    private bool isChasing = false; // Estado del cazador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Verificar si hay objetivos en la lista
        if (targets.Count == 0) return;

        // Inicializar la distancia mínima y el objetivo actual
        float closestDistance = Mathf.Infinity;
        currentTarget = null;

        // Buscar el objetivo más cercano
        foreach (Transform target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Comprobar si el objetivo está dentro del rango de persecución
            if (distanceToTarget < chaseRange && distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                currentTarget = target; // Actualizar el objetivo actual
            }
        }

        // Si hay un objetivo actual, moverse hacia él
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position); // Mover hacia el objetivo

            // Verificar si el cazador está lo suficientemente cerca del objetivo
            if (closestDistance < killDistance)
            {
                var studentAgent = currentTarget.GetComponent<StudentAgent>();
                if (studentAgent != null)
                {
                    studentAgent.Eliminate(); // Cambiar color del estudiante
                }
                targets.Remove(currentTarget); // Remover el objetivo de la lista
            }
        }
        else
        {
            // Detener el agente si no hay objetivos
            agent.SetDestination(transform.position); // Detener el agente si no hay objetivos
        }

        // Cambiar el color del cazador si está persiguiendo
        if (isChasing && GetComponent<Renderer>().material.color != chaserColor)
        {
            ChangeColor(chaserColor); // Cambiar color
            NotifyOthers(); // Notificar a los otros agentes
        }
    }

    // Método para cambiar el estado del cazador
    public void StartChasing()
    {
        isChasing = true;
    }

    // Método para notificar a otros agentes
    private void NotifyOthers()
    {
        Debug.Log("El cazador ha cambiado de color, notificando a los estudiantes.");
        // Aquí puedes añadir cualquier lógica adicional si lo deseas
        // No se requiere notificar a los estudiantes ya que no hay una zona segura
    }

    // Método para cambiar el color del cazador
    private void ChangeColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }
}
