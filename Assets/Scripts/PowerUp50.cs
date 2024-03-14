using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp50 : MonoBehaviour
{
    public float velocidadDeCaida = 1f;


    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(0, -velocidadDeCaida);
        Destroy(gameObject, 10f);
    }


    public void RecogerPowerUp()
    {
        GameManager.instance.PowerUpPuntos();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el objeto con el que hemos colisionado es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            RecogerPowerUp();
        }
        else
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("limit"))
        {
            Destroy(gameObject);
        }
    }
}
