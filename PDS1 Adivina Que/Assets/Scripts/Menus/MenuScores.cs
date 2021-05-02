using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScores : MonoBehaviour
{
    // Referencias a elementos en la jerarquia
    public TMP_Dropdown dropdownMaterias;
    public TMP_Dropdown dropdownTemas;
    public TablaResultados tablaResultados;

    DatabaseConnection database;

    // Start is called before the first frame update
    void Start()
    {
        database = GetComponent<DatabaseConnection>();

        // Establecer un Listener para ejecutar codigo cuando se seleccione una materia
        dropdownMaterias.onValueChanged.AddListener(delegate {
            MateriaFueSeleccionada(dropdownMaterias);
        });

        // Establecer un Listener para ejecutar codigo cuando se seleccione un tema
        dropdownTemas.onValueChanged.AddListener(delegate
        {
            TemaSeleccionado(dropdownTemas);
        });

        // Obtener la lista de materias de la base de datos y cargarla en el dropdown correspondiente
        List<string> materias = database.ObtenerMaterias();
        CargarDropdown(dropdownMaterias, materias);

        // Llamar el metodo correspondiente para cargar la lista de temas por primera vez
        MateriaFueSeleccionada(dropdownMaterias);
        TemaSeleccionado(dropdownTemas);
    }

    /* Carga una lista de strings a un elemento de tipo Dropdown.
     * Recibe el elemnto Dropdown al que se introducen las opciones y la una lista de strings. */
    void CargarDropdown(TMP_Dropdown dropdown, List<string> opciones)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(opciones);
        dropdown.RefreshShownValue();
    }

    /* Obtiene el nombre de la materia seleccionada y carga su lista de temas en el dropdown correspondiente.
     * Es llamada cada que el valor seleccionado en el dropdown de materias cambia. */
    void MateriaFueSeleccionada(TMP_Dropdown materias)
    {
        string materiaSeleccionada = materias.options[materias.value].text;

        List<string> temas = database.ObtenerTemas(materiaSeleccionada);

        CargarDropdown(dropdownTemas, temas);
        TemaSeleccionado(dropdownTemas);
    }

    void TemaSeleccionado(TMP_Dropdown tema)
    {
        
        var nombreTema = tema.options[tema.value].text;
        var nombreMateria = dropdownMaterias.options[dropdownMaterias.value].text;
        Debug.Log(nombreMateria);

        var idMateria = database.ObtenerIdMateriaPorNombre(nombreMateria);
        Debug.Log(idMateria);
        var idTema = database.ObtenerIdTema(nombreTema);

        var resultados = database.ObtenerPuntajesDeTema(idTema, idMateria);
        tablaResultados.CargarPuntajes(resultados);
    }

}
