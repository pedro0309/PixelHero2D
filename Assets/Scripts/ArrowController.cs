using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D arrowRB; //Referencia al Rigidbody del arrow
    [SerializeField]
    private float arrowSpeed;
    private Vector2 _arrowDirection;
    [SerializeField] private GameObject arrowImpact; // Prefab de la explosión de la flecha
    [SerializeField] private GameObject enemyImpact; // Prefab de la explosión del enemigo
    private Transform transformArrow;
    public Vector2 ArrowDirection { get => _arrowDirection; set => _arrowDirection = value; }

    private void Awake()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        transformArrow = GetComponent<Transform>();
    }

    void Update()
    {
        arrowRB.velocity = ArrowDirection * arrowSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Colisión con un enemigo
            Destroy(gameObject);
            Instantiate(enemyImpact, collision.transform.position, Quaternion.identity);
            DestroyEnemy(collision.gameObject);
        }
        else
        {
            // Colisión con otro objeto (no enemigo)
            Instantiate(arrowImpact, transformArrow.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DestroyEnemy(GameObject enemy)
    {
        // Lógica para destruir al enemigo
        // Puedes agregar aquí cualquier efecto adicional o acciones que desees realizar al destruir al enemigo
        Destroy(enemy);
    }

    private void OnBecameInvisible() //Si sale del rango del mundo (osea si se convierte en invisible), se destruirá
    {
        Destroy(gameObject);
    }
}
