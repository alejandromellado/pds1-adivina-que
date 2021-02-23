using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataMantainer
{
    private static string materia, nombre;
    private static int dificultad, idMateria;

    public static string Materia
    {
        get { return materia; }
        set { materia = value; }
    }

    public static string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public static int Dificultad
    {
        get { return dificultad; }
        set { dificultad = value; }
    }

    public static int IdMateria
    {
        get { return idMateria; }
        set { idMateria = value; }
    }
}
