using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class LabelNombre : MonoBehaviour
{
    TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();

        if (DataMantainer.Nombre != "")
        {
            texto.text = "Bienvenid@\n" + DataMantainer.Nombre;
        }
        else
        {
            texto.text = "Bienvenid@\n$NOMBRE";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
