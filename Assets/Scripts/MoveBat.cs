using UnityEngine;

public class MoveBat : MonoBehaviour
{
    private Vector2 startPosition; // Posición inicial del enemigo
    private Vector2 endPosition;   // Posición final hacia la derecha
    private float duration = 2f;   // Duración del movimiento en un sentido

    private void Start()
    {
        // Configurar la posición inicial del enemigo al comenzar el juego
        startPosition = transform.position;
        endPosition = startPosition + Vector2.up * 2f; // Desplazar 2 unidades hacia la derecha

        if (gameObject.name == "Enemy_6" || gameObject.name == "Enemy_7")
        {
            duration = .75f; // Ajustar duración si es Skeleton_5
        }

        if (gameObject.name == "Enemy_8" || gameObject.name == "Enemy_9" || gameObject.name == "Enemy_10")
        {
            duration = .75f; // Ajustar duración si es Skeleton_5
            endPosition = startPosition + Vector2.right * 2f; // Desplazar 2 unidades hacia la derecha
        }
    }

    private void Update()
    {
        // Calcular el tiempo interpolado usando PingPong
        float time = Mathf.PingPong(Time.time / duration, 1f);

        // Interpolar entre la posición inicial y final
        transform.position = Vector2.Lerp(startPosition, endPosition, time);
    }
}

