using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;


public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI labelNombre;

    void Start()
    {
        if (DataMantainer.Nombre != null)
        {
            labelNombre.text = "Bienvenid@\n" + DataMantainer.Nombre;
        }
        else
        {
            labelNombre.text = "Bienvenid@\n$NOMBRE";
        }
            
    }

    public void CargarJuego()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego.");
        Application.Quit();
    }
}
