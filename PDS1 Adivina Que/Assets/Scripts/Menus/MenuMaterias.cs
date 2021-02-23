using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuMaterias : MonoBehaviour
{
    string materia;

    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] Toggle facil;
    [SerializeField] Toggle medio;
    [SerializeField] Toggle dificil;

    public void SeleccionarMateria(int idMateria)

    {
        switch (idMateria)
        {
            case 1:
                DataMantainer.Materia = "Español";
                break;
            case 2:
                DataMantainer.Materia = "Matemáticas";
                break;
            case 3:
                DataMantainer.Materia = "Ciencias Naturales";
                break;
            case 4:
                DataMantainer.Materia = "Historia";
                break;
            case 5:
                DataMantainer.Materia = "Geografía";
                break;
        }

        DataMantainer.IdMateria = idMateria;
    }

    public void SeleccionarDificultad()
    {
        var selectedToggle = toggleGroup.ActiveToggles().FirstOrDefault();

        if (selectedToggle == facil)
        {
            DataMantainer.Dificultad = 1;
        }
        else if (selectedToggle == medio)
        {
            DataMantainer.Dificultad = 2;
        }
        else
        {
            DataMantainer.Dificultad = 3;
        }

        Debug.Log("Materia" + DataMantainer.Materia);
        Debug.Log("Dificultad: " + DataMantainer.Dificultad);
        Debug.Log("idMateria " + DataMantainer.IdMateria);
    }
}
