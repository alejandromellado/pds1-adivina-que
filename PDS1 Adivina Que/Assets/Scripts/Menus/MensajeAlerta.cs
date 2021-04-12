using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeAlerta : MonoBehaviour
{
    public GameObject botonSeguir;
    public GameObject mensaje;
    public GameObject menuRegistro;

    void Start()
    {
        botonSeguir.SetActive(true);
    }

    public void BotonRegresar()
    {
        mensaje.SetActive(false);
        menuRegistro.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
