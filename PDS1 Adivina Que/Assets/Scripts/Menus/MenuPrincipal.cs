using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;


public class MenuPrincipal : MonoBehaviour
{

    void Start()
    {
         
    }

    public void CargarJuego()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void CargarMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego.");
        Application.Quit();
    }
}
