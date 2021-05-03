using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuMaterias : MonoBehaviour
{
    string materia;

    [SerializeField] ToggleGroup grupoDificultad;
    [SerializeField] Toggle facil;
    [SerializeField] Toggle medio;
    [SerializeField] Toggle dificil;

    [SerializeField] ToggleGroup grupoNiveles;
    [SerializeField] Toggle mixto;
    [SerializeField] Toggle complejo;

    [SerializeField] Toggle contrarreloj;
    [SerializeField] Toggle dosjugadores;

    [SerializeField] GameObject menuDificultad;
    [SerializeField] MenuPrincipal menuPrincipal;

    public void CargarNivelEspecial()
    {
        var selectedToggle = grupoNiveles.ActiveToggles().FirstOrDefault();

        if (selectedToggle == mixto)
        {
            DataMantainer.Materia = "Mixto";
            DataMantainer.IdMateria = 6;

            menuDificultad.SetActive(true);
        }
        else if (selectedToggle == complejo)
        {
            DataMantainer.Dificultad = 3;
            DataMantainer.Materia = "Mixto Complejo";
            DataMantainer.IdMateria = 7;

            menuPrincipal.CargarJuego();
        }

        Debug.Log("Materia" + DataMantainer.Materia);
        Debug.Log("Dificultad: " + DataMantainer.Dificultad);
        Debug.Log("idMateria " + DataMantainer.IdMateria);
    }


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

    public void SeleccionarTema(int idTema)
    {
        DataMantainer.IdTema = idTema;
    }

    public void SeleccionarDificultad()
    {
        var selectedToggle = grupoDificultad.ActiveToggles().FirstOrDefault();
        DataMantainer.Contrarreloj = false;
        if (facil.isOn)
        {
            DataMantainer.Dificultad = 1;
            DataMantainer.Tiempo = 15;
        }
        else if (medio.isOn)
        {
            DataMantainer.Dificultad = 2;
            DataMantainer.Tiempo = 30;
        }
        else
        {
            DataMantainer.Dificultad = 3;
            DataMantainer.Tiempo = 60;
        }

        if (contrarreloj.isOn)
        {
            DataMantainer.Contrarreloj = true;
        }else if (dosjugadores.isOn)
        {
        }

        Debug.Log("Materia" + DataMantainer.Materia);
        Debug.Log("Dificultad: " + DataMantainer.Dificultad);
        Debug.Log("idMateria " + DataMantainer.IdMateria);
    }
}
