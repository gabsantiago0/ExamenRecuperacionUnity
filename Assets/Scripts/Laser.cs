using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb;
    private GameManager gameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>(); // Busca la instancia de GameManager en la escena
        if (gameManager == null)
        {
            Debug.LogError("No se encontró un objeto GameManager en la escena.");
        }
    }

    public void Shoot(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyGA"))
        {
            
            if (gameManager != null)
            {
                //GameManager.instance.sumarPuntosVA(); //Sumar 200 puntos
               GameManager.instance.sumarPuntos(); //original
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("La referencia a GameManager es nula.");
            }
            
        }else if (other.gameObject.CompareTag("EnemyR"))
        {
            if (gameManager != null)
            {
                GameManager.instance.sumarPuntosR();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("La referencia a GameManager es nula.");
            }
        }
        if (other.gameObject.CompareTag("limit"))
        {
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            gameObject.SetActive(false);
        }

    }

}

