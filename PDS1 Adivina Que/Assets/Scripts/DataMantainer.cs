using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataMantainer
{
    private static string materia = "";
    private static string nombre = "";
    private static int dificultad = 1;
    private static int idMateria = 1;
    private static int idTema = 1;

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

    public static int IdTema
    {
        get { return idTema; }
        set { idTema = value; }
    }
}
