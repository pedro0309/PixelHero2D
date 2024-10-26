using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public GameObject EnemyImpact; // Prefab de las part�culas de destrucci�n del enemigo

    public void DestroyEnemy()
    {
        // Instancia de las part�culas de destrucci�n en la posici�n del enemigo
        Instantiate(EnemyImpact, transform.position, Quaternion.identity);
        // Destruye al enemigo
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadScene("Prototype");
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); //Se cargar� la escena Prototype
    }

}

