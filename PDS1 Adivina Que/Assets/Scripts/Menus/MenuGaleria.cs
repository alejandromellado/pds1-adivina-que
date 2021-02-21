using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UIElements;

public class MenuGaleria : MonoBehaviour
{
    DatabaseConnection database;

    // Referencias a elementos en la jerarquia
    public TMP_Dropdown dropdownMaterias;
    public TMP_Dropdown dropdownTemas;
    public GameObject scrollView;
    

    // Start is called before the first frame update
    void Start()
    {
        database = GetComponent<DatabaseConnection>();

        // Establecer un Listener para ejecutar codigo cuando se seleccione una materia
        dropdownMaterias.onValueChanged.AddListener(delegate { 
            MateriaFueSeleccionada(dropdownMaterias); 
        });

        // Obtener la lista de materias de la base de datos y cargarla en el dropdown correspondiente
        List<string> materias = database.ObtenerMaterias();
        CargarDropdown(dropdownMaterias, materias);

        // Llamar el metodo correspondiente para cargar la lista de temas por primera vez
        MateriaFueSeleccionada(dropdownMaterias);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Obtiene el nombre de la materia seleccionada y carga su lista de temas en el dropdown correspondiente.
     * Es llamada cada que el valor seleccionado en el dropdown de materias cambia. */
    void MateriaFueSeleccionada(TMP_Dropdown materias)
    {
        string materiaSeleccionada = materias.options[materias.value].text;

        List<string> temas = database.ObtenerTemas(materiaSeleccionada);
        
        CargarDropdown(dropdownTemas, temas);
    }

    /* Carga una lista de strings a un elemento de tipo Dropdown.
     * Recibe el elemnto Dropdown al que se introducen las opciones y la una lista de strings. */
    void CargarDropdown(TMP_Dropdown dropdown, List<string> opciones)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(opciones);
        dropdown.RefreshShownValue();
    }

    void CargarScrollView(List<string> opciones)
    {
        foreach (string opcion in opciones)
        {
            Toggle toggle = new Toggle(opcion);
            
        }
    }

}
