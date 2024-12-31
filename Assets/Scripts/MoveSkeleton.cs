using UnityEngine;

public class MoveSkeleton : MonoBehaviour
{
    private Vector2 startPosition; // Posición inicial del enemigo
    private Vector2 endPosition;   // Posición final hacia la derecha
    private float duration = 2f;   // Duración del movimiento en un sentido
    private bool facingRight = true; // Indica si el enemigo está mirando a la derecha

    private void Start()
    {
        // Configurar la posición inicial del enemigo al comenzar el juego
        startPosition = transform.position;
        endPosition = startPosition + Vector2.right * 2f; // Desplazar 2 unidades hacia la derecha

        // Verificar el nombre del GameObject
        if (gameObject.name == "Skeleton_5" || gameObject.name == "Skeleton_6")
        {
            duration = 1f; // Ajustar duración si es Skeleton_5
            Debug.Log("El GameObject es Skeleton_5, duration ajustado a 3");
        }
    }

    private void Update()
    {
        // Calcular el tiempo interpolado usando PingPong
        float time = Mathf.PingPong(Time.time / duration, 1f);

        // Interpolar entre la posición inicial y final
        Vector2 newPosition = Vector2.Lerp(startPosition, endPosition, time);

        // Verificar la dirección del movimiento
        if (newPosition.x < transform.position.x && !facingRight)
        {
            Flip(); // Está yendo a la derecha y no está mirando a la derecha
        }
        else if (newPosition.x > transform.position.x && facingRight)
        {
            Flip(); // Está yendo a la izquierda y no está mirando a la izquierda
        }

        // Actualizar la posición del enemigo
        transform.position = newPosition;
    }

    private void Flip()
    {
        // Cambiar la dirección (invertir la escala en X)
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Invertir la escala en el eje X
        transform.localScale = localScale;
    }
}
