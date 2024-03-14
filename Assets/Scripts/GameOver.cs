using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI ultimaPuntuacion;
    // Start is called before the first frame update
    void Start()
    {
        ultimaPuntuacion.text = GameManager.instance.MostrarUltimaPuntuacion();
    }

   
  
}
