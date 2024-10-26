using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f; // Duración de la transición de desvanecimiento
    [SerializeField] private float yOffset = 2f; // Distancia en el eje Y que el ítem se desplazará
    [SerializeField] private AnimationCurve fadeCurve; // Curva de animación para el desvanecimiento

    private SpriteRenderer spriteRenderer;
    private bool isCollected = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            StartCoroutine(FadeAndMove());

            if (this.CompareTag("Item_1"))
            {
                isCollected = true;
                ItemsManager.instance.AddItemCoinSpin();
                StartCoroutine(FadeAndMove());
            }
            else if (this.CompareTag("Item_2"))
            {
                isCollected = true;
                ItemsManager.instance.AddItemCoinShine();
                StartCoroutine(FadeAndMove());
            }
            else if (this.CompareTag("Item_3"))
            {
                isCollected = true;
                ItemsManager.instance.AddItemPickHeart();
                StartCoroutine(FadeAndMove());
            }
        }
    }

    IEnumerator FadeAndMove()
    {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + Vector2.up * yOffset;
        
        // Desvanecimiento gradual
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        // Movimiento hacia arriba
        while (elapsedTime < fadeDuration)
        {
            transform.position = targetPosition;
            float alpha = fadeCurve.Evaluate(elapsedTime / fadeDuration); // Usa la curva de animación para el desvanecimiento
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        
        // Destruir el objeto
        Destroy(gameObject);
    }
}


