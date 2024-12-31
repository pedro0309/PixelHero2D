using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
       

        // Verifica si hay datos de posición guardados
        if (PlayerPrefs.HasKey("SpawnX") && PlayerPrefs.HasKey("SpawnY"))
        {
            float x = PlayerPrefs.GetFloat("SpawnX");
            float y = PlayerPrefs.GetFloat("SpawnY");

            Debug.Log($"Transportando al jugador a la posición: {x}, {y}");

            // Ajusta la posición del jugador
            transform.position = new Vector2(x, y);

            // Limpia los datos guardados para evitar conflictos futuros
            PlayerPrefs.DeleteKey("SpawnX");
            PlayerPrefs.DeleteKey("SpawnY");
        }
        else
        {
            Debug.LogWarning("No se encontró posición guardada para el jugador.");
        }
    }
}
