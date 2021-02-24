using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class MenuGaleria : MonoBehaviour
{
    DatabaseConnection database;

    // Referencias a elementos en la jerarquia
    public TMP_Dropdown dropdownMaterias;
    public TMP_Dropdown dropdownTemas;

    public GameObject scrollView;
    public GameObject viewPort;
    public GameObject contentTemplate;
    public GameObject toggleTemplate;

    private GameObject currentContent;

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
            CargarScrollView();
        });

        // Obtener la lista de materias de la base de datos y cargarla en el dropdown correspondiente
        List<string> materias = database.ObtenerMaterias();
        CargarDropdown(dropdownMaterias, materias);

        // Llamar el metodo correspondiente para cargar la lista de temas por primera vez
        MateriaFueSeleccionada(dropdownMaterias);
        CargarScrollView();
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
        CargarScrollView();
    }

    /* Carga una lista de strings a un elemento de tipo Dropdown.
     * Recibe el elemnto Dropdown al que se introducen las opciones y la una lista de strings. */
    void CargarDropdown(TMP_Dropdown dropdown, List<string> opciones)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(opciones);
        dropdown.RefreshShownValue();
    }

    void CargarScrollView()
    {
        Debug.Log("CargarScrollView() ha sido llamado");

        // Revisar si hay contenido previo a cargar cartas
        if (currentContent != null)
        {
            Destroy(currentContent);
        }

        // Obtener datos necesarios para cargar cartas
        var tema = dropdownTemas.options[dropdownTemas.value].text;
        var cartas = database.CargarGaleria(tema);
        Debug.Log("Tema escogido: " + tema);

        // Instanciar el contenedor para el ScrollView y activar la instancia
        currentContent = Instantiate(contentTemplate);
        currentContent.transform.SetParent(viewPort.transform, false);

        scrollView.GetComponent<ScrollRect>().content = currentContent.GetComponent<RectTransform>();

        currentContent.SetActive(true);

        // Crear un toggle para cada carta y asignarle el texto adecuado
        foreach (var carta in cartas)
        {
            Debug.Log(carta);

            GameObject nuevoBoton = Instantiate(toggleTemplate);
            nuevoBoton.transform.SetParent(currentContent.transform, false);
            nuevoBoton.GetComponent<ToggleScrollView>().SetText(carta);
            nuevoBoton.SetActive(true);
        }

    }

}
