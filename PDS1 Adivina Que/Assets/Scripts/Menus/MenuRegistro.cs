using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MenuRegistro : MonoBehaviour
{
    DatabaseConnection database;

    // Referencias a elementos en la jerarquia
    public TMP_InputField inputField;
    public GameObject menuRegistro;
    public GameObject menuDatos;
    public GameObject menuPrincipal;
    public GameObject fondo;
    public GameObject menuOpciones;

    // Start is called before the first frame update
    void Start()
    {
        database = GetComponent<DatabaseConnection>();

        if (DataMantainer.Nombre != "")
        {
            Debug.Log("This was true");
            menuRegistro.SetActive(false);
            menuDatos.SetActive(true);
            fondo.SetActive(false);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continuar()
    {
        string nombre = inputField.text;

        if (!string.IsNullOrWhiteSpace(nombre))
        {
            database.RegistrarUsuario(nombre);
            menuRegistro.SetActive(false);
            menuPrincipal.SetActive(true);
            fondo.SetActive(true);
            
            DataMantainer.Nombre = nombre;
        }

        else { Debug.LogWarning("El nombre introducido no es valido."); }
        
    }

    public void Opciones()
    {
        menuRegistro.SetActive(false);
        menuOpciones.SetActive(true);
    }
}
