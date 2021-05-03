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
        if (DataMantainer.Dosjugadores)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(2);
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        
    }

    public void CargarMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego.");
        Application.Quit();
    }
}
