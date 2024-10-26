using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockExtras : MonoBehaviour
{
    [SerializeField] private GameObject player; //Obtenemos el GO de player
    [SerializeField] private PlayerExtrasTracker playerExtrasTracker;
    public bool canDoubleJump, canDash, canEnterBallMode, canDropBombs;
    public static UnlockExtras instance;

    private void Start()
    {
        instance = this;
        player = GameObject.Find("Player");
        playerExtrasTracker = player.GetComponent<PlayerExtrasTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //Unity recomienda mas el CompareTag que comparar con String
        {
            SetTracker();
            print("Ha colisionado con SetTracker");
        }
        
    }

    private void SetTracker() //Aqui se verificarán las Condiciones de los Extras, estarán en Privadas y se conectará con el PlayerExtrasTracker (para switchear valores)
    {
        if(canDoubleJump) playerExtrasTracker.CanDoubleJump = true;
        if (canDash) playerExtrasTracker.CanDash = true;
        if (canEnterBallMode) playerExtrasTracker.CanEnterBallMode = true;
        if (canDropBombs) playerExtrasTracker.CanDropBombs = true;
    }
}