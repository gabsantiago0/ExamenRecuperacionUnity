using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI puntuacion;
    public int puntos = 0;
    public int vidas = 3;
    public int MaxVidas = 5;
    public GameObject[] corazones;
    public float invulnerabilityTime = 1f;
    private float lastHitTime;
    public GameObject UIRecord;
    public TextMeshProUGUI newRecord;
    private bool mensaje = true;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ActualizarPuntos();

        if (mensaje)
        {
            AvisarRecord();
        }
        
    }

    public void AvisarRecord()
    {
        if (puntos >PlayerPrefs.GetInt("Puntuacion" + 0, 0))
        {
            StartCoroutine(avisar());
            mensaje = false;
        }
            
    }

    public void ActualizarPuntos()
    {
        puntuacion.text = puntos.ToString();
    }

    IEnumerator avisar()
    {
        UIRecord.SetActive(true);
        newRecord.SetText("NUEVO RECORD");
        yield return new WaitForSeconds(5f);
        newRecord.SetText("");
        UIRecord.SetActive(false);
    }


    public void PowerUpPuntosExtra()
    {
        puntos += 100;
        puntuacion.text = puntos.ToString();
    }

    public void PowerUpPuntos()
    {
        puntos += 50;
        puntuacion.text = puntos.ToString();
    }


    public void sumarPuntos()
    {
        puntos += 10;
        puntuacion.text = puntos.ToString();
    }

    public void sumarPuntosVA()
    {
        puntos += 200;
        puntuacion.text = puntos.ToString();
    }
    public void sumarPuntosR()
    {
        puntos += 15;
        puntuacion.text = puntos.ToString();
    }

    // Método para perder una vida
    public void PerderVida()
    {
        // Solo pierde una vida si ha pasado suficiente tiempo desde el último golpe
        if (Time.time > lastHitTime)
        {
            vidas -= 1;
            if (vidas >= 0 && vidas < corazones.Length)
            {
                corazones[vidas].SetActive(false);
            }

            if (vidas <= 0)
            {
                Perder();
            }

            // Actualiza la última vez que el jugador fue golpeado
            lastHitTime = Time.time;
        }
    }

    public void GanarVida()
    {
        if (vidas < corazones.Length)
        {
            corazones[vidas].SetActive(true);
            vidas += 1;
        }

        if (vidas <= 0)
        {
            Perder();
        }
    }


    // Método para activar todas las vidas al inicio del juego o al reiniciar
    public void ReiniciarVidas()
    {
        vidas = corazones.Length;
        foreach (GameObject corazon in corazones)
        {
            corazon.SetActive(true);
        }
    }

    // Método para cargar la escena de pérdida
    public void Perder()
    {
        GuardarUltimaPuntuacion();
        GuardarPuntuacion();
        SceneManager.LoadScene("GameOver");
    }

    public void GuardarPuntuacion()
    {
        for (int i = 0; i < 1; i++)
        {
            // Comprueba si la puntuación actual es mayor que alguna de las guardadas
            if (puntos > PlayerPrefs.GetInt("Puntuacion" + i, 0))
            {
                // Si es mayor, desplaza las puntuaciones inferiores una posición hacia abajo
                for (int j = 4; j > i; j--)
                {
                    PlayerPrefs.SetInt("Puntuacion" + j, PlayerPrefs.GetInt("Puntuacion" + (j - 1)));
                }

                // Guarda la nueva puntuación en la posición que le corresponde
                PlayerPrefs.SetInt("Puntuacion" + i, puntos);
                break;
            }
        }
    }

    public void GuardarUltimaPuntuacion()
    {
        PlayerPrefs.SetInt("Ultima", puntos);
    }

    public string MostrarUltimaPuntuacion()
    {
        int puntosU = PlayerPrefs.GetInt("Ultima", 0);
        string puntuacion = "Ultima Puntuacion: "+ puntosU;
        return puntuacion;

    }

    public string MostrarPuntuaciones()
    {
        string puntuaciones = "";
        for (int i = 0; i < 1; i++)
        {
            int puntuacion = PlayerPrefs.GetInt("Puntuacion" + i, 0);
            puntuaciones += "Puntuación " + (i + 1) + ": " + puntuacion + "\n";
        }
        return puntuaciones;
    }




}
