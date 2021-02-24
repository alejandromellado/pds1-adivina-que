using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class RenglonResultado : MonoBehaviour
{
    // Referencias a elementos en la jerarquia
    [SerializeField] private TextMeshProUGUI labelLugar;
    [SerializeField] private TextMeshProUGUI labelNombre;
    [SerializeField] private TextMeshProUGUI labelPuntos;

    public void EstablecerDatos(int lugar, string nombre, string puntos)
    {
        labelLugar.text = lugar.ToString();
        labelNombre.text = nombre;
        labelPuntos.text = puntos;
    }
}
