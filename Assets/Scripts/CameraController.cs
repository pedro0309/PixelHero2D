using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController; //Tenemos referencia al Script
    private Transform camTransform;
    private Transform playerTransform;
    private BoxCollider2D levelLimit;
    private float cameraSizeHorizontal;
    private float cameraSizeVertical;

    // Start is called before the first frame update
    void Start()
    {
        levelLimit = GameObject.Find("LevelLimit").GetComponent<BoxCollider2D>();

        //Almacenamos todo lo relacionado a camara en el Start para no ejercer tantas llamadas en el Update
        camTransform = GetComponent<Transform>(); //Se almacena en camTransform el valor de transform (position, rotation, scale)
        playerController = FindObjectOfType<PlayerController>();//Es mucho mas optimo buscar así, que por string
        playerTransform = playerController.GetComponent<Transform>(); //Ya obtenemos las 3 referencias que necesitamos para controlar la camara
        cameraSizeVertical = Camera.main.orthographicSize; //Obtenemos margen superior
        cameraSizeHorizontal = Camera.main.orthographicSize * Camera.main.aspect;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null) //Si playerController no es nulo, por ej si se destruye o algo, ejecutara el trackeo de camara del player
        {
            camTransform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, 
                                    levelLimit.bounds.min.x + cameraSizeHorizontal, 
                                    levelLimit.bounds.max.x - cameraSizeHorizontal), //Creamos los limites del Eje X
                Mathf.Clamp(playerTransform.position.y, 
                                    levelLimit.bounds.min.y + cameraSizeVertical, 
                                    levelLimit.bounds.max.y - cameraSizeVertical),//Limites Eje Y
                camTransform.position.z); //No hay limites en Eje Z, queda igual
        }
    }
}
