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
    public GameObject menuInicio;
    public GameObject fondo;

    // Start is called before the first frame update
    void Start()
    {
        database = GetComponent<DatabaseConnection>();
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
            menuInicio.SetActive(true);
            fondo.SetActive(true);
            DataMantainer.Nombre = nombre;
        }

        else { Debug.LogWarning("El nombre introducido no es valido."); }
        
    }
}
