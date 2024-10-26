using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UnlockExtraDoubleJump : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerExtrasTracker playerExtrasTracker;
    public bool canDoubleJump;

    private void Start()
    {
        // Se obtiene la instancia del SingletonManager
        SingletonManager singletonManager = FindObjectOfType<SingletonManager>();

        if (singletonManager != null) 
        {
            // Registra este objeto con el SingletonManager
            singletonManager.unlockExtraDoubleJump = this;
        }
        
        player = GameObject.Find("Player");
        playerExtrasTracker = player.GetComponent<PlayerExtrasTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemsManager.instance.itemPH >= 5)
            {
                SetTracker();
            }

            Debug.Log("Ha colisionado con SetTracker DoubleJump");
        }
    }

    private void SetTracker()
    {
        if (canDoubleJump == false) playerExtrasTracker.CanDoubleJump = true;
    }
}
