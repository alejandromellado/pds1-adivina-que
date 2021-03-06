﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseConnection : MonoBehaviour
{
    // Variables
    private string dbName = "URI=file:" + Application.streamingAssetsPath + "/AdivinaQue.db";   // almacena la direccion de la base de datos en el proyecto (raiz del proyecto)

    /* Establece conexion a la base de datos para obtener la tabla de materias.
     * Retorna una lista con los nombres de todas las materias registradas. */
    public List<string> ObtenerMaterias()
    {
        List<string> materias = new List<string>();

        // Crear y abrir conexion a la base de datos usando la direccion al archivo
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de materias
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM materia LIMIT 5;";

                // Ejecutar la consulta crea un objeto IDataReader que puede iterar sobre cada registro en la tabla que resulta de la consulta
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        materias.Add(reader["nombre"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return materias;
    }

    /* Establece conexion a la base de datos y obtiene la lista de temas de una materia.
     * Recibe un string con el nombre de la materia y retorna una lista con los nombres de los temas. */
    public List<string> ObtenerTemas(string materia)
    {
        List<string> temas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de temas y el id de la materia usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tema WHERE (idMateria = (SELECT idMateria FROM materia WHERE (nombre = '" + materia + "')));";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        temas.Add(reader["nombre"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return temas;
    }

    /* Registra el nombre de un jugador en la base de datos, en caso de que ya este registrado el jugador el comando es ignorado. 
     * Recibe el nombre en forma de string. */
    public void RegistrarUsuario(string nombre)
    {
        int registrosCreados = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT OR IGNORE INTO jugador(nombre)VALUES(@nombre);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@nombre", nombre));

                registrosCreados = command.ExecuteNonQuery();
            }

            connection.Close();
        }

        Debug.Log("Se añadieron " + registrosCreados + " registros a la tabla 'jugador'.");
    }

    public List<string> CargarGaleria(string tema)
    {
        List<string> cartas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM carta WHERE (idTema = (SELECT idTema FROM tema WHERE (nombre = @tema)));";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@tema", tema));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        cartas.Add(reader["titulo"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return cartas;
    }

    public List<string> ObtenerCartas(int idTema)
    {
        List<string> cartas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM carta WHERE (idTema = @idTema);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idTema", idTema.ToString()));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        cartas.Add(reader["imagen"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return cartas;
    }

    public string ObtenerMateria(int id)
    {
        string materia = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT nombre FROM materia WHERE(idMateria = @idMateria));";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idMateria", id.ToString()));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        materia = (reader["nombre"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return materia;
        }
    }

    public int ObtenerIdMateria(string imagen)
    {
        int idMateria = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT t.idMateria FROM carta c INNER JOIN tema t WHERE (c.idTema = t.idTema AND c.imagen = @imagen);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@imagen", imagen));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idMateria = Int32.Parse(reader["idMateria"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return idMateria;
        }
    }

    public int ObtenerIdMateriaPorNombre(string nombre)
    {
        int idMateria = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT idMateria FROM materia WHERE (nombre = @nombre);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@nombre", nombre));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idMateria = Int32.Parse(reader["idMateria"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return idMateria;
        }
    }

    public int ObtenerIdTema(string tema)
    {
        int idMateria = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT idTema FROM tema WHERE (nombre = @imagen);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@imagen", tema));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idMateria = Int32.Parse(reader["idTema"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return idMateria;
        }
    }

    public List<string[]> ObtenerPuntajes(int idMateria)
    {
        var resultados = new List<string[]>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT nombre, puntaje FROM jugador j INNER JOIN partida p WHERE (J.idJugador = p.idJugador AND p.idMateria = @idMateria) ORDER BY puntaje DESC;";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idMateria", idMateria));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resultado = new string[2];
                        resultado[0] = (reader["nombre"].ToString());
                        resultado[1] = (reader["puntaje"].ToString());

                        resultados.Add(resultado);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return resultados;
    }

    public List<string[]> ObtenerPuntajesDeTema(int idTema, int idMateria)
    {
        var resultados = new List<string[]>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT nombre, puntaje FROM jugador j INNER JOIN partida p WHERE (J.idJugador = p.idJugador AND p.idTema = @idTema AND p.idMateria = @idMateria) ORDER BY puntaje DESC;";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idTema", idTema));
                command.Parameters.Add(new SqliteParameter("@idMateria", idMateria));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resultado = new string[2];
                        resultado[0] = (reader["nombre"].ToString());
                        resultado[1] = (reader["puntaje"].ToString());

                        resultados.Add(resultado);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return resultados;
    }

    public void RegistrarScore(int id, int materia, int num_errores, int puntos, int tema)
    {
        int registrosCreados = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO partida (idJugador, idMateria, num_errores, puntaje, idTema) VALUES(@jugador, @materia, @errores, @puntos, @tema);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@jugador", id.ToString()));
                command.Parameters.Add(new SqliteParameter("@materia", materia.ToString()));
                command.Parameters.Add(new SqliteParameter("@errores", num_errores.ToString()));
                command.Parameters.Add(new SqliteParameter("@puntos", puntos.ToString()));
                command.Parameters.Add(new SqliteParameter("@tema", tema.ToString()));

                registrosCreados = command.ExecuteNonQuery();
            }

            connection.Close();
        }

        Debug.Log("Se añadieron " + registrosCreados + " registros a la tabla 'resultados'.");
    }

    public int ObtenerJugador(string nombre)
    {
        int idJugador = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT idJugador FROM jugador WHERE(nombre = @nombre);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@nombre", nombre));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idJugador = Int32.Parse(reader["idJugador"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return idJugador;
        }


    }

    public List<string> ObtenerCartas()
    {
        List<string> cartas = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM carta;";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        cartas.Add(reader["imagen"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return cartas;
    }

    public List<string> ObtenerDatos(int idMateria)
    {
        List<string> datos = new List<string>();
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT dato FROM datos WHERE (idMateria = @idMateria);";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idMateria", idMateria));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        datos.Add(reader["dato"].ToString());
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        return datos;
    }

    public string ObtenerDato(int idMateria)
    {
        string dato = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT dato FROM datos WHERE (idMateria = @idMateria) ORDER BY RANDOM() LIMIT 1;";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idMateria", idMateria));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dato = (reader["dato"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return dato;
        }
    }

    public List<string> ObtenerCarta(string nombre)
    {
        List<string> carta = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = string.Format("SELECT * FROM carta WHERE (titulo = '{0}');", nombre);

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar los nombres de los temas a la lista
                        carta.Add(reader["imagen"].ToString());
                        carta.Add(reader["pista"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return carta;
    }

    public string ObtenerTema(int id)
    {
        string tema = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT nombre FROM tema WHERE(idTema = @idTema);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idTema", id.ToString()));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tema = (reader["nombre"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return tema;
        }
    }

    public string ObtenerInstrucciones(int id)
    {
        string tema = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Crear consulta para obtener tabla de cartas y el id del tema usando su nombre
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT indicaciones FROM tema WHERE(idTema = @idTema);";

                // Especificar el comando como una consulta y añadir el parametro 'nombre'
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("@idTema", id.ToString()));

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tema = (reader["indicaciones"].ToString());
                    }

                    reader.Close();
                }
            }

            connection.Close();

            return tema;
        }
    }

}
