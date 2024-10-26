using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Sprites
    private GameObject standingPlayer;
    private GameObject ballPlayer;

    [Header("Player Movement")]
    [SerializeField] private float movesSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask selectedLayerMask;
    [SerializeField] private float waitForBallMode;
    private Rigidbody2D playerRB; //Instancia del Componente de Rigidbody
    private Animator animatorStandingPlayer;
    private Animator animatorBallPlayer;
    private bool isGrounded, isFlippedInX;
    private Transform transformPlayerController;
    //Creamos las var que almaceran los Id's *Paso 1 para Optimizar las llamadas de Animation mediante Id's
    //Player Animator
    private int IdSpeed, IdIsGrounded, IdShootArrow, IdCanDoubleJump;
    private float afterImageCounter;
    private float ballModeCounter;
    [SerializeField] private float isGroundedRange;

    [Header("Player Shoot")]
    [SerializeField] private ArrowController arrowController;
    private Transform transformArrowPoint;
    [SerializeField] private Transform checkGroundPoint;
    private Transform transformBombPoint;
    [SerializeField] private GameObject prefabBomb;

    
    [Header("Player Dust")]
    [SerializeField]
    private GameObject dustJump;
    private Transform transformDustPoint;
    private bool isIdle, canDoubleJump;

    [Header("Player Dash")]
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;
    private float dashCounter;
    [SerializeField] private float waitForDash;
    private float afterDashCounter;

    [Header("Player Dash After Image")]
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private SpriteRenderer afterImageSR;
    [SerializeField] private float afterImageLifetime;
    [SerializeField] private Color afterImageColor;
    [SerializeField] private float afterImageTimeBetween;

    //Player Extras
    private PlayerExtrasTracker playerExtrasTracker;
    
    private void Awake()
    {
        
        playerRB = GetComponent<Rigidbody2D>(); //Obtiene la instancia del componente Rigidbody
        transformPlayerController = GetComponent<Transform>();
        playerExtrasTracker = GetComponent<PlayerExtrasTracker>(); //Busca Instancia de PlayerExtrasTracker
    }

    private void Start()
    {
        standingPlayer = GameObject.Find("Standing Player");
        ballPlayer = GameObject.Find("BallPlayer");
        ballPlayer.SetActive(false);
        transformDustPoint = GameObject.Find("DustPoint").GetComponent<Transform>();
        transformArrowPoint = GameObject.Find("ArrowPoint").GetComponent<Transform>();
        //checkGroundPoint = GameObject.Find("CheckGroundPoint").GetComponent<Transform>();
        transformBombPoint = GameObject.Find("BombPoint").GetComponent<Transform>();
        animatorStandingPlayer = standingPlayer.GetComponent<Animator>();//Se guarda en animator, lo encontrado por el GO.find de StandingPlayer, que obtuvo el Componente de Animator *** PASO 2 ***
        animatorBallPlayer = ballPlayer.GetComponent<Animator>();

        //Paso 2 para la Optimizacion de las llamadas de Animation mediante Id's - Asignación de Id's
        IdSpeed = Animator.StringToHash("speed");
        IdIsGrounded = Animator.StringToHash("isGrounded");
        IdShootArrow = Animator.StringToHash("shootArrow"); //
        IdCanDoubleJump = Animator.StringToHash("canDoubleJump");
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Jump();
        CheckAndSetDirection();
        Shoot();
        PlayDust();
        BallMode();
    }

    private void Dash()
    {
        if (afterDashCounter > 0)
        
            afterDashCounter -= Time.deltaTime;
        else
            {
                if ((Input.GetButtonDown("Fire2") && standingPlayer.activeSelf) && playerExtrasTracker.CanDash)
                {
                    dashCounter = dashTime;
                    ShowAfterImage();
                }
            }

        if(dashCounter > 0 ) 
        {
            dashCounter -= Time.deltaTime;
            playerRB.velocity = new Vector2(dashSpeed * transform.localScale.x, playerRB.velocity.y);
            afterImageCounter -= Time.deltaTime;
            if( afterImageCounter <= 0 )
            {
                ShowAfterImage();
            }
            afterDashCounter = waitForDash;
        }
        else 
        { 
        Move();
        }
    }

    private void Shoot() //Si el Player está de Pie, lanza flechas. Si está hecho bola, lanza bombas
    {
        if (Input.GetButtonDown("Fire1") && standingPlayer.activeSelf)
        {
            ArrowController tempArrowController = Instantiate(arrowController, transformArrowPoint.position, transformArrowPoint.rotation);
            if (isFlippedInX)
            {
                tempArrowController.ArrowDirection = new Vector2(-1, 0f);
                tempArrowController.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                tempArrowController.ArrowDirection = new Vector2(1, 0f);
            }
            tempArrowController.ArrowDirection = new Vector2(transformPlayerController.localScale.x, 0f);//Depende donde este aputando el player, la flecha saldra en una direccion, o en otra

            animatorStandingPlayer.SetTrigger(IdShootArrow);
        }
        if ((Input.GetButtonDown("Fire1") && ballPlayer.activeSelf) && playerExtrasTracker.CanDropBombs)
            Instantiate(prefabBomb, transformBombPoint.position, Quaternion.identity);
    }

    private void Move() //No importa si se aplica el Move() en el Update, debido que utiliza Parametros fijos como el Rigidbody que establece los limites de la Camara. No importa si el juego va a 25fps o 150fps, como no requiere Párametros que se calculan mediante los fps no existirá variación
    {
        float inputX = Input.GetAxisRaw("Horizontal") * movesSpeed; //No hace falta multiplicarlo por Time.deltaTime, con Rigidbody.Velocity no es necesario
        playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
        if (standingPlayer.activeSelf) 
        { 
            animatorStandingPlayer.SetFloat(IdSpeed, Math.Abs(playerRB.velocity.x));
        }
        // ***PASO 3*** //Llamadas a Update. No es recomendable utilizar en llamadas de Animaciones en string, porque en cada Update descompone el string
        if (ballPlayer.activeSelf)
        {
            animatorBallPlayer.SetFloat(IdSpeed, Mathf.Abs(playerRB.velocity.x));
        }
    }

    private void Jump()
    {
        //isGrounded = Physics2D.OverlapCircle(checkGroundPoint.position, isGroundRange, selectedLayerMask);//Si la posicion del checkGroundPoint, y el radio esté tocando este LayerMask, dará true en isGrounded
        isGrounded= Physics2D.Raycast(checkGroundPoint.position, Vector2.down, isGroundedRange, selectedLayerMask);
        if (Input.GetButtonDown("Jump") && (isGrounded || (canDoubleJump && playerExtrasTracker.CanDoubleJump))) //Si saltamos y isGrounded es falso, no dará un segundo salto (salto en el aire)
        {
            if(isGrounded) //Si es true isGrounded (canDoubleJump falso)
            { 
                canDoubleJump = true;
                Instantiate(dustJump, transformDustPoint.position, Quaternion.identity);
            }
            else
            {
                canDoubleJump = false;
                animatorStandingPlayer.SetTrigger(IdCanDoubleJump);
            }
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
        animatorStandingPlayer.SetBool(IdIsGrounded, isGrounded);
    }
    private void CheckAndSetDirection()
    {
        if (playerRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFlippedInX = true;
        }

        else if (playerRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFlippedInX = false;
        }
    }
    private void PlayDust() //Crea humito  cuando se mueve el player o cambia de direccion
    {
        if (playerRB.velocity.x != 0 && isIdle) 
        {
            isIdle = false;
            if(isGrounded)
            
            Instantiate(dustJump, transformDustPoint.position, Quaternion.identity);
        }
        if(playerRB.velocity.x == 0)
        {
            isIdle = true;
        }
    }

    private void ShowAfterImage()
    {
        SpriteRenderer afterImage = Instantiate(afterImageSR, transformPlayerController.position, transformPlayerController.rotation);
        afterImage.sprite = playerSR.sprite;
        afterImage.transform.localScale = transformPlayerController.localScale;
        afterImage.color = afterImageColor;
        Destroy(afterImage.gameObject, afterImageLifetime);
        afterImageCounter = afterImageTimeBetween;
    }

    private void BallMode()
    {
        float inputVertical = Input.GetAxisRaw("Vertical");
        if ((inputVertical <= -.9f && !ballPlayer.activeSelf || inputVertical >= .9 && ballPlayer.activeSelf) && playerExtrasTracker.CanEnterBallMode)
        {
            ballModeCounter -= Time.deltaTime;
            if (ballModeCounter < 0)
            {
                ballPlayer.SetActive(!ballPlayer.activeSelf);
                standingPlayer.SetActive(!standingPlayer.activeSelf);
            }
        }
        else ballModeCounter = waitForBallMode;
    }
    private void OnDrawGizmos() //Sirve para visualizar el rango de la Explosion de la Bomba
    {
        Gizmos.DrawWireSphere(checkGroundPoint.position, isGroundedRange);
    }
}
