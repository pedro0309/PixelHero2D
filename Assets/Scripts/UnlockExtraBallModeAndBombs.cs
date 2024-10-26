using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockExtraBallModeAndBombs : MonoBehaviour
{
    // Variables de instancia
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerExtrasTracker playerExtrasTracker;
    public bool canEnterBallMode, canDropBombs;

    // Lista est�tica para mantener todas las instancias
    private static List<UnlockExtraBallModeAndBombs> instances = new List<UnlockExtraBallModeAndBombs>();

    // M�todo est�tico para acceder a la instancia �nica
    public static List<UnlockExtraBallModeAndBombs> Instances
    {
        get { return instances; }
    }

    private void Awake()
    {
        // Agregar esta instancia a la lista de instancias
        instances.Add(this);

        // Inicializar los componentes
        player = GameObject.Find("Player");
        playerExtrasTracker = player.GetComponent<PlayerExtrasTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //Unity recomienda mas el CompareTag que comparar con String
        {
            if (ItemsManager.instance.itemCSh >= 10)
            {
                SetTracker();
            }
            print("Ha colisionado con SetTracker Ball Mode & Bombs");
        }
    }

    private void SetTracker() //Aqui se verificar�n las Condiciones de los Extras, estar�n en Privadas y se conectar� con el PlayerExtrasTracker (para switchear valores)
    {
        if (canEnterBallMode == false)
        {
            playerExtrasTracker.CanEnterBallMode = true;
        }

        if (canDropBombs == false)
        {
            playerExtrasTracker.CanDropBombs = true;
        }
    }
}
