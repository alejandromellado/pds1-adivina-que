using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazUsuario : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuGanador;
    public bool menuMostrado;
    public bool menuMostradoGanador;
    

    public void MostrarMenu()
    {
        menu.SetActive(true);
        menuMostrado = true;
    }

    public void EsconderMenu()
    {
        menu.SetActive(false);
        menuMostrado = false;
    }

    public void MostrarMenuGanador()
    {
        menuGanador.SetActive(true);
        menuMostradoGanador = true;
    }

    public void EsconderMenuGanador()
    {
        menuGanador.SetActive(false);
        menuMostradoGanador = false;
    }

    public void SaliraMenu()
    {

    }
}
