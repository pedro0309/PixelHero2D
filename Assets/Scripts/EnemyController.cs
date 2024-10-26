using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public GameObject EnemyImpact; // Prefab de las partículas de destrucción del enemigo

    public void DestroyEnemy()
    {
        // Instancia de las partículas de destrucción en la posición del enemigo
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
        SceneManager.LoadScene(sceneName); //Se cargará la escena Prototype
    }

}

