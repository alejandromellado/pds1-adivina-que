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
    private static float volumen = 0f;
    private static bool dosjugadores = true;

    public static string Materia
    {
        get { return materia; }
        set { materia = value; }
    }

    public static bool Dosjugadores
    {
        get { return dosjugadores; }
        set { dosjugadores = value; }
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

    public static float Volumen
    {
        get { return volumen; }
        set { volumen = value; }
    }
}
