using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public Material colorCarta;
    public int idCarta = 0;
    public Vector3 posicionOriginal;
    public Texture2D texturaAnverso;
    public Texture2D texturaReverso;
    public float tiempoDelay;
    public GameObject crearCartas;
    public bool Mostrando;

    void Awake()
    {
        crearCartas = GameObject.Find("scripts");
    }

    void Start()
    {
        EsconderCarta();
    }

    public void OnMouseDown()
    {
        print(idCarta.ToString());
        MostrarCarta();
    }

    // Update is called once per frame
    public void AsignarTextura(Texture2D _textura)
    {
       texturaAnverso= _textura;
    }

    public void MostrarCarta()
    {
        if (!Mostrando && crearCartas.GetComponent<CrearCartas>().sePuedeMostrar)
        {
            Mostrando = true;
            GetComponent<MeshRenderer>().material.mainTexture = texturaAnverso;
            //Invoke("EsconderCarta", tiempoDelay);
            crearCartas.GetComponent<CrearCartas>().HacerClick(this);
        }
        
    }

    public void EsconderCarta()
    {
        Invoke("Esconder", tiempoDelay);
        crearCartas.GetComponent<CrearCartas>().sePuedeMostrar = false;
    }

    void Esconder()
    {
        GetComponent<MeshRenderer>().material.mainTexture = texturaReverso;
        Mostrando = false;
        crearCartas.GetComponent<CrearCartas>().sePuedeMostrar = true;
    }

}
