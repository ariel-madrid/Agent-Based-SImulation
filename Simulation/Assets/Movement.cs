using UnityEngine;
using UnityEngine.AI;

public class StudentAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Renderer rend;
    private Color originalColor; // Color original del estudiante
    public Color eliminatedColor = Color.red; // Color al ser "eliminado"
    public float moveSpeed = 3.5f; // Velocidad de movimiento del estudiante
    private Vector3 targetPosition; // Nueva posición objetivo

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();

        // Verificar si el componente Renderer está presente
        if (rend == null)
        {
            Debug.LogError("No hay componente Renderer adjunto a " + gameObject.name);
            return; // Salir si no hay Renderer
        }

        originalColor = rend.material.color; // Guardar el color original
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Si el estudiante ha sido "eliminado", detener su movimiento
        if (rend.material.color == eliminatedColor) return;

        // Verifica si el agente ha llegado al destino
        if (agent.remainingDistance < 0.5f)
        {
            SetRandomTargetPosition(); // Establecer una nueva posición aleatoria
        }

        // Mover el estudiante
        agent.SetDestination(targetPosition);
    }

    // Método para establecer una nueva posición aleatoria
    void SetRandomTargetPosition()
    {
        float x = Random.Range(-5f, 5f); // Ajusta el rango según tu superficie
        float z = Random.Range(-5f, 5f); // Ajusta el rango según tu superficie
        targetPosition = new Vector3(x, transform.position.y, z); // Mantener la altura original
    }

    // Método que simula la eliminación del estudiante (cambio de color)
    public void Eliminate()
    {
        if (rend != null) // Verificar que el Renderer existe
        {
            rend.material.color = eliminatedColor; // Cambiar color
            agent.isStopped = true; // Detener el movimiento del agente
            Debug.Log($"{gameObject.name} ha sido alcanzado por el asesino y ha cambiado de color.");
        }
    }
}
