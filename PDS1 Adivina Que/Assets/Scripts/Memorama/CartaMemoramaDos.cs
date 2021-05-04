using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartaMemoramaDos : MonoBehaviour
{
    // Referencias
    [SerializeField] private ControladorDosJugadores controlador;
    [SerializeField] public GameObject reversoDeCarta;         // Permite llamar desde codigo la parte de atras de la carta para activarla/desactivarla
    SpriteRenderer carta;
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

    public void Voltear()
    {
        reversoDeCarta.SetActive(true);
    }
    IEnumerator MostrarInicio()
    {
        reversoDeCarta.SetActive(false);
        yield return new WaitForSeconds(4);
        reversoDeCarta.SetActive(true);
    }
    public void Visible(bool esta)
    {
        carta.enabled=esta;
    }
    // Start is called before the first frame update
    void Start()
    {
        carta = GetComponent<SpriteRenderer>();
        StartCoroutine(MostrarInicio());
    }
}

