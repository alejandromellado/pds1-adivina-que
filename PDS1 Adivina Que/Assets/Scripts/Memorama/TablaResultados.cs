using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaResultados : MonoBehaviour
{
    GameObject tablaActual;
    DatabaseConnection database;

    // Referencias a elementos en la jerarquia
    [SerializeField] GameObject scrollView;
    [SerializeField] GameObject viewPort;
    [SerializeField] GameObject templateTabla;
    [SerializeField] GameObject templateRenglon;
    
    // Start is called before the first frame update
    void Start()
    {
        //database = GetComponent<DatabaseConnection>();

        //var puntajes = database.ObtenerPuntajes(1);
        //CargarPuntajes(puntajes);

    }

    void CargarPuntajes(List<string[]> puntajes)
    {
        // Destruir tabla actual si existe
        if (tablaActual != null)
        {
            Destroy(tablaActual);
        }

        // Crear una nueva tabla del template y colocar en ScrollView
        tablaActual = Instantiate(templateTabla);
        tablaActual.transform.SetParent(viewPort.transform, false);
        scrollView.GetComponent<ScrollRect>().content = tablaActual.GetComponent<RectTransform>();
        tablaActual.SetActive(true);

        // Crear renglones y agregarlos a la tabla
        var lugar = 1;

        foreach (var partida in puntajes)
        {
            var nombre = partida[0];
            var puntos = partida[1];

            var nuevoRenglon = Instantiate(templateRenglon);
            nuevoRenglon.GetComponent<RenglonResultado>().EstablecerDatos(lugar, nombre, puntos);
            nuevoRenglon.transform.SetParent(tablaActual.transform, false);


            nuevoRenglon.GetComponent<Image>().color = new Color(228, 228, 228);


            nuevoRenglon.SetActive(true);

            lugar++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
