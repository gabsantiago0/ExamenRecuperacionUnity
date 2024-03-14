using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject UI;
    public GameObject UI2;
    public TextMeshProUGUI records;


    public void iniciarPartida()
    {
        SceneManager.LoadScene("Game");
    }

    public void cargarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }

    public void ActivarUI()
    {
        UI.SetActive(true);
    }

    public void ActivarUI2()
    {
        UI2.SetActive(true);
    }

    public void ApagarUI()
    {
        UI.SetActive(false);
    }

    public void ApagarUI2()
    {
        UI2.SetActive(false);
    }


    public void ShowRecords()
    {
        ApagarUI();
        ActivarUI2();
        string puntuaciones = GameManager.instance.MostrarPuntuaciones();
        records.text = puntuaciones.ToString();
    }

    public void volverMenu()
    {
        ApagarUI2();
        ActivarUI();
    }
}
