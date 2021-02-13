using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    public void Salir()
    {
        Debug.Log("Saliendo del juego.");
        Application.Quit();
    }
}
