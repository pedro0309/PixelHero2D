using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockExtraDash : MonoBehaviour
{
    // Variables de instancia
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerExtrasTracker playerExtrasTracker;
    public bool canDash;

    // Lista est�tica para mantener todas las instancias
    private static List<UnlockExtraDash> instances = new List<UnlockExtraDash>();

    // M�todo est�tico para acceder a la instancia �nica
    public static List<UnlockExtraDash> Instances
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
            if (ItemsManager.instance.itemCSp >= 6)
            {
                SetTracker();
            }
            print("Ha colisionado con SetTracker Dash");
        }
    }

    private void SetTracker() //Aqui se verificar�n las Condiciones de los Extras, estar�n en Privadas y se conectar� con el PlayerExtrasTracker (para switchear valores)
    {
        if (canDash == false)
        {
            playerExtrasTracker.CanDash = true;
        }
    }
}
