﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMaterias : MonoBehaviour
{
    string materia;

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
}