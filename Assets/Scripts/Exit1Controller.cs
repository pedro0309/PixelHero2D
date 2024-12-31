using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit1Controller : MonoBehaviour
{
    public Vector2 playerSpawnPosition; //Definir la posición del Player en la nueva escena
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("SpawnX", playerSpawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", playerSpawnPosition.y);

            LoadScene("Level1");
        }
    }
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); //Se cargará la escena Prototype
    }
}
