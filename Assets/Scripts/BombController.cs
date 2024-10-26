using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]private float waitForExplode;
    [SerializeField]private float waitForDestroy;
    private Animator animator;
    private bool isActive;
    private int IDIsActive;
    [SerializeField]private Transform transformBomb;
    [SerializeField] private float expansiveWaveRange; //Rango de Onda Expansiva de Bomba
    [SerializeField] private LayerMask isDestroyable; //Layer de Destruible

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        IDIsActive = Animator.StringToHash("isActive");
        transformBomb = GetComponent<Transform>();
    }

    private void Update()
    {
        waitForExplode -= Time.deltaTime;
        waitForDestroy -= Time.deltaTime;
        if(waitForExplode <= 0  && !isActive) 
        {
            ActivatedBomb();
        }
        if(waitForDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ActivatedBomb()
    {
        isActive = true;
        animator.SetBool(IDIsActive, isActive);
        Collider2D[] destroyedObjects = Physics2D.OverlapCircleAll(transformBomb.position, expansiveWaveRange, isDestroyable);
        if(destroyedObjects.Length > 0 ) //Si encuentra algun objecto en su radio, que los destruya
        {
            foreach (var col in destroyedObjects) //Verificara coliders del array destroyedObjects
            {
                Destroy(col.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transformBomb.position, expansiveWaveRange);
    }
}
