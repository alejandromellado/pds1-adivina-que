using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Instrucciones : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tema;
    [SerializeField] TextMeshProUGUI instrucciones;
    DatabaseConnection database;

    // Start is called before the first frame update
    void Start()
    {
        if (DataMantainer.Materia != "Mixto" && DataMantainer.Materia != "Mixto Complejo")
        {
            database = GetComponent<DatabaseConnection>();
            tema.text = database.ObtenerTema(DataMantainer.IdTema);
            instrucciones.text = database.ObtenerInstrucciones(DataMantainer.IdTema);
        }
        else
        {
            tema.text = DataMantainer.Materia;
            instrucciones.text = "Relaciona las cartartas correspondientes de diferentes temas.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
