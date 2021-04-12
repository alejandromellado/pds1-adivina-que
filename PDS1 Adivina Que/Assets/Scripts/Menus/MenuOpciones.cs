using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpciones : MonoBehaviour
{
    public GameObject slider;
    public GameObject botonSeguir;
    public GameObject menuOpciones;
    public GameObject menuRegistro;
    public GameObject menuPrincipal;
    
    void Start()
    {
        slider.SetActive(true);
        botonSeguir.SetActive(true);
    }

    public void BotonRegresar()
    {
        menuOpciones.SetActive(false);
        
        if (DataMantainer.Nombre == "")
        {
            menuRegistro.SetActive(true);
        }
        else
        {
            menuPrincipal.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
