using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaMemoramaDos : MonoBehaviour
{
    // Referencias
    [SerializeField] private ControladorDosJugadores controlador;
    [SerializeField] public GameObject reversoDeCarta;         // Permite llamar desde codigo la parte de atras de la carta para activarla/desactivarla

    // Variables
    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void EstablecerCarta(int id, Sprite imagen)
    {
        _id = id;

        // Asignar la imagen al renderizador cuando se crea la carta
        GetComponent<SpriteRenderer>().sprite = imagen;
    }

    /* Revela la carta cuando se registra un click. */
    void OnMouseDown()
    {
        if (reversoDeCarta.activeSelf && controlador.puedeEscoger)
        {
            reversoDeCarta.SetActive(false);
            controlador.CartaSeleccionada(this);
        }
    }

    public void Esconder()
    {
        reversoDeCarta.SetActive(true);
    }
    IEnumerator MostrarInicio()
    {
        reversoDeCarta.SetActive(false);
        yield return new WaitForSeconds(4);
        reversoDeCarta.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MostrarInicio());
    }
}

